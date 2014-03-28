using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class Alert
    {
        public static Alert Cancel = new Alert(AlertField.CancelButton);
        public static Alert OK = new Alert(AlertField.OKButton);
        public static Alert Text = new Alert(AlertField.Text);

        public readonly AlertField Field;

        public Alert(AlertField field)
        {
            this.Field = field;
        }
    }

    public enum AlertField
    {
        OKButton,
        CancelButton,
        Text
    }
}
