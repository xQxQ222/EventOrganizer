using ModelHolder.Context;
using ModelHolder.Exceptions;

namespace EventManager.Utility
{
    public class HelperMethods
    {
        private readonly EventManagerDbContext dbContext;

        public HelperMethods(EventManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CheckAdminRights(long userId)
        {
            var user = dbContext.Users.Find(userId);
            VerifyUserExistence(userId);
            if(user.RoleId != 1)
            {
                throw new ForbiddenException();
            }
        }

        public void VerifyUserExistence(long userId)
        {
            var user = dbContext.Users.Find(userId);
            if(user == null)
            {
                throw new NotRegistratedUserException(userId);
            }
        }
    }
}
