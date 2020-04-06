using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication13.Helper
{
    public enum Enumstatus
    {
        [Description("Mở")]
        Open = 0,
        [Description("Đóng")]
        In_Progress = 1,
        [Description("Đóng")]
        Close = 4,
        [Description("check_circle_outline")]
        Confirm_True = 5,
        [Description("highlight_off")]
        Confirm_False = 6,


    }
}
