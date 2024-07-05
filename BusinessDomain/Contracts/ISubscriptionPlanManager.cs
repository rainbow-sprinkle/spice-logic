using DataAccess.Entities;

namespace BusinessDomain.Contracts;

public interface ISubscriptionPlanManager
{
    Task<SubscriptionPlan?> GetSubscriptionPlanById(long id);
    Task AddSubscriptionPlan(SubscriptionPlan subscriptionPlan);
    Task UpdateSubscriptionPlan(SubscriptionPlan subscriptionPlan);
    Task DeleteSubscriptionPlan(SubscriptionPlan subscriptionPlan);
    IQueryable<SubscriptionPlan> GetAllSubscriptionPlans();
    Task DeleteSubscriptionPlan(long id);
}