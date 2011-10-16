using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API
{
    /// <summary>
    /// Action Bucket
    /// </summary>
    public class ActionBucket : List<Action>
    {
        private CommandManager _manager = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBucket"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public ActionBucket(CommandManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Adds the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public new void Add(Action action)
        {
            if (_manager.IsRecordReplay)
                base.Add(action);
            else
                action.Invoke();
        }
    }
}
