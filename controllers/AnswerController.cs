using AutoMapper;
using backend.dtos.answer;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class AnswerController(TeacherDbContext context, IMapper mapper) : ControllerBase
{
    
    [Authorize]
    [HttpPut]
    [Route(Routes.AnswerByIdUrl)]
    public async Task<ActionResult<AnswerDto>> Put(
        [FromRoute] int id,
        [FromBody] AnswerUpdateDto answerUpdatedto)
    {
        var answer = await context.Answers.AsQueryable()
            .Where(a => a.Id == id)
            .Include(a => a.AiComments)
            .Include(a => a.TeacherComments)
            .FirstOrDefaultAsync();

        if (answer == null)
            return new NotFoundResult();
        
        answer = mapper.Map<AnswerUpdateDto, Answer>(answerUpdatedto);
        
        answer = context.Answers.Update(answer).Entity;
        await context.SaveChangesAsync();
        
        return new OkObjectResult(mapper.Map<Answer, AnswerDto>(answer));
    }
    
}