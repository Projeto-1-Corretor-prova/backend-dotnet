using AutoMapper;
using backend.dtos.student;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class StudentController(TeacherDbContext teacherDbContext, IMapper mapper) : ControllerBase
{
    [Authorize]
    [HttpPost]
    [Route(Routes.StudentCreateUrl)]
    public async Task<ActionResult<StudentDto>> Post(
        [FromRoute] int id,
        [FromBody] StudentCreateDto studentCreateDto)
    {
        var teacherClass = await teacherDbContext.TeacherClasses.FindAsync(id);
        if (teacherClass == null) return new NotFoundResult();
        var student = mapper.Map<Student>(studentCreateDto);
        student.TeacherClassId = id;
        var createdStudent = teacherDbContext.Students.Add(student).Entity;
        await teacherDbContext.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<StudentDto>(createdStudent));
    }

    [Authorize]
    [HttpPut]
    [Route(Routes.StudentByIdUrl)]
    public async Task<ActionResult<StudentDto>> Put(
        [FromRoute] int id,
        [FromBody] StudentUpdateDto studentUpdateDto)
    {
        var student = await teacherDbContext
            .Students
            .AsQueryable()
            .Where(s => s.Id == id)
            .Include(s => s.Corrections)
            .ThenInclude(c => c.TestWritten)
            .FirstOrDefaultAsync();
        if (student == null) return new NotFoundResult();
        var updatedStudent = mapper.Map<StudentUpdateDto, Student>(studentUpdateDto, student);
        updatedStudent = teacherDbContext.Students.Update(updatedStudent).Entity;
        await teacherDbContext.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<StudentDto>(updatedStudent));
    }

    [Authorize]
    [HttpGet]
    [Route(Routes.StudentByIdUrl)]
    public async Task<ActionResult<StudentDto>> Get(
        [FromRoute] int id)
    {
        var student = await teacherDbContext.Students
            .AsNoTracking()
            .AsQueryable()
            .Where(s => s.Id == id)
            .Include(s => s.Corrections)
            .ThenInclude(c => c.TestWritten)
            .FirstOrDefaultAsync();
        if (student == null) return new NotFoundResult();
        return new OkObjectResult(mapper.Map<StudentDto>(student));
    }
}