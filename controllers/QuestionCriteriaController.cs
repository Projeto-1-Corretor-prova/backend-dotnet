using AutoMapper;
using backend.dtos.questionCriteria;
using backend.models;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers;

[ApiController]
public class QuestionCriteriaController(TeacherDbContext teacherDbContext, IMapper mapper) : ControllerBase
{

    [backend.attributes.Authorize]
    [HttpPost]
    [Route(Routes.QuestionCriteriaCreateUrl)]
    public async Task<IActionResult> Post(
        [FromRoute] int questionId,
        [FromBody] QuestionCriteriaCreateDto questionCriteriaCreateDto)
    {
        var question = await teacherDbContext.QuestionCriterias.FindAsync(questionId);
        if (question == null) return new NotFoundResult();
        var questionCriteria = mapper.Map<QuestionCriteria>(questionCriteriaCreateDto);
        questionCriteria.QuestionId = questionId;
        var createdQuestionCriteria = teacherDbContext.QuestionCriterias.Add(questionCriteria).Entity;
        await teacherDbContext.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<QuestionCriteriaDto>(createdQuestionCriteria));
    }

    [backend.attributes.Authorize]
    [HttpPut]
    [Route(Routes.QuestionCriteriaByIdUrl)]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] QuestionCriteriaUpdateDto questionCriteriaUpdateDto)
    {
        var questionCriteria = await teacherDbContext.QuestionCriterias.FindAsync(id);
        if (questionCriteria == null) return new NotFoundResult();
        var updatedQuestionCriteria = mapper.Map<QuestionCriteriaUpdateDto, QuestionCriteria>(questionCriteriaUpdateDto, questionCriteria);
        updatedQuestionCriteria = teacherDbContext.QuestionCriterias.Update(updatedQuestionCriteria).Entity;
        await teacherDbContext.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<QuestionCriteriaDto>(updatedQuestionCriteria));
    }
    
}