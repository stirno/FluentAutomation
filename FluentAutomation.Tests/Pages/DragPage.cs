using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Tests.Pages
{
    public class DragPage : PageObject<DragPage>
    {
        public DragPage(FluentTest test)
            : base(test)
        {
            this.Url = "/DragAndDrop";
        }

        public string Peg1 = "#pt1";
        public string Peg2 = "#pt2";
        public string Peg3 = "#pb1";
        public string Peg4 = "#pb2";
        public string Peg5 = "#pboth1";
        public string Peg6 = "#pboth2";

        public string Hole1 = "#t1";
        public string Hole2 = "#t2";
        public string Hole3 = "#b1";
        public string Hole4 = "#b2";
        public string Hole5 = "#b3";
        public string Hole6 = "#b4";
    }
}
