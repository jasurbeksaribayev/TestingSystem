using AspnetApp.Domain.Commons;

namespace AspnetApp.Domain.Entities
{
    public class Attachment : Auditable
    {
        public string Path { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
