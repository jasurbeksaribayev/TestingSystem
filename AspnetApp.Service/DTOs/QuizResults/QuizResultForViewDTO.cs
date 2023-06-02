using AspnetApp.Service.DTOs.Quizes;
using AspnetApp.Service.DTOs.SolvedQuestions;
using AspnetApp.Service.DTOs.Users;

namespace AspnetApp.Service.DTOs.QuizResults
{
    public class QuizResultForViewDTO
    {
        public int Id { get; set; }
        public int CorrectAnswers { get; set; }
        public QuizForViewDTO Quiz { get; set; }
        public UserForViewDTO User { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public ICollection<SolvedQuestionForViewDTO> SolvedQuestions { get; set; }

    }
}
