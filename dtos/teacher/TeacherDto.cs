using backend.dtos.questionBank;
using backend.dtos.teacherClass;

namespace backend.dtos.teacher;

public record TeacherDto(
    int Id,
    string Name, 
    string Email,
    List<TeacherClassMiniDto> TeacherClasses,
    List<QuestionBankMiniDto> QuestionBanks
    );