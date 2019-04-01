using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ToDo.iOS
{
    public class Bootstrapper : ToDo.Bootstrapper
    {

        public static void Init()
        {
            var instance = new Bootstrapper();
        }

    }
}