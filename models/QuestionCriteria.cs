namespace backend.models;

using backend.enums;

public class QuestionCriteria
{
    public int Id { get; set; }
    
    public string Criteria { get; set; }
    public CritereaEnum Type { get; set; }

    #region Question Foreign Key
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    #endregion
}