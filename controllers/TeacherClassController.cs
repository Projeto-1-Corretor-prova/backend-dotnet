using AutoMapper;
using backend.dtos.teacherClass;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class TeacherClassController(TeacherDbContext context, IMapper mapper) : ControllerBase
{
    
    [HttpPost]
    [Route(Routes.BaseTeacherClassUrl)]
    [Authorize]
    public async Task<ActionResult<TeacherClassDto>> Post(
        [FromBody] TeacherClassCreateDto teacherClassCreateDto)
    {
                
        if (!this.TryGetUserIdFromToken(out int teacherId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }
        var teacher = await context.Teachers.FindAsync(teacherId);
        if (teacher == null) return new NotFoundResult();
        var teacherClass = mapper.Map<TeacherClass>(teacherClassCreateDto);
        teacherClass.TeacherId = teacherId;
        var createdTeacherClass = context.TeacherClasses.Add(teacherClass).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<TeacherClassDto>(createdTeacherClass));
    }
    
    [HttpPut(Routes.TeacherClassByIdUrl)]
    [Authorize]
    public async Task<ActionResult<TeacherClassDto>> Put(
        [FromRoute] int id,
        [FromBody] TeacherClassUpdateDto teacherClassUpdateDto)
    {
        var teacherClass = await context
            .TeacherClasses
            .AsQueryable()
            .Where(tc => tc.Id == id)
            .Include(tc => tc.Students)
            .Include(tc => tc.TestWrittens)
            .ThenInclude(tw => tw.QuestionTestWrittens)
            .FirstOrDefaultAsync();
        if (teacherClass == null) return new NotFoundResult();
        teacherClass = mapper.Map<TeacherClassUpdateDto, TeacherClass>(teacherClassUpdateDto, teacherClass);
        teacherClass = context.TeacherClasses.Update(teacherClass).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<TeacherClassDto>(teacherClass));
    }
    
     [HttpGet(Routes.TeacherClassByIdUrl)]
     [Authorize]
     public async Task<ActionResult<TeacherClassDto>> Get(
         [FromRoute] int id)
     {
         var teacherClass = await context.TeacherClasses
             .AsNoTracking()
             .AsQueryable()
             .Where(tc => tc.Id == id)
             .Include(tc => tc.Students)
             .Include(tc => tc.TestWrittens)
             .ThenInclude(tw => tw.QuestionTestWrittens)
             .FirstOrDefaultAsync();
         if (teacherClass == null) return new NotFoundResult();
         return new OkObjectResult(mapper.Map<TeacherClassDto>(teacherClass));
     }
}