using backend.dtos.teacherClass;

namespace backend.dtos.teacher;

public record TeacherDto(
    int Id,
    string Name,
    List<TeacherClassMiniDto> Classes
    );