using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.Managers
{
    // TODO 10: Implement the collisions manager service:
    public class CollisionsManager : GameService, ICollisionsManager
    {
        protected readonly List<ICollidable> m_Collidables = new List<ICollidable>();

        public CollisionsManager(Game i_Game) :
            base(i_Game, int.MaxValue)
        { }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ICollisionsManager), this);
        }

        public void AddObjectToMonitor(ICollidable i_Collidable)
        {
            if (!this.m_Collidables.Contains(i_Collidable))
            {
                this.m_Collidables.Add(i_Collidable);
                i_Collidable.PositionChanged += collidable_PositionChanged;
                i_Collidable.Disposed += collidable_Disposed;
            }
        }

        private void collidable_PositionChanged(object i_Collidable)
        {
            if (i_Collidable is ICollidable)
            {// to be on the safe side :)
                checkCollision(i_Collidable as ICollidable);
            }
        }

        private void checkCollision(ICollidable i_Source)
        {
            List<ICollidable> collidedComponents = new List<ICollidable>();

            // finding who collided with i_Source:
            foreach (ICollidable target in m_Collidables)
            {
                if (i_Source.Visible && i_Source != target && target.Visible)
                {
                    if (target.CheckCollision(i_Source))
                    {
                        collidedComponents.Add(target);
                    }
                }
            }

            // Informing i_Source and all the collided targets about the collision:
            foreach (ICollidable target in collidedComponents)
            {
                target.Collided(i_Source);
                i_Source.Collided(target);
            }
        }

        private void collidable_Disposed(object sender, EventArgs e)
        {
            ICollidable collidable = sender as ICollidable;

            if (collidable != null
                &&
                this.m_Collidables.Contains(collidable))
            {
                collidable.PositionChanged -= collidable_PositionChanged;
                collidable.Disposed -= collidable_Disposed;
                m_Collidables.Remove(collidable);
            }
        }
    }
    // -- end of TODO 10
}
