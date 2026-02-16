using AutoMapper;
using backend.dtos.questionTestWritten;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class QuestionTestWrittenController(TeacherDbContext context, IMapper mapper) : ControllerBase
{

    [Authorize]
    [HttpPost]
    [Route(Routes.QuestionTestWrittenCreateUrl)]
    public async Task<ActionResult<QuestionTestWrittenDto>> Post(
        [FromRoute] int questionId,
        [FromRoute] int testWrittenId,
        [FromBody] QuestionTestWrittenCreateDto questionTestWrittenCreateDto)
    {
        var question = await context.Questions.FindAsync(questionId);
        var testWritten = await context.TestWrittens.FindAsync(testWrittenId);
        if (question == null || testWritten == null) return new NotFoundResult();
        var questionTestWritten = mapper.Map<QuestionTestWritten>(questionTestWrittenCreateDto);
        questionTestWritten.Question = question;
        questionTestWritten.QuestionId = questionId;
        questionTestWritten.TestWrittenId = testWrittenId;
        var createdQuestionTestWritten = context.QuestionTestWrittens.Add(questionTestWritten).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<QuestionTestWrittenDto>(createdQuestionTestWritten));
    }
    
    [Authorize]
    [HttpPut]
    [Route(Routes.QuestionTestWrittenByIdUrl)]
    public async Task<ActionResult<QuestionTestWrittenDto>> Put(
        [FromRoute] int id,
        [FromBody] QuestionTestWrittenUpdateDto questionTestWrittenUpdateDto)
    {
        var questionTestWritten = await context
            .QuestionTestWrittens
            .AsQueryable()
            .Where(q => q.Id == id)
            .Include(q => q.Question)
            .FirstOrDefaultAsync();
        if (questionTestWritten == null) return new NotFoundResult();
        var updatedQuestionTestWritten =
            mapper.Map<QuestionTestWrittenUpdateDto, QuestionTestWritten>(questionTestWrittenUpdateDto,
                questionTestWritten);
        updatedQuestionTestWritten = context.QuestionTestWrittens.Update(updatedQuestionTestWritten).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<QuestionTestWrittenDto>(updatedQuestionTestWritten));
    }
    
    [Authorize]
    [HttpDelete]
    [Route(Routes.QuestionTestWrittenByIdUrl)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var questionTestWritten = await context.QuestionTestWrittens.FindAsync(id);
        if (questionTestWritten == null) return new NotFoundResult();
        context.QuestionTestWrittens.Remove(questionTestWritten);
        await context.SaveChangesAsync();
        return new NoContentResult();
    }

}