using CVCore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.Interfaces
{
    public interface IJwtToken    
    {        
         string GenerateJwtToken(ApplicationUser user, IConfiguration configuration,IList<string> roles);
    }
}
