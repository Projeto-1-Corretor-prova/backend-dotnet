using backend.dtos.student;
using backend.dtos.testWritten;

namespace backend.dtos.teacherClass;

public record TeacherClassDto(
    int Id,
    string Title,
    List<TestWrittenMiniDto> Tests,
    List<StudentMiniDto> Students);