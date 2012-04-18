using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FluentAutomation.Node
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class BindingSignatureAttribute : Attribute
    {
        private readonly BindingType? bindingType = null;
        private readonly string bindingName = null;
        private readonly string[] arguments = null;

        internal BindingSignatureAttribute(BindingType bindingType, string bindingName, params string[] arguments)
            : base()
        {
            this.bindingType = bindingType;
            this.bindingName = bindingName;
            this.arguments = arguments;
        }

        internal bool IsMatch(JToken action)
        {
            var matchingKeys = 0;
            var bindingKey = this.bindingType == BindingType.Expect ? action["Expect"] : action["Action"];

            if (bindingKey != null && bindingKey.ToString() == this.bindingName)
            {
                foreach (var argument in this.arguments)
                {
                    var item = action[argument.ToString()];
                    if (item != null) matchingKeys++;
                }

                return matchingKeys == this.arguments.Length;
            }

            return false;
        }
    }
}
