using AutoMapper;
using backend.dtos.teacher;
using backend.models;
using backend.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers;

[ApiController]
public class TeacherController : ControllerBase
{
    private readonly TeacherDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public TeacherController(
        TeacherDbContext context, 
        IJwtService jwtService, 
        IAuthService authService,
        IMapper mapper)
    {
        _context = context;
        _jwtService = jwtService;
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route(Routes.TeacherRegisterUrl)]
    public async Task<IActionResult> Register([FromBody] TeacherCreateDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if email already exists
        if (await _context.Teachers.AnyAsync(t => t.Email == registerDto.Email))
        {
            return BadRequest(new { message = "Email already in use" });
        }

        var teacher = _mapper.Map<Teacher>(registerDto);
        
        // Hash password
        teacher.Password = _authService.HashPassword(registerDto.Password);

        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registration successful" });
    }
    
    [HttpPost(Routes.TeacherLoginUrl)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Find teacher by email
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Email == loginDto.Email);

        if (teacher == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Verify password
        if (!_authService.VerifyPassword(loginDto.Password, teacher.Password))
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Generate JWT token
        var token = _jwtService.GenerateToken(teacher);

        return Ok(new
        {
            token,
            teacher = new
            {
                id = teacher.Id,
                name = teacher.Name,
                email = teacher.Email
            }
        });
    }

    [HttpGet(Routes.TeacherProfileUrl)]
    [backend.attributes.Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var teacherId = (int?)HttpContext.Items["TeacherId"];
        
        if (teacherId == null)
        {
            return Unauthorized(new { message = "Teacher not found in token" });
        }

        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == teacherId);

        if (teacher == null)
        {
            return NotFound(new { message = "Teacher not found" });
        }

        return Ok(new
        {
            id = teacher.Id,
            name = teacher.Name,
            email = teacher.Email
        });
    }
}