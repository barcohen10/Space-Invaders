using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace C15Ex03Dotan301810610Bar308000322.Menu
{
    public class MethodKey
    {
        public Keys ActivateKey { get; set; }

        public ClickedEventHandler MethodToRun { get; set; }
    }
}
