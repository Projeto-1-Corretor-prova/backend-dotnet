using AutoMapper;
using backend.dtos.comment;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers;

[ApiController]
public class CommentController(TeacherDbContext context, IMapper mapper) : ControllerBase
{
    
    [Authorize]
    [HttpPost]
    [Route(Routes.CommentCreateUrl)]
    public async Task<ActionResult<CommentDto>> Post(
        [FromRoute] int answerId,
        [FromBody] CommentCreateDto commentCreateDto)
    {
        var comment = mapper.Map<Comment>(commentCreateDto);
        comment.AnswerTeacherId = answerId;
        comment = context.Comments.Add(comment).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<Comment, CommentDto>(comment));
    }

    [Authorize]
    [HttpPut]
    [Route(Routes.CommentByIdUrl)]
    public async Task<ActionResult<CommentDto>> Put(
        [FromRoute] int commentId,
        [FromBody] CommentUpdateDto commentUpdateDto)
    {
        var comment = await context.Comments.FindAsync(commentId);
        if (comment == null)
            return new NotFoundResult();
        comment = mapper.Map<CommentUpdateDto, Comment>(commentUpdateDto, comment);
        comment = context.Comments.Update(comment).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<Comment, CommentDto>(comment));
    }
    
}