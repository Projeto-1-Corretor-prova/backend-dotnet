using AutoMapper;
using backend.dtos.questionBank;
using backend.dtos.questionCriteria;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class QuestionBankController(TeacherDbContext context, IMapper mapper) : ControllerBase
{
    [Authorize]
    [HttpPost]
    [Route(Routes.BaseQuestionBankUrl)]
    public async Task<ActionResult<QuestionBankDto>> Post(
        [FromBody] QuestionBankCreateDto questionBankCreateDto)
    {
                
        if (!this.TryGetUserIdFromToken(out int teacherId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }
        var questionBank = mapper.Map<QuestionBank>(questionBankCreateDto);
        questionBank.TeacherId = teacherId;
        var createdQuestionBank = context.QuestionBanks.Add(questionBank).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<QuestionBankDto>(createdQuestionBank));
    }

    [Authorize]
    [HttpPut]
    [Route(Routes.QuestionBankByIdUrl)]
    public async Task<ActionResult<QuestionBankDto>> Put(
        [FromRoute] int id,
        [FromBody] QuestionBankUpdateDto questionBankUpdateDto)
    {
        var questionBank = await context
            .QuestionBanks
            .AsQueryable()
            .Where(q => q.Id == id)
            .Include(q => q.Questions)
            .FirstOrDefaultAsync();
        if (questionBank == null) return new NotFoundResult();
        questionBank = mapper.Map<QuestionBankUpdateDto, QuestionBank>(questionBankUpdateDto, questionBank);
        questionBank = context.QuestionBanks.Update(questionBank).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<QuestionBankDto>(questionBank));
    }
    
    [Authorize]
    [HttpGet]
    [Route(Routes.QuestionBankByIdUrl)]
    public async Task<ActionResult<QuestionBankDto>> Get(
        [FromRoute] int id)
    {
        var questionBank = await context.QuestionBanks
            .AsNoTracking()
            .AsQueryable()
            .Where(q => q.Id == id)
            .Include(q => q.Questions)
            .FirstOrDefaultAsync();
        if (questionBank == null) return new NotFoundResult();
        return new OkObjectResult(mapper.Map<QuestionBankDto>(questionBank));
    }

    [Authorize]
    [HttpDelete]
    [Route(Routes.QuestionBankByIdUrl)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var questionBank = await context.QuestionBanks.FindAsync(id);
        if (questionBank == null)
            return new NotFoundResult();
        context.QuestionBanks.Remove(questionBank);
        await context.SaveChangesAsync();
        return new NoContentResult();
    }
    
}