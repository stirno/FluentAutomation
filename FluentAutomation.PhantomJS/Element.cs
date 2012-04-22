using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Interfaces;
using Newtonsoft.Json.Linq;

namespace FluentAutomation
{
    public class Element : IElement
    {
        public Element(JObject data)
        {
            this.selector = data["Selector"].ToString();
            this.tagName = data["TagName"].ToString();
            this.value = data["Value"].ToString();
            this.text = data["Text"].ToString();
            this.posX = Convert.ToInt32(data["PosX"].ToString());
            this.posY = Convert.ToInt32(data["PosY"].ToString());
            this.width = Convert.ToInt32(data["Width"].ToString());
            this.height = Convert.ToInt32(data["Height"].ToString());

            this.attributes = new ElementAttributeSelector(data["Attributes"] as JArray);
        }

        private string selector = null;
        public string Selector
        {
            get { return this.selector; }
        }

        private string tagName = null;
        public string TagName
        {
            get { return this.tagName; }
        }

        private IElementAttributeSelector attributes = null;
        public IElementAttributeSelector Attributes
        {
            get { return this.attributes; }
        }

        private string value = null;
        public string Value
        {
            get { return this.value; }
        }

        private string text = null;
        public string Text
        {
            get { return this.text; }
        }

        public IEnumerable<string> SelectedOptionValues
        {
            get { return new List<string>() { }; }
        }

        public IEnumerable<string> SelectedOptionTextCollection
        {
            get { return new List<string>() { }; }
        }

        public bool IsText
        {
            get
            {
                bool isText = false;
                switch (this.TagName)
                {
                    case "input":
                        switch (this.Attributes.Get("type").ToLower())
                        {
                            case "text":
                            case "password":
                            case "hidden":
                                isText = true;
                                break;
                        }

                        break;
                    case "textarea":
                        isText = true;
                        break;
                }

                return isText;
            }
        }

        public bool IsSelect
        {
            get
            {
                return this.TagName.ToLower() == "select";
            }
        }

        public bool IsMultipleSelect
        {
            get
            {
                if (this.IsSelect)
                {
                    var multiAttr = this.Attributes.Get("multiple");
                    return multiAttr != null;
                }
                else
                {
                    return false;
                }
            }
        }

        private int posX = -1;
        public int PosX
        {
            get { return posX; }
        }

        private int posY = -1;
        public int PosY
        {
            get { return posY; }
        }

        private int width = -1;
        public int Width
        {
            get { return width; }
        }

        private int height = -1;
        public int Height
        {
            get { return height; }
        }
    }

    public class ElementAttributeSelector : IElementAttributeSelector
    {
        private Dictionary<string, string> attributes = new Dictionary<string, string>();

        public ElementAttributeSelector(JArray attributes)
        {
            this.attributes = attributes.ToDictionary(x => x["Name"].ToString().ToLower(), x => x["Value"].ToString());
        }

        public string Get(string name)
        {
            var keyName = name.ToLower();
            if (this.attributes.ContainsKey(keyName))
            {
                return this.attributes[keyName];
            }
            else
            {
                return null;
            }
        }
    }
}
