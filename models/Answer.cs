namespace backend.models;

public class Answer
{
    public int Id { get; set; }
    
    public string StudentAnswer { get; set; }
    public double Score { get; set; }
    
    # region Correction Foreign Key
    public int CorrectionId { get; set; }
    public Correction Correction { get; set; }
    # endregion
    
    #region QuestionTestWritten Foreign Key
    public int QuestionTestWrittenId { get; set; }
    public QuestionTestWritten QuestionTestWritten { get; set; }
    #endregion

    public List<Comment> AiComments { get; set; } = [];
    public List<Comment> TeacherComments { get; set; } = [];
    
}