using DataAccess.Entities;

namespace BusinessDomain.Contracts;

public interface IApplicationUserManager
{
    ApplicationUser? GetUserById(string id);
    void AddUser(ApplicationUser user);
    void UpdateUser(ApplicationUser user);
    void DeleteUser(ApplicationUser user);
    IQueryable<ApplicationUser> GetAllUsers();
}