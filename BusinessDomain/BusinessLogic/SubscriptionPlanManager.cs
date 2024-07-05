using BusinessDomain.Contracts;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessDomain.BusinessLogic;

public class SubscriptionPlanManager(IDbContextFactory<ApplicationDbContext> contextFactory) : ISubscriptionPlanManager
{
    public async Task<SubscriptionPlan?> GetSubscriptionPlanById(long id)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        return await context.SubscriptionPlans.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddSubscriptionPlan(SubscriptionPlan subscriptionPlan)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        context.SubscriptionPlans.Add(subscriptionPlan);
        await context.SaveChangesAsync();
    }

    public async Task UpdateSubscriptionPlan(SubscriptionPlan subscriptionPlan)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        context.Attach(subscriptionPlan).State = EntityState.Modified;
        //context.SubscriptionPlans.Update(subscriptionPlan);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!SubscriptionPlanExists(subscriptionPlan.Id))
            {
                throw new Exception($"Subscription Plan Not Found with id {subscriptionPlan.Id}", ex);
            }

            throw new Exception("Concurrency Exception!", ex);
        }

        bool SubscriptionPlanExists(long id)
        {
            return context.SubscriptionPlans.Any(e => e.Id == id);
        }
    }

    public Task DeleteSubscriptionPlan(long id)
    {
        return DeleteSubscriptionPlan(new SubscriptionPlan { Id = id });
    }
    
    public async Task DeleteSubscriptionPlan(SubscriptionPlan subscriptionPlan)
    {
        ApplicationDbContext context = await contextFactory.CreateDbContextAsync();
        context.SubscriptionPlans.Remove(subscriptionPlan);
        await context.SaveChangesAsync();
    }

    public IQueryable<SubscriptionPlan> GetAllSubscriptionPlans()
    {
        ApplicationDbContext context = contextFactory.CreateDbContext();
        return context.SubscriptionPlans;
    }
}