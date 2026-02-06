namespace backend.models;

public class TestWritten
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    public string RegexQuestionIdentifier { get; set; }
    public double TotalWeight => QuestionTestWrittens.Sum(q => q.Weight);
    
    # region TeacherClass Foreign Key
    public int TeacherClassId { get; set; }
    public TeacherClass TeacherClass { get; set; }
    # endregion
    
    public List<Correction> Corrections { get; set; } = [];
    public List<QuestionTestWritten> QuestionTestWrittens { get; set; } = [];
}