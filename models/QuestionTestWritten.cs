namespace backend.models;

public class QuestionTestWritten
{
    public int Id { get; set; }
    
    public double Weight { get; set; }
    public int Lines { get; set; }
    
    # region TestWritten Foreign Key
    public int TestWrittenId { get; set; }
    public TestWritten TestWritten { get; set; }
    # endregion
    
    # region Question Foreign Key
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    # endregion
    
    public List<Answer> Answers { get; set; } = [];
    
}