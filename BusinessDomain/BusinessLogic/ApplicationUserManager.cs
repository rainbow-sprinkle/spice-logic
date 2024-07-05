using BusinessDomain.Contracts;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessDomain.BusinessLogic;

public class ApplicationUserManager(IDbContextFactory<ApplicationDbContext> contextFactory) : IApplicationUserManager
{
    public ApplicationUser? GetUserById(string id)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Users.FirstOrDefault(o => o.Id == id);
    }

    public void AddUser(ApplicationUser user)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        context.Users.Add(user);
        context.SaveChanges();
    }

    public void UpdateUser(ApplicationUser user)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        context.Users.Update(user);
        context.SaveChanges();
    }

    public void DeleteUser(ApplicationUser user)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        context.Users.Remove(user);
        context.SaveChanges();
    }

    public IQueryable<ApplicationUser> GetAllUsers()
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Users;
    }

    public ApplicationUser? GetUserByUsername(string username)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Users.FirstOrDefault(o => o.UserName == username);
    }

    public ApplicationUser? GetUserByEmail(string email)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Users.FirstOrDefault(o => o.Email == email);
    }
}