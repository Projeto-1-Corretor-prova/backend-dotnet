using AutoMapper;
using backend.dtos.answer;
using backend.dtos.correction;
using backend.dtos.question;
using backend.dtos.questionBank;
using backend.dtos.questionCriteria;
using backend.dtos.questionTestWritten;
using backend.dtos.student;
using backend.dtos.teacher;
using backend.dtos.teacherClass;
using backend.dtos.testWritten;
using backend.models;

namespace backend;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TeacherCreateDto, Teacher>();
        CreateMap<Teacher, TeacherDto>();
        
        CreateMap<TeacherClass, TeacherClassMiniDto>();
        CreateMap<TeacherClass, TeacherClassDto>();
        CreateMap<TeacherClassCreateDto, TeacherClass>();
        CreateMap<TeacherClassUpdateDto, TeacherClass>();
        
        CreateMap<Student, StudentDto>();
        CreateMap<Student, StudentMiniDto>();
        CreateMap<StudentCreateDto, Student>();
        CreateMap<StudentUpdateDto, Student>();
        
        CreateMap<TestWritten, TestWrittenMiniDto>();
        CreateMap<TestWritten, TestWrittenDto>();
        CreateMap<TestWrittenCreateDto, TestWritten>();
        CreateMap<TestWrittenUpdateDto, TestWritten>();
        
        CreateMap<QuestionTestWritten, QuestionTestWrittenDto>();
        CreateMap<QuestionTestWrittenCreateDto, QuestionTestWritten>();
        CreateMap<QuestionTestWrittenUpdateDto, QuestionTestWritten>();
        
        CreateMap<QuestionBank, QuestionBankDto>();
        CreateMap<QuestionBank, QuestionBankMiniDto>();
        CreateMap<QuestionBankCreateDto, QuestionBank>();
        CreateMap<QuestionBankUpdateDto, QuestionBank>();

        CreateMap<Question, QuestionDto>();
        CreateMap<Question, QuestionMiniDto>();
        CreateMap<QuestionCreateDto, Question>();
        CreateMap<QuestionUpdateDto, Question>();

        CreateMap<QuestionCriteria, QuestionCriteriaDto>();
        CreateMap<QuestionCriteriaCreateDto, QuestionCriteria>();
        CreateMap<QuestionCriteriaUpdateDto, QuestionCriteria>();
        
        CreateMap<Correction, CorrectionDto>();
        CreateMap<Correction, CorrectionMiniDto>();
        
        CreateMap<Answer, AnswerDto>();
        CreateMap<AnswerUpdateDto, Answer>();
        
    }
}