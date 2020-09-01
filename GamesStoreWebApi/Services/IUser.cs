using GamesStoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Services
{
    public interface IUser
    {
        User Authenticate(string username, string password);
    }
}
