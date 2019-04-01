using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ToDo.Droid
{
    public class Bootstrapper : ToDo.Bootstrapper
    {

        public static void Init()
        {
            var instance = new Bootstrapper();
        }

    }
}