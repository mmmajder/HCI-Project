﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace HelpSistem
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        // Window window;
        // private Action<string> doThings;
        public JavaScriptControlHelper()
        {
            // this.doThings = doThings;
        }

        /*public JavaScriptControlHelper(Window w)
        {
            window = w;
        }
*/
        public void RunFromJavascript(string param)
        {
            // doThings(param);
        }
    }
}
