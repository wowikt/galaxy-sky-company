using Abp.Authorization;
using GalaxySkyCompany.Authorization.Roles;
using GalaxySkyCompany.Authorization.Users;

namespace GalaxySkyCompany.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
