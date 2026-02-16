using AutoMapper;
using backend.dtos.testWritten;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class TestWrittenController(TeacherDbContext teacherDbContext, IMapper mapper): ControllerBase
{
    
    [Authorize]
    [HttpPost(Routes.TestWrittenCreateUrl)]
    public async Task<ActionResult<TestWrittenDto>> Post(
        [FromRoute] int teacherClassId,
        [FromBody] TestWrittenCreateDto teacherCreateDto)
    {
        var teacherClass = await teacherDbContext.TeacherClasses.FindAsync(teacherClassId);
        if (teacherClass == null) return new NotFoundResult();
        var testWritten = mapper.Map<TestWritten>(teacherCreateDto);
        testWritten.TeacherClassId = teacherClassId;
        var createdTestWritten = teacherDbContext.TestWrittens.Add(testWritten).Entity;
        await teacherDbContext.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<TestWrittenDto>(createdTestWritten));
    }

    [Authorize]
    [HttpPut(Routes.TestWrittenByIdUrl)]
    public async Task<ActionResult<TestWrittenDto>> Put(
        [FromRoute] int id,
        [FromBody] TestWrittenUpdateDto testWrittenUpdateDto)
    {
        var testWritten = await teacherDbContext
            .TestWrittens
            .AsQueryable()
            .Where(tw => tw.Id == id)
            .Include(tw => tw.Corrections)
            .ThenInclude(c => c.Student)
            .Include(tw => tw.QuestionTestWrittens)
            .ThenInclude(qtw => qtw.Question)
            .FirstOrDefaultAsync();
        if (testWritten == null) return new NotFoundResult();
        var updatedTestWritten = mapper.Map<TestWrittenUpdateDto, TestWritten>(testWrittenUpdateDto, testWritten);
        updatedTestWritten = teacherDbContext.TestWrittens.Update(updatedTestWritten).Entity;
        await teacherDbContext.SaveChangesAsync();
        return new OkObjectResult(mapper.Map<TestWrittenDto>(updatedTestWritten));
    }

    [Authorize]
    [HttpGet(Routes.TestWrittenByIdUrl)]
    public async Task<ActionResult<TestWrittenDto>> Get(
        [FromRoute] int id)
    {
        var testWritten = await teacherDbContext
            .TestWrittens
            .AsNoTracking()
            .AsQueryable()
            .Where(tw => tw.Id == id)
            .Include(tw => tw.Corrections)
            .ThenInclude(c => c.Student)
            .Include(tw => tw.QuestionTestWrittens)
            .ThenInclude(qtw => qtw.Question)
            .FirstOrDefaultAsync();;
        if (testWritten == null) return new NotFoundResult();
        return new OkObjectResult(mapper.Map<TestWrittenDto>(testWritten));
    }
    
    [Authorize]
    [HttpDelete]
    [Route(Routes.TestWrittenByIdUrl)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var testWritten = await teacherDbContext.TestWrittens.FindAsync(id);
        if (testWritten == null) return new NotFoundResult();
        teacherDbContext.TestWrittens.Remove(testWritten);
        await teacherDbContext.SaveChangesAsync();
        return new NoContentResult();
    }
}