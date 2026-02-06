using backend.enums;

namespace backend.models;

public class Comment
{
    public int Id { get; set; }
    
    public string Content { get; set; }
    public CommentEnum Type { get; set; }
    
    #region Answer (Ai) Foreign Key
    public int? AnswerAiId { get; set; }
    public Answer? AnswerAi { get; set; }
    #endregion
    
    #region Answer (Teacher) Foreign Key
    public int? AnswerTeacherId { get; set; }
    public Answer? AnswerTeacher { get; set; }
    #endregion
}