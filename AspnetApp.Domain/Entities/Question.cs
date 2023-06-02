using AspnetApp.Domain.Commons;

namespace AspnetApp.Domain.Entities
{
    public class Question : Auditable
    {
        public int Number { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<SolvedQuestion> SolvedQuestions { get; set; }
    }
}
