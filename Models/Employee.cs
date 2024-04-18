using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
        
    }
}