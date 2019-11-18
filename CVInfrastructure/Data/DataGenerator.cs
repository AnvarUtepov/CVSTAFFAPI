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
                        Id = 1,
                        NameRu = "Узбек",
                        NameUz = "Узбек",
                        NameUzlat = "O'zbek",  
                        Active=true
                    },
                    new SPNation{
                        Id = 2,
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
                        Id = 1,
                        NameRu = "Средняя школа",
                        NameUz = "Урта мактаб",
                        NameUzlat = "",  
                        Active=true
                    },
                    new SPEducation
                    {
                        Id = 2,
                        NameRu = "Средняя специальное образование(коледж)",
                        NameUz = "Урта махсус",
                        NameUzlat = "",  
                        Active=true
                    },
                    new SPEducation
                    {
                        Id = 3,
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

                
            }
        }
    } 
}
