using AspnetApp.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Domain.Entities
{
    public class Course : Auditable
    {
        public string Name { get; set; }
        public ICollection<Quiz> Quizes { get; set; }
    }
}
