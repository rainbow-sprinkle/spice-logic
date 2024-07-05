using DataAccess.Entities;

namespace BusinessDomain.Contracts;

public interface IOrganizationManager
{
    Organization? GetOrganizationById(long id);
    void AddOrganization(Organization organization);
    void UpdateOrganization(Organization organization);
    void DeleteOrganization(Organization organization);
    ICollection<ApplicationUser> GetOrganizationMembers(long organizationId);
    ICollection<SerializedProject> GetOrganizationProjects(long organizationId);
    ICollection<SerializedProject> GetOrganizationProjectsByName(string organizationName);
    IQueryable<Organization> SearchOrganizations(string keyword);
    IQueryable<SerializedProject> GetOrganizationProjectsByMember(string memberId);
    IQueryable<Organization> GetOrganizationsByMember(string memberId);
    IQueryable<Organization> GetOrganizationsByProject(Guid projectId);
    List<Organization> GetOrganizationsBySuperAdmin(string superAdminUserName);
    IQueryable<Organization> GetAllOrganizations();
    void CreateOrganizationWithMember(ApplicationUser member, string organizationName);
    Task InviteMemberToOrganization(string emailAddress, long organizationId);
}