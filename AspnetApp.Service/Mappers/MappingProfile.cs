using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Answers;
using AspnetApp.Service.DTOs.Attachments;
using AspnetApp.Service.DTOs.Courses;
using AspnetApp.Service.DTOs.Quetions;
using AspnetApp.Service.DTOs.Quizes;
using AspnetApp.Service.DTOs.QuizResults;
using AspnetApp.Service.DTOs.SolvedQuestions;
using AspnetApp.Service.DTOs.Users;
using AutoMapper;

namespace AspnetApp.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users
            CreateMap<User, UserForCreationDTO>().ReverseMap();
            CreateMap<User, UserForUpdateDTO>().ReverseMap();
            CreateMap<User, UserForViewDTO>().ReverseMap();

            // Courses
            CreateMap<Course, CourseForCreationDTO>().ReverseMap();
            CreateMap<Course, CourseForUpdateDTO>().ReverseMap();
            CreateMap<Course, CourseForViewDTO>().ReverseMap();

            // Quizzes
            CreateMap<Quiz, QuizForCreationDTO>().ReverseMap();
            CreateMap<Quiz, QuizForViewDTO>().ReverseMap();

            // Questions
            CreateMap<Question, QuestionForCreationDTO>().ReverseMap();
            CreateMap<Question, QuestionForUpdateDTO>().ReverseMap();
            CreateMap<Question, QuestionForViewDTO>().ReverseMap();

            // Attachments
            CreateMap<Attachment, AttachmentForCreationDTO>().ReverseMap();
            CreateMap<Attachment, AttachmentForViewDTO>().ReverseMap();

            // Answers
            CreateMap<Answer, AnswerForCreationDTO>().ReverseMap();
            CreateMap<Answer, AnswerForUpdateDTO>().ReverseMap();
            CreateMap<Answer, AnswerForViewDTO>().ReverseMap();

            // SolvedQuestions
            CreateMap<SolvedQuestion, SolvedQuestionForCreationDTO>().ReverseMap();
            CreateMap<SolvedQuestion, SolvedQuestionForViewDTO>().ReverseMap();
            CreateMap<SolvedQuestion, SolvedQuestionForUpdateDTO>().ReverseMap();


            // QuizResults
            CreateMap<QuizRezult, QuizResultForCreationDTO>().ReverseMap();
            CreateMap<QuizRezult, QuizResultForViewDTO>().ReverseMap();
        }
    }
}
