using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.Attachments
{
    public class AttachmentForViewDTO
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Path { get; set; }

    }
}
