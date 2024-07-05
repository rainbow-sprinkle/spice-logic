using BusinessDomain.Contracts;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessDomain.BusinessLogic;

public class GlobalPreferencesManager(IDbContextFactory<ApplicationDbContext> contextFactory)
    : IGlobalPreferencesManager
{
    public IQueryable<GlobalPreferences> GetAllGlobalPreferences()
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.GlobalPreferences;
    }

    public async Task<bool> SaveGlobalPreferences(GlobalPreferences preferences)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        context.GlobalPreferences.Update(preferences);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteGlobalPreferences(long id)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        GlobalPreferences? preferences = context.GlobalPreferences.FirstOrDefault(o => o.Id == id);
        if (preferences == null)
        {
            return false;
        }
        context.GlobalPreferences.Remove(preferences);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddGlobalPreference(GlobalPreferences preferences)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        context.GlobalPreferences.Add(preferences);
        await context.SaveChangesAsync();
        return true;
    }

    public GlobalPreferences? GetGlobalPreferences()
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.GlobalPreferences.FirstOrDefault();
    }

    public void UpdateGlobalPreferences(GlobalPreferences preferences)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        context.GlobalPreferences.Update(preferences);
        context.SaveChanges();
    }

    public GlobalPreferences? GetGlobalPreferencesByUserId(string userId)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.GlobalPreferences.FirstOrDefault(o => o.User.Id == userId);
    }

    public async Task<bool> UpdateGlobalPreferencesByUserId(string userId, GlobalPreferences preferences)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        GlobalPreferences? existingPreferences = context.GlobalPreferences.FirstOrDefault(o => o.User.Id == userId);
        if (existingPreferences == null)
        {
            return false;
        }
        existingPreferences.User = preferences.User;
        await context.SaveChangesAsync();
        return true;
    }
}