using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Models
{
    public class Register
    {
        public int Id { get; set; }
        public DateTime Entries { get; set; }
        public DateTime? Exits { get; set; } 
        public int? EmployeeId { get; set; } 
        
    }
}