using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public abstract class BaseCommandProvider
    {
        protected bool ExecuteImmediate = true;
        protected List<Action> StoredActions = new List<Action>();

        public void Act(Action action)
        {
            if (this.ExecuteImmediate)
            {
                action();
            }
            else
            {
                this.StoredActions.Add(action);
            }
        }

        public void ExecuteDeferredActions()
        {
            this.StoredActions.ForEach(action => action());
        }
    }
}
