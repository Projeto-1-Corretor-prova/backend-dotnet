namespace backend.models;

public class Student
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Identifier { get; set; }
    
    #region TeacherClass Foreign Key
    public int TeacherClassId { get; set; }
    public TeacherClass TeacherClass { get; set; }
    #endregion
    
    public List<Correction> Corrections { get; set; } = [];
}