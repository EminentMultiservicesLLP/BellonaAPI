using BellonaAPI.Models;
using System;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IUserMenuRightsRepository
    {
        IEnumerable<MenuUserRightsModel> GetAllUserMenRights(Guid userId);
        UserInfo VerifyLogin(string loginId, string password);
        UserInfo GetForgotPassword(string loginId);
        bool SaveMenuSettings(UserInfo userInfo);
    }
}
