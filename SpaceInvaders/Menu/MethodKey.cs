using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu
{
    public class MethodKey
    {
        public Keys ActivateKey { get; set; }

        public ClickedEventHandler MethodToRun { get; set; }
    }
}
