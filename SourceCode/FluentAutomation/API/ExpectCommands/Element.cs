using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAutomation.API.Interfaces;
using FluentAutomation.API.Providers;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Exceptions;

namespace FluentAutomation.API.ExpectCommands
{
    /// <summary>
    /// Element Expects
    /// </summary>
    public class Element : CommandBase
    {
        private MatchConditions _matchConditions;
        private Expression<Func<IElementDetails, bool>> _elementExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="elementExpression">The element expression.</param>
        public Element(AutomationProvider provider, CommandManager manager, Expression<Func<IElementDetails, bool>> elementExpression)
            : base(provider, manager)
        {
            _elementExpression = elementExpression;
        }

        /// <summary>
        /// Ins the specified field selector.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        public void In(string fieldSelector)
        {
            In(fieldSelector, MatchConditions.None);
        }

        /// <summary>
        /// Ins the specified field selector.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        public void In(string fieldSelector, MatchConditions conditions)
        {
            _matchConditions = conditions;

            if (CommandManager.EnableRemoteExecution)
            {
                // args
                var arguments = new Dictionary<string, dynamic>();
                arguments.Add("selector", fieldSelector);
                if (_elementExpression != null) arguments.Add("expression", _elementExpression.ToExpressionString());

                CommandManager.RemoteCommands.Add(new RemoteCommands.RemoteCommandDetails()
                {
                    Name = "ExpectElement",
                    Arguments = arguments
                });
            }
            else
            {
                CommandManager.CurrentActionBucket.Add(() =>
                {
                    var element = Provider.GetElement(fieldSelector, conditions);

                    var compiledFunc = _elementExpression.Compile();
                    if (!compiledFunc(element))
                    {
                        Provider.TakeAssertExceptionScreenshot();
                        throw new AssertException("Element assertion failed. Expected element [{0}] to match expression [{1}].", fieldSelector, _elementExpression.ToExpressionString());
                    }
                });
            }
        }

        /// <summary>
        /// Ins the specified conditions.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="fieldSelectors">The field selectors.</param>
        public void In(MatchConditions conditions, params string[] fieldSelectors)
        {
            _matchConditions = conditions;
            In(fieldSelectors);
        }

        /// <summary>
        /// Ins the specified field selectors.
        /// </summary>
        /// <param name="fieldSelectors">The field selectors.</param>
        public void In(params string[] fieldSelectors)
        {
            foreach (var fieldSelector in fieldSelectors)
            {
                In(fieldSelector, _matchConditions);
            }
        }
    }
}
