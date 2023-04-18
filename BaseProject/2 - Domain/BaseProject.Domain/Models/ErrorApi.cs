using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Models
{
    public class ErrorApi
    {
        public string Message { get; set; }
        public List<string> Erros { get; set; }
    }
}
