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

        public void In(string fieldSelector)
        {
            In(fieldSelector, MatchConditions.None);
        }

        public void In(string fieldSelector, MatchConditions conditions)
        {
            _matchConditions = conditions;

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

        public void In(MatchConditions conditions, params string[] fieldSelectors)
        {
            _matchConditions = conditions;
            In(fieldSelectors);
        }

        public void In(params string[] fieldSelectors)
        {
            foreach (var fieldSelector in fieldSelectors)
            {
                In(fieldSelector, _matchConditions);
            }
        }
    }
}
