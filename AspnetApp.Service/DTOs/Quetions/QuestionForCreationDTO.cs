﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.Quetions
{
    public class QuestionForCreationDTO
    {
        public int Number { get; set; }
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
