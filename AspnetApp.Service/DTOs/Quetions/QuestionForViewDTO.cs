using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Answers;
using AspnetApp.Service.DTOs.Attachments;

namespace AspnetApp.Service.DTOs.Quetions
{
    public class QuestionForViewDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<AttachmentForViewDTO> Attachments { get; set; }
        public ICollection<AnswerForViewDTO> Answers { get; set; }
    }
}
