using BusinessDomain.Contracts;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessDomain.BusinessLogic;

public class OrganizationManager(IDbContextFactory<ApplicationDbContext> contextFactory) : IOrganizationManager
{
    public Organization? GetOrganizationById(long id)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Organizations.FirstOrDefault(o => o.Id == id);
    }

    public void AddOrganization(Organization organization)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        context.Organizations.Add(organization);
        context.SaveChanges();
    }

    public void UpdateOrganization(Organization organization)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        context.Organizations.Update(organization);
        context.SaveChanges();
    }

    public void DeleteOrganization(Organization organization)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        context.Organizations.Remove(organization);
        context.SaveChanges();
    }

    public ICollection<ApplicationUser> GetOrganizationMembers(long organizationId)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        Organization? organization = context.Organizations.Include(organization => organization.Members).FirstOrDefault(o => o.Id == organizationId);
        if (organization != null)
        {
            return organization.Members;
        }
        return [];
    }

    public ICollection<SerializedProject> GetOrganizationProjects(long organizationId)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        Organization? organization = context.Organizations.Include(organization => organization.Projects).FirstOrDefault(o => o.Id == organizationId);
        if (organization != null)
        {
            return organization.Projects;
        }
        return [];
    }

    public ICollection<SerializedProject> GetOrganizationProjectsByName(string organizationName)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        Organization? organization = context.Organizations.Include(organization => organization.Projects).FirstOrDefault(o => o.Name == organizationName);
        if (organization != null)
        {
            return organization.Projects;
        }
        return [];
    }

    public IQueryable<Organization> SearchOrganizations(string keyword)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Organizations.Where(o => o.Name.Contains(keyword));
    }

    public IQueryable<SerializedProject> GetOrganizationProjectsByMember(string memberId)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Organizations.Where(o => o.Members.Any(m => m.Id == memberId)).SelectMany(o => o.Projects);
    }

    public IQueryable<Organization> GetOrganizationsByMember(string memberId)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Organizations.Where(o => o.Members.Any(m => m.Id == memberId));
    }

    public IQueryable<Organization> GetOrganizationsByProject(Guid projectId)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Organizations.Where(o => o.Projects.Any(p => p.Id == projectId));
    }

    public List<Organization> GetOrganizationsBySuperAdmin(string superAdminUserName)
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return [.. context.Organizations.Where(o => o.SuperAdminUserName == superAdminUserName)];
    }

    public IQueryable<Organization> GetAllOrganizations()
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.Organizations;
    }

    public void CreateOrganizationWithMember(ApplicationUser member, string organizationName)
    {
        using ApplicationDbContext context = contextFactory.CreateDbContext();
        // Add the new member
        context.ApplicationUsers.Add(member);
        // Create a new organization
        Organization organization = new() { Name = organizationName, SuperAdminUserName = member.UserName };
        context.Organizations.Add(organization);
        context.SaveChanges();
    }

    // Invite a member with email address to join an organization
    public async Task InviteMemberToOrganization(string emailAddress, long organizationId)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        Organization? organization = await context.Organizations.FirstOrDefaultAsync(o => o.Id == organizationId);
        if (organization != null)
        {
            // Send invitation email to the provided email address
            // ...
        }
    }
}