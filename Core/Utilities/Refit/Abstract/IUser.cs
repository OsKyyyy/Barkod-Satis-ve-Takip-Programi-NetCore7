using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.User;
using Core.Utilities.Refit.Models.Request.User;
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
        [Post("/auth/Login")]
        Task<DataResult<LoginModel>> Login([Body] LoginRequestModel loginRequest);

		[Post("/auth/Register")]
        Task<DataResult<RegisterModel>> Register([Header("Authorization")] string token, [Body] RegisterRequestModel registerRequest);

        [Put("/user/Update")]
        Task<DataResult<UpdateModel>> Update([Header("Authorization")] string token, [Body] UpdateRequestModel editRequestModel);

        [Delete("/user/Delete")]
        Task<DataResult<ViewModel>> Delete([Header("Authorization")] string token, int id);

        [Get("/user/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token);

        [Get("/user/ListByMail")]
        Task<DataResult<ViewModel>> ListByMail([Header("Authorization")] string token, [AliasAs("email")] string email);

        [Get("/user/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);

        [Post("/user/AddRole")]
        Task<Result> AddRole([Header("Authorization")] string token, [Body] AddRoleRequestModel addRoleRequestModel);

        [Put("/user/UpdateRole")]
        Task<Result> UpdateRole([Header("Authorization")] string token, [Body] UpdateRoleRequestModel updateRoleRequestModel);

        [Get("/user/RoleList")]
        Task<DataResult<List<RoleListViewModel>>> RoleList([Header("Authorization")] string token);

        [Get("/user/GetRoleById")]
        Task<DataResult<RoleListViewModel>> GetRoleById([Header("Authorization")] string token, int id);

        [Get("/user/GetRoleByName")]
        Task<DataResult<RoleListViewModel>> GetRoleByName([Header("Authorization")] string token, string name);
    }
}
