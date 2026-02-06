namespace backend.models;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public List<TeacherClass> TeacherClasses { get; set; } = [];
    
    public List<QuestionBank> QuestionBanks { get; set; } = [];
}