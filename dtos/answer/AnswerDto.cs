using backend.dtos.comment;
using backend.dtos.question;
using backend.dtos.questionTestWritten;

namespace backend.dtos.answer;

public record AnswerDto(
    int Id,
    string StudentAnswer,
    double Score,
    QuestionTestWrittenDto Question,
    List<CommentDto> AIComments,
    List<CommentDto> TeacherComments);