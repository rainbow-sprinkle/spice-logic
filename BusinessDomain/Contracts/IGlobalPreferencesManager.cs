using DataAccess.Entities;

namespace BusinessDomain.Contracts;

public interface IGlobalPreferencesManager
{
    IQueryable<GlobalPreferences> GetAllGlobalPreferences();
    Task<bool> SaveGlobalPreferences(GlobalPreferences preferences);
    Task<bool> DeleteGlobalPreferences(long id);
    Task<bool> AddGlobalPreference(GlobalPreferences preferences);
    GlobalPreferences? GetGlobalPreferences();
    void UpdateGlobalPreferences(GlobalPreferences preferences);
    GlobalPreferences? GetGlobalPreferencesByUserId(string userId);
    Task<bool> UpdateGlobalPreferencesByUserId(string userId, GlobalPreferences preferences);
}