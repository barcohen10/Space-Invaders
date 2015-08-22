//*** Guy Ronen (c) 2008-2011 ***//
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using SpaceInvaders.Infrastructure.ServiceInterfaces;

namespace SpaceInvaders.Infrastructure.ObjectModels
{
    public abstract class LoadableDrawableComponent : DrawableGameComponent
    {
        public event EventHandler<EventArgs> Disposed;
        protected virtual void OnDisposed(object sender, EventArgs args)
        {
            if (Disposed != null)
            {
                Disposed.Invoke(sender, args);
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            OnDisposed(this, EventArgs.Empty);
        }

        protected string m_AssetName;

        // used to load the sprite:
        protected ContentManager ContentManager
        {
            get { return this.Game.Content; }
        }

        // TODO 11: Implement the PositionChanged event:
        public event EventHandler<EventArgs> PositionChanged;
        protected virtual void OnPositionChanged()
        {
            if (PositionChanged != null)
            {
                PositionChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> SizeChanged;
        protected virtual void OnSizeChanged()
        {
            if (SizeChanged != null)
            {
                SizeChanged(this, EventArgs.Empty);
            }
        }
        // -- end of TODO 11

        public string AssetName
        {
            get { return m_AssetName; }
            set { m_AssetName = value; }
        }

        public LoadableDrawableComponent(
            string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_Game)
        {
            this.AssetName = i_AssetName;
            this.UpdateOrder = i_UpdateOrder;
            this.DrawOrder = i_DrawOrder;

            // register in the game:
            this.Game.Components.Add(this);
        }

        public LoadableDrawableComponent(
            string i_AssetName,
            Game i_Game,
            int i_CallsOrder)
            : this(i_AssetName, i_Game, i_CallsOrder, i_CallsOrder)
        { }

        public override void Initialize()
        {
            base.Initialize();

            // TODO 12: Register in the collisions manager:
            if (this is ICollidable)
            {
                ICollisionsManager collisionMgr =
                    this.Game.Services.GetService(typeof(ICollisionsManager))
                        as ICollisionsManager;

                if (collisionMgr != null)
                {
                    collisionMgr.AddObjectToMonitor(this as ICollidable);
                }
            }
            // -- end of TODO 12

            // After everything is loaded and initialzied,
            // lets init graphical aspects:
            InitBounds();   // a call to an abstract method;
        }

        // TODO 02: Show/Hide Bounding box
#if DEBUG
        protected bool m_ShowBoundingBox = true;
#else
        protected bool m_ShowBoundingBox = false;
#endif

        public bool ShowBoundingBox
        {
            get { return m_ShowBoundingBox; }
            set { m_ShowBoundingBox = value; }
        }
        // -- end of TODO 02

        protected abstract void InitBounds();

        // TODO 03: enforce the logic of drawing the bounding box to the derivies:
        public override void Draw(GameTime gameTime)
        {
            DrawBoundingBox();
            base.Draw(gameTime);
        }

        protected abstract void DrawBoundingBox();
        // -- end of TODO 03
    }
}