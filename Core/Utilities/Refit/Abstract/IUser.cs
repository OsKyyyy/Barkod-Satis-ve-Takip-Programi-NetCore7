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

		[Post("/auth/register")]
        Task<DataResult<Register>> Register([Header("Authorization")] string token, [Body] RegisterRequest registerRequest);

        [Get("/user/List")]
        Task<DataResult<List<UserInfo>>> List([Header("Authorization")] string token);

		[Get("/user/ListByMail")]
        Task<DataResult<UserInfo>> ListByMail([Header("Authorization")] string token, [AliasAs("email")] string email);

        [Get("/user/ListById")]
        Task<DataResult<UserInfo>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);

    }
}
