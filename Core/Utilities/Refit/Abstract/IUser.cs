using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Request;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Abstract
{
    public interface IUser
    {
        [Post("/auth/login")]
        Task<DataResult<Login>> Login([Body] LoginRequest loginRequest);

        [Get("/auth/GetInfoByMail")]
        Task<DataResult<UserInfo>> GetInfoByMail([AliasAs("email")] string email);
    }
}
