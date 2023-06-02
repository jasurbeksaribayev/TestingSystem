using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.Quetions
{
    public class QuestionForUpdateDTO
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
