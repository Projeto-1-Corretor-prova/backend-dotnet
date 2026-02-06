namespace backend.models;

public class QuestionBank
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    # region Teacher Foreign Key
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    # endregion
    
    public List<Question> Questions { get; set; } = [];
}