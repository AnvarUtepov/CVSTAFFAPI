using CVCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVInfrastructure.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CVStaffDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<CVStaffDBContext>>()))
            {
                // Look for any board games.
                if (!context.SPNations.Any())
                {
                    context.SPNations.AddRange(
                    new SPNation
                    {                        
                        NameRu = "Узбек",
                        NameUz = "Узбек",
                        NameUzlat = "O'zbek",  
                        Active=true
                    },
                    new SPNation{                        
                        NameRu = "Русский",
                        NameUz = "Рус",
                        NameUzlat = "Rus",                        
                        Active=true
                    }
                    ); 
                    context.SaveChanges();    
                }
                if (!context.SPEducations.Any())
                {
                    context.SPEducations.AddRange(
                    new SPEducation
                    {
                        
                        NameRu = "Средняя школа",
                        NameUz = "Урта мактаб",
                        NameUzlat = "",  
                        Active=true
                    },
                    new SPEducation
                    {
                        
                        NameRu = "Средняя специальное образование(коледж)",
                        NameUz = "Урта махсус",
                        NameUzlat = "",  
                        Active=true
                    },
                    new SPEducation
                    {
                        
                        NameRu = "Высшее образование",
                        NameUz = "Олий",
                        NameUzlat = "",  
                        Active=true
                    }                                       
                    ); 
                    context.SaveChanges();    
                }
                if (!context.Users.Any())
                {
                      var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                      ApplicationUser user = new ApplicationUser()
                        {
                            Email = "test@gmail.com",
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = "test123",
                            FIO="Игорь Игоревич"
                        };
                      userManager.CreateAsync(user, "4297f44b13955235245b2497399d7a93");
                }
                if (!context.Staffs.Any())
                {
                    context.Staffs.AddRange(new Staff
                    {
                        //Id=1,
                        FIO="Петров Петр Петрович",
                        BirthDate=new DateTime(2000,10,15),
                        SPNationId=1,
                        Phone="998911351236",
                        Email="info@info.uz",
                        Active=true,                        
                    });
                    context.Jobs.AddRange(new Job{ 
                        Place="УзСофт",
                        YearOfBegin="2015",
                        YearOfEnd="-",
                        StaffId=1
                    });
                    context.Educations.AddRange(new Education{ 
                         Place="ТУИТ",
                         YearOfDone="2007",
                         SPEducationId=1,
                         StaffId=1
                    });
                    context.SaveChanges();
                }
                
            }
        }
    } 
}
