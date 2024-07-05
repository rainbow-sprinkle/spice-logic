using DecisionAnalysis.Web.Client.Components.DecisionModules.Ahp.Pages.Placeholder;
using System.Security.Cryptography;

namespace DecisionAnalysis.Web.Client.Components.Service
{
    public class AhpStateContainerService
    {
        public class Criteria
        {
            public string Name { get; set; }
            public List<Criteria> Children { get; set; }

            public Criteria(string name)
            {
                Name = name;
                Children = [];
            }

            public void AddChild(Criteria child)
            {
                Children.Add(child);
            }
        }
        
        public string Goal { get; set; }
        public List<Criteria> CriteriaDatas {  get; set; } = [];

        public event Action OnStateChange;
        public void SetGoal(string goal)
        {
            Goal = goal;
            NotifyStateChanged();
        }

        public void SetCriterias(Criteria criteria)
        {
            CriteriaDatas.Add(criteria);
            NotifyStateChanged();
        }

        public List<Tree.CriteriaData> TreeDatas { get; set; } = new List<Tree.CriteriaData>
        {
            new Tree.CriteriaData {
                Id = "1",
                Name = "Johnson",
                HasChild = true
            },
            new Tree.CriteriaData {
                Id = "2",
                Pid = "1",
                Name = "Sourav"
            }
        };

        
        

        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
