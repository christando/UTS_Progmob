using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace UTS_72210454
{
    static class Style
    {

        //login 
        public static readonly Color LoginWarningColor = Color.FromRgb(255, 0, 0);
        public static readonly double LoginWarningFontSize = 16;

        public static readonly double LoginFontSize = 18;
        public static readonly double LoginTitleFontSize = 24;

        //public static readonly Color LoginButtonColor = Color.FromRgb(105, 66, 245);

        //homepage
        //public static readonly string HomePageCategoriesGroupFont = "Large";
        //public static readonly string HomePageCoursesGroupFontSize = "medium";

        //Category

        //Course
       // public static readonly double CourseTitleFontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
        //public static readonly string CoTitleMargin = "0, 0, 0, 20";
        public static readonly string CoLayoutFlag = "All";
        public static readonly string CoLayoutBond = "0, 0, 1, 1";
        //public static readonly string CoCVMargin = "30, 20, 30, 30";
        public static readonly string CoOrientation = "Vertical";
        public static readonly string CoItemSpacing = "20";
        //public static readonly string CoVSPadding = "15, 10";
        //public static readonly string CoVSMargin = "10, 5, 10, 5";

        //add button
        public static readonly Color btnColor = Color.FromRgb(0,90,156);
        public static readonly Color btnTextColor = Color.FromRgb(255, 255, 255);
        //public static readonly string btnFontSize = "Large";
        //public static readonly string btnFontAtribute = "Bold";
        //public static readonly double btnCRadius = 30;
        public static readonly double btnWReq = 60;
        public static readonly double btnHReq = 60;
        public static readonly double btnMargin = 20;
        public static readonly string btnLF = "PositionProportional";
        public static readonly string btnLBond = "1, 1, AutoSize, AutoSize";
        public static readonly string btnVops = "End";
        public static readonly string btnHops = "End";
    }
}
