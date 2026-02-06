using AutoMapper;
using backend.dtos.teacherClass;
using backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class TeacherClassController(TeacherDbContext context, IMapper mapper) : ControllerBase
{
    
    [backend.attributes.Authorize]
    [HttpPost]
    [Route(Routes.BaseTeacherClassUrl)]
    public async Task<IActionResult> Post(
        [FromBody] TeacherClassCreateDto teacherClassCreateDto)
    {
        var teacherId = (int?) HttpContext.Items["TeacherId"];
        var teacher = await context.Teachers.FindAsync(teacherId);
        if (teacher == null) return new NotFoundResult();
        var teacherClass = mapper.Map<TeacherClass>(teacherClassCreateDto);
        teacherClass.TeacherId = teacherId!.Value;
        var createdTeacherClass = context.TeacherClasses.Add(teacherClass).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<TeacherClassDto>(createdTeacherClass));
    }
    
    [backend.attributes.Authorize]
    [HttpPut(Routes.TeacherClassByIdUrl)]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] TeacherClassUpdateDto teacherClassUpdateDto)
    {
        var teacherClass = await context
            .TeacherClasses
            .AsNoTracking()
            .AsQueryable()
            .Where(tc => tc.Id == id)
            .Include(tc => tc.Students)
            .Include(tc => tc.TestWrittens)
            .FirstOrDefaultAsync();
        if (teacherClass == null) return new NotFoundResult();
        teacherClass = mapper.Map<TeacherClassUpdateDto, TeacherClass>(teacherClassUpdateDto, teacherClass);
        teacherClass = context.TeacherClasses.Update(teacherClass).Entity;
        await context.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<TeacherClassDto>(teacherClass));
    }
    
     [backend.attributes.Authorize]
     [HttpGet(Routes.TeacherClassByIdUrl)]
     public async Task<IActionResult> Get(
         [FromRoute] int id)
     {
         var teacherClass = await context.TeacherClasses
             .AsNoTracking()
             .AsQueryable()
             .Where(tc => tc.Id == id)
             .Include(tc => tc.Students)
             .Include(tc => tc.TestWrittens)
             .FirstOrDefaultAsync();
         if (teacherClass == null) return new NotFoundResult();
         return new OkObjectResult(mapper.Map<TeacherClassDto>(teacherClass));
     }
}