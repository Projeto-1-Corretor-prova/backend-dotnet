namespace backend;

public static class Routes
{
    // Base
    public const string BaseUrl = "api";
    
    // Teacher routes
    public const string BaseTeacherUrl = "teacher";
    public const string TeacherRegisterUrl = BaseTeacherUrl + "/register";
    public const string TeacherLoginUrl = BaseTeacherUrl + "/login";
    public const string TeacherProfileUrl = BaseTeacherUrl + "/profile";
    
    // Student routes
    public const string BaseStudentUrl = "student";
    public const string StudentCreateUrl = BaseStudentUrl + "/teacher-class/{id}";
    public const string StudentByIdUrl = BaseStudentUrl + "/{id}";
    
    // Teacher Class routes
    public const string BaseTeacherClassUrl = "teacher-class";
    public const string TeacherClassByIdUrl = BaseTeacherClassUrl + "/{id}";
    
    // Question Bank routes
    public const string BaseQuestionBankUrl = "question-bank";
    public const string QuestionBankByIdUrl = BaseQuestionBankUrl + "/{id}";
    
    // Question routes
    public const string BaseQuestionUrl = "question";
    public const string QuestionByIdUrl = BaseQuestionUrl + "/{id}";
    public const string QuestionCreateUrl = BaseQuestionUrl + "/question-bank/{questionBankId}";
    
    // Test Written routes
    public const string BaseTestWrittenUrl = "test-written";
    public const string TestWrittenCreateUrl = BaseTestWrittenUrl + "/teacher-class/{teacherClassId}";
    public const string TestWrittenByIdUrl = BaseTestWrittenUrl + "/{id}";
    
    // Answer routes
    public const string BaseAnswerUrl = "answer";
    public const string AnswerByIdUrl = BaseAnswerUrl + "/{id}";
    
    // Comment routes
    public const string BaseCommentUrl = "comment";
    public const string CommentCreateUrl = BaseCommentUrl + "/answers/{answerId}";
    public const string CommentByIdUrl = BaseCommentUrl + "/{commentId}";
    
    // Correction routes
    public const string BaseCorrectionUrl = "correction";
    public const string CorrectionByIdUrl = BaseCorrectionUrl + "/{id}";
    
    // Question Criteria routes
    public const string BaseQuestionCriteriaUrl = "question-criteria";
    public const string QuestionCriteriaCreateUrl = BaseQuestionCriteriaUrl + "/question/{questionId}";
    public const string QuestionCriteriaByIdUrl = BaseQuestionCriteriaUrl + "/{id}";
    
    // Question Test Written routes
    public const string BaseQuestionTestWrittenUrl = "question-test-written";
    public const string QuestionTestWrittenCreateUrl = BaseQuestionTestWrittenUrl + "/question/{questionId}/test-written/{testWrittenId}";
    public const string QuestionTestWrittenByIdUrl = BaseQuestionTestWrittenUrl + "/{id}";
}