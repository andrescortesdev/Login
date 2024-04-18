using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Models;
using Microsoft.EntityFrameworkCore;

namespace Login.Data
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Register> Registers { get; set; }
        
    }
}