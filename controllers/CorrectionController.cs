using AutoMapper;
using backend.attributes;
using backend.dtos.correction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class CorrectionController(TeacherDbContext context, IMapper mapper) : ControllerBase
{
    
    [Authorize]
    [HttpGet]
    [Route(Routes.CorrectionByIdUrl)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var correction = await context
            .Corrections
            .AsNoTracking()
            .AsQueryable()
            .Where(c => c.Id == id)
            .Include(c => c.TestWritten)
            .Include(c => c.Student)
            .Include(c => c.Answers)
            .ThenInclude(a => a.AiComments)
            .Include(c => c.Answers)
            .ThenInclude(a => a.TeacherComments)
            .Include(c => c.Answers)
            .ThenInclude(a => a.QuestionTestWritten)
            .ThenInclude(q => q.Question)
            .FirstOrDefaultAsync();
            
        if (correction == null)
            return new NotFoundResult();
        
        var correctionDto = mapper.Map<CorrectionDto>(correction);
        
        return new OkObjectResult(correctionDto);
    }
    
}