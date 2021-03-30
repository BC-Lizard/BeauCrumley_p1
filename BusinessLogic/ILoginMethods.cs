using BusinessLogic.Models;

namespace BusinessLogic
{
    public interface ILoginMethods
    {
        bool CheckCreds(IAUser user, string hashedPassword);
    }
}