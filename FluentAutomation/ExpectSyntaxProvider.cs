using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class ExpectSyntaxProvider : BaseExpectSyntaxProvider
    {
        public ExpectSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider)
            : base(commandProvider, expectProvider)
        {
        }

        #region Count
        public ExpectCountSyntaxProvider Count(int count)
        {
            return new ExpectCountSyntaxProvider(this.commandProvider, this.expectProvider, count);
        }

        public class ExpectCountSyntaxProvider : BaseExpectSyntaxProvider
        {
            private readonly int count = 0;

            public ExpectCountSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, int count) 
                : base(commandProvider, expectProvider)
            {
                this.count = count;
            }

            public void Of(string selector)
            {
                this.expectProvider.Count(selector, this.count);
            }

            public void Of(Func<IEnumerable<IElement>> elements)
            {
                this.expectProvider.Count(elements, this.count);
            }
        }
        #endregion

        #region CSS Class
        public ExpectClassSyntaxProvider Class(string className)
        {
            return new ExpectClassSyntaxProvider(this.commandProvider, this.expectProvider, className);
        }

        public class ExpectClassSyntaxProvider : BaseExpectSyntaxProvider
        {
            private readonly string className = null;

            public ExpectClassSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, string className)
                : base(commandProvider, expectProvider)
            {
                this.className = className;
            }

            public void On(string selector)
            {
                this.expectProvider.CssClass(selector, this.className);
            }

            public void On(Func<IElement> element)
            {
                this.expectProvider.CssClass(element, this.className);
            }
        }
        #endregion

        #region Text
        public ExpectTextSyntaxProvider Text(string text)
        {
            return new ExpectTextSyntaxProvider(this.commandProvider, this.expectProvider, text);
        }

        public ExpectTextSyntaxProvider Text(Expression<Func<string, bool>> matchFunc)
        {
            return new ExpectTextSyntaxProvider(this.commandProvider, this.expectProvider, matchFunc);
        }

        public class ExpectTextSyntaxProvider : BaseExpectSyntaxProvider
        {
            private readonly string text = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public ExpectTextSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, string text) 
                : base(commandProvider, expectProvider)
            {
                this.text = text;
            }

            public ExpectTextSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, Expression<Func<string, bool>> matchFunc)
                : base(commandProvider, expectProvider)
            {
                this.matchFunc = matchFunc;
            }

            public void In(string selector)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    this.expectProvider.Text(selector, this.text);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Text(selector, this.matchFunc);
                }
            }

            public void In(Func<IElement> element)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    this.expectProvider.Text(element, this.text);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Text(element, this.matchFunc);
                }
            }
        }
        #endregion

        #region Value
        public ExpectValueSyntaxProvider Value(int value)
        {
            return this.Value(value.ToString());
        }

        public ExpectValueSyntaxProvider Value(string value)
        {
            return new ExpectValueSyntaxProvider(this.commandProvider, this.expectProvider, value);
        }

        public ExpectValueSyntaxProvider Value(Expression<Func<string, bool>> matchFunc)
        {
            return new ExpectValueSyntaxProvider(this.commandProvider, this.expectProvider, matchFunc);
        }

        public class ExpectValueSyntaxProvider : BaseExpectSyntaxProvider
        {
            private readonly string value = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public ExpectValueSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, string value)
                : base(commandProvider, expectProvider)
            {
                this.value = value;
            }

            public ExpectValueSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, Expression<Func<string, bool>> matchFunc)
                : base(commandProvider, expectProvider)
            {
                this.matchFunc = matchFunc;
            }

            public void In(string selector)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    this.expectProvider.Value(selector, this.value);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Value(selector, this.matchFunc);
                }
            }

            public void In(Func<IElement> element)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    this.expectProvider.Value(element, this.value);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Value(element, this.matchFunc);
                }
            }
        }
        #endregion

        #region Url
        public void Url(string expectedUrl)
        {
            Url(new Uri(expectedUrl, UriKind.Absolute));
        }

        public void Url(Uri expectedUrl)
        {
            this.expectProvider.Url(expectedUrl);
        }

        public void Url(Expression<Func<Uri, bool>> urlExpression)
        {
            this.expectProvider.Url(urlExpression);
        }
        #endregion

        #region Boolean / Throws
        public void True(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(() =>
            {
                this.expectProvider.True(matchFunc);
            });
        }

        public void False(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(() =>
            {
                this.expectProvider.False(matchFunc);
            });
        }

        public void Throws(Expression<Action> matchAction)
        {
            this.commandProvider.Act(() =>
            {
                this.expectProvider.Throws(matchAction);
            });
        }
        #endregion
    }

    public class BaseExpectSyntaxProvider
    {
        internal readonly ICommandProvider commandProvider = null;
        internal readonly IExpectProvider expectProvider = null;

        public BaseExpectSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider)
        {
            this.commandProvider = commandProvider;
            this.expectProvider = expectProvider;
        }
    }
}
