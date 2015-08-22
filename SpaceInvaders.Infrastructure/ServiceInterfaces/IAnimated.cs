using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ServiceInterfaces
{
    public interface IAnimated
    {
        void InitAnimations();
        void LastAnimation();
    }
}
