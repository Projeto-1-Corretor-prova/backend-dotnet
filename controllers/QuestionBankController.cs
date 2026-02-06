using AutoMapper;
using backend.dtos.questionBank;
using backend.dtos.questionCriteria;
using backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class QuestionBankController(TeacherDbContext context, IMapper mapper) : ControllerBase
{
    [backend.attributes.Authorize]
    [HttpPost]
    [Route(Routes.BaseQuestionBankUrl)]
    public async Task<IActionResult> Post(
        [FromBody] QuestionBankCreateDto questionBankCreateDto)
    {
        var teacherId = (int?) HttpContext.Items["TeacherId"];
        var questionBank = mapper.Map<QuestionBank>(questionBankCreateDto);
        questionBank.TeacherId = teacherId!.Value;
        var createdQuestionBank = context.QuestionBanks.Add(questionBank).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<QuestionBankDto>(createdQuestionBank));
    }

    [backend.attributes.Authorize]
    [HttpPut]
    [Route(Routes.QuestionBankByIdUrl)]
    public async Task<IActionResult> Put(
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
    
    [backend.attributes.Authorize]
    [HttpGet]
    [Route(Routes.QuestionBankByIdUrl)]
    public async Task<IActionResult> Get(
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

}