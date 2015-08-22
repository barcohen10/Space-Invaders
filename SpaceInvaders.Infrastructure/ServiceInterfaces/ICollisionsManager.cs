using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ServiceInterfaces
{
    // TODO 06: Define the base interface for collidable objects (2D/3D):
    public interface ICollidable
    {
        event EventHandler<EventArgs> PositionChanged;
        event EventHandler<EventArgs> SizeChanged;
        event EventHandler<EventArgs> VisibleChanged;
        event EventHandler<EventArgs> Disposed;
        bool Visible { get; }
        bool CheckCollision(ICollidable i_Source);
        void Collided(ICollidable i_Collidable);
    }
    // -- end of TODO 06

    // TODO 07: Define the 2D specific interface for 2D collidable objects:
    public interface ICollidable2D : ICollidable
    {
        Rectangle Bounds { get; }
        Vector2 Velocity { get; }
    }
    // -- end of TODO 07

    // TODO 08: Define the 3D specific interface for 3D collidable objects:
    public interface ICollidable3D : ICollidable
    {
        BoundingBox Bounds { get; }
        Vector3 Velocity { get; }
    }
    // -- end of TODO 08

    // TODO 09: Define the collisions manager service interface:
    public interface ICollisionsManager
    {
        void AddObjectToMonitor(ICollidable i_Collidable);
    }
    // -- end of TODO 09
}
