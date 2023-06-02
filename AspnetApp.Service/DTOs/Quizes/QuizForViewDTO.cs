using AspnetApp.Service.DTOs.Quetions;
using AspnetApp.Service.DTOs.QuizResults;

namespace AspnetApp.Service.DTOs.Quizes
{
    public class QuizForViewDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public ICollection<QuizResultForViewDTO> QuizRezults { get; set; }
        public ICollection<QuestionForViewDTO> Questions { get; set; }
    }
}
