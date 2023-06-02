using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.QuizResults
{
    public class QuizResultForCreationDTO
    {
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int CorrectAnswers { get; set; }

    }
}
