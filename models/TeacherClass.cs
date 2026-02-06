namespace backend.models;

public class TeacherClass
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    # region Teacher Foreign Key
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    # endregion
    
    public List<TestWritten> TestWrittens { get; set; } = [];
    
    public List<Student> Students { get; set; } = [];
}