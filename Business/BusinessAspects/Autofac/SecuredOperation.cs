using System.Collections.Generic;
using System.Security;
using Business.Constant;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Business.Abstract;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        //private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleDal _roleDal;
        private readonly string _pageName;

        public SecuredOperation(/*string roles, */string pageName)
        {
            //_roles = roles.Split(',');
            _pageName = pageName;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _roleDal = ServiceTool.ServiceProvider.GetService<IRoleDal>();
            if (_httpContextAccessor == null || _roleDal == null)
            {
                throw new Exception("Dependencies are not resolved properly.");
            }
        }

        protected override void OnBefore(IInvocation invocation)
        {
            //var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            //foreach (var role in _roles)
            //{
            //    if (roleClaims.Contains(role))
            //    {
            //        return;
            //    }
            //}

            //throw new SecurityException(Messages.AuthorizationDenied);

            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            if (!_roleDal.HasAccessToPage(userId, _pageName))
            {
                throw new SecurityException(Messages.AuthorizationDenied);
            }
        }
    }

    //public class SecuredOperation : MethodInterception
    //{
    //    private IHttpContextAccessor _httpContextAccessor;
    //    private string _pageName;

    //    public SecuredOperation(string pageName)
    //    {
    //        _pageName = pageName;
    //        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    //    }

    //    protected override void OnBefore(IInvocation invocation)
    //    {
    //        var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

    //        using (var context = new YourDbContext())
    //        {
    //            var operationClaimIds = context.PageClaims
    //                .Where(pc => pc.PageName == _pageName)
    //                .Select(pc => pc.OperationClaimId)
    //                .ToList();

    //            var userOperationClaims = context.UserOperationClaims
    //                .Where(uoc => uoc.UserId == _httpContextAccessor.HttpContext.User.GetUserId())
    //                .Select(uoc => uoc.OperationClaimId)
    //                .ToList();

    //            if (!operationClaimIds.Intersect(userOperationClaims).Any())
    //            {
    //                throw new SecurityException(Messages.AuthorizationDenied);
    //            }
    //        }
    //    }
    //}

}