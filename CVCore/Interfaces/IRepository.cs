using CVCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CVCore.Interfaces
{
     public interface IRepository
    {
        DbSet<SPNation> SPNations { get; set; }
        DbSet<SPEducation> SPEducations { get; set; }     
        DbSet<Education> Educations { get; set; }
        DbSet<Job> Jobs { get; set; }
        DbSet<Staff> Staffs { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
