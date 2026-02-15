using AutoMapper;
using backend.dtos.question;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class QuestionController(TeacherDbContext context, IMapper mapper) : ControllerBase
{
        [Authorize]
        [HttpGet]
        [Route(Routes.QuestionByIdUrl)]
        public async Task<ActionResult<QuestionDto>> Get([FromRoute] int id)
        {
            var question = await context
                .Questions
                .AsNoTracking()
                .AsQueryable()
                .Where(q => q.Id == id)
                .Include(q => q.QuestionCriterias)
                .FirstOrDefaultAsync();
            if (question == null)
                return new NotFoundResult();
            
            return new OkObjectResult(mapper.Map<QuestionDto>(question));
        }

        [Authorize]
        [HttpPost]
        [Route(Routes.QuestionCreateUrl)]
        public async Task<ActionResult<QuestionDto>> Post(
            [FromRoute] int questionBankId,
            [FromBody] QuestionCreateDto questionCreateDto)
        {
            var questionBank = await context.QuestionBanks.FindAsync(questionBankId);
            if (questionBank == null) return new NotFoundResult();
            var question = mapper.Map<Question>(questionCreateDto);
            question.QuestionBankId = questionBankId;
            var createdQuestion = context.Questions.Add(question).Entity;
            await context.SaveChangesAsync();
            return new OkObjectResult(mapper.Map<QuestionDto>(createdQuestion));
        }

        [Authorize]
        [HttpPut]
        [Route(Routes.QuestionByIdUrl)]
        public async Task<ActionResult<QuestionDto>> Put(
            [FromRoute] int id,
            [FromBody] QuestionUpdateDto questionUpdateDto)
        {
            var question = await context
                .Questions
                .AsQueryable()
                .Where(q => q.Id == id)
                .Include(q => q.QuestionCriterias)
                .FirstOrDefaultAsync();
            if (question == null)
                return new NotFoundResult();
            question = mapper.Map<QuestionUpdateDto, Question>(questionUpdateDto, question);
            question = context.Questions.Update(question).Entity;
            await context.SaveChangesAsync();
            return new OkObjectResult(mapper.Map<QuestionDto>(question));
        }
}