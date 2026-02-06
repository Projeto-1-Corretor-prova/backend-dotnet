namespace backend.models;

public class Question
{
    public int Id { get; set; }
    
    public string Statement { get; set; }

    #region Question Bank Foreign Key
    public int QuestionBankId { get; set; }
    public QuestionBank QuestionBank { get; set; }
    #endregion

    public List<QuestionTestWritten> QuestionTestWrittens { get; set; } = [];
    
    public List<QuestionCriteria> QuestionCriterias { get; set; } = [];
    
}