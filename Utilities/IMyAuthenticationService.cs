
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtTokens.Utilities
{
    public interface IMyAuthenticationService
    {
        bool isAuthenticated(UserPassword request, out string token);
        
    }
}
