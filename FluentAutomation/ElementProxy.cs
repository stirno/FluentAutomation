using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class ElementProxy
    {
        /// <summary>
        /// Base constructor should not be executed by user-code. Exists for framework use only.
        /// </summary>
        public ElementProxy()
        {
        }

        /// <summary>
        /// Representation of an element tied to a specific command provider/browser instance.
        /// </summary>
        /// <param name="commandProvider"></param>
        /// <param name="element"></param>
        public ElementProxy(ICommandProvider commandProvider, Func<IElement> element)
        {
            this.Elements.Add(commandProvider, element);
        }

        public List<Func<ElementProxy>> Children { get; set; }

        private Dictionary<ICommandProvider, Func<IElement>> elements = new Dictionary<ICommandProvider, Func<IElement>>();

        /// <summary>
        /// Representation of an element across command providers, wrapped for lazy loading.
        /// </summary>
        public Dictionary<ICommandProvider, Func<IElement>> Elements
        {
            get
            {
                // if a FindMultiple result has been passed in, we need to 'elevate' its items first
                if (this.Children != null)
                {
                    foreach (var proxy in this.Children)
                    {
                        foreach (var element in proxy().Elements)
                        {
                            this.elements.Add(element.Key, element.Value);
                        }
                    }

                    this.Children = null;
                }

                return this.elements;
            }
        }

        public IElement Element
        {
            get
            {
                return this.Elements.First().Value();
            }
        }
    }
}
