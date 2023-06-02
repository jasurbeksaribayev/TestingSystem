using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.Attachments
{
    public class AttachmentForCreationDTO
    {
        public string Name { get; set; }
        public int QuestionId { get; set; }
        public Stream Stream { get; set; }
    }
}
