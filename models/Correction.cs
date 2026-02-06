namespace backend.models;

public class Correction
{
    
    public int Id { get; set;  }
    
    public double Score;

    #region TestWritten Foreign Key
    public int TestWrittenId { get; set; }
    public TestWritten TestWritten { get; set; }
    #endregion
    
    #region Student Foreign Key
    public int StudentId { get; set; }
    public Student Student { get; set; }
    #endregion
    
    public List<Answer> Answers { get; set; } = [];
    
}