using Infrastructure.ObjectModel.Animators;
//*** Guy Ronen (c) 2008-2011 ***//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.Managers;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using System;

namespace SpaceInvaders.Infrastructure.ObjectModels
{
    public class Sprite : LoadableDrawableComponent
    {
        private Color[] m_Pixels;

        protected void UseOwnSpriteBatch(SpriteSortMode i_SpriteSortMode, BlendState i_BlendState)
        {
            SpriteBatch = new SpriteBatch(this.GraphicsDevice);
            m_UseSharedBatch = false;
            m_BlendState = i_BlendState;
            m_SortMode = i_SpriteSortMode;
        }

        private GameScreen m_GameScreen;

        public GameScreen GameScreen 
        {
            get{return m_GameScreen; }
            set { m_GameScreen = value; }
        }

        public Color[] Pixels
        {
            get
            {
                if (m_Pixels == null)
                {
                    m_Pixels = new Color[Texture.Width * Texture.Height];
                    Texture.GetData<Color>(m_Pixels);
                }
                return m_Pixels;
            }
            set
            {
              m_Pixels = value.Clone() as Color[];
              Texture.SetData<Color>(value);
            }
        }

        private string m_Id = Guid.NewGuid().ToString();
        public string Id { get { return m_Id; } }

        public event EventHandler<EventArgs> Disposed;

        protected virtual void OnDisposed()
        {
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }

        protected override void Dispose(bool disposing)
        {
            OnDisposed();
            base.Dispose(disposing);
        }

        protected CompositeAnimator m_Animations;
        public CompositeAnimator Animations
        {
            get { return m_Animations; }
            set { m_Animations = value; }
        }

        private Texture2D m_Texture;
        public Texture2D Texture
        {
            get { return m_Texture; }
            set { m_Texture = value; }
        }

        public virtual float Width
        {
            get { return m_WidthBeforeScale * m_Scales.X; }
            set { m_WidthBeforeScale = value / m_Scales.X; }
        }

        public virtual float Height
        {
            get { return m_HeightBeforeScale * m_Scales.Y; }
            set { m_HeightBeforeScale = value / m_Scales.Y; }
        }

        protected float m_WidthBeforeScale;
        public float WidthBeforeScale
        {
            get { return m_WidthBeforeScale; }
            set { m_WidthBeforeScale = value; }
        }

        protected float m_HeightBeforeScale;
        public float HeightBeforeScale
        {
            get { return m_HeightBeforeScale; }
            set { m_HeightBeforeScale = value; }
        }

        protected Vector2 m_Position = Vector2.Zero;
        /// <summary>
        /// Represents the location of the sprite's origin point in screen coorinates
        /// </summary>
        public Vector2 Position
        {
            get { return m_Position; }
            set
            {
                if (m_Position != value)
                {
                    m_Position = value;
                    OnPositionChanged();
                }
            }
        }

        public Vector2 m_PositionOrigin;
        public Vector2 PositionOrigin
        {
            get { return m_PositionOrigin; }
            set { m_PositionOrigin = value; }
        }

        public Vector2 m_RotationOrigin = Vector2.Zero;
        public Vector2 RotationOrigin
        {
            get { return m_RotationOrigin; }// r_SpriteParameters.RotationOrigin; }
            set { m_RotationOrigin = value; }
        }

        private Vector2 PositionForDraw
        {
            get { return this.Position - this.PositionOrigin + this.RotationOrigin; }
        }

        public Vector2 TopLeftPosition
        {
            get { return this.Position - this.PositionOrigin; }
            set { this.Position = value + this.PositionOrigin; }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)TopLeftPosition.X,
                    (int)TopLeftPosition.Y,
                    (int)this.Width,
                    (int)this.Height);
            }
        }

        public Rectangle BoundsBeforeScale
        {
            get
            {
                return new Rectangle(
                    (int)TopLeftPosition.X,
                    (int)TopLeftPosition.Y,
                    (int)this.WidthBeforeScale,
                    (int)this.HeightBeforeScale);
            }
        }

        protected Rectangle m_SourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return m_SourceRectangle; }
            set { m_SourceRectangle = value; }
        }

        public Vector2 TextureCenter
        {
            get
            {
                return new Vector2((float)(m_Texture.Width / 2), (float)(m_Texture.Height / 2));
            }
        }

        public Vector2 SourceRectangleCenter
        {
            get { return new Vector2((float)(m_SourceRectangle.Width / 2), (float)(m_SourceRectangle.Height / 2)); }
        }

        protected float m_Rotation = 0;
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        protected Vector2 m_Scales = Vector2.One;
        public Vector2 Scales
        {
            get { return m_Scales; }
            set
            {
                if (m_Scales != value)
                {
                    m_Scales = value;
                    // Notify the Collision Detection mechanism:
                    OnPositionChanged();
                }
            }
        }

        protected Color m_TintColor = Color.White;
        public Color TintColor
        {
            get { return m_TintColor; }
            set { m_TintColor = value; }
        }

        public float Opacity
        {
            get { return (float)m_TintColor.A / (float)byte.MaxValue; }
            set { m_TintColor.A = (byte)(value * (float)byte.MaxValue); }
        }

        protected float m_LayerDepth;
        public float LayerDepth
        {
            get { return m_LayerDepth; }
            set { m_LayerDepth = value; }
        }

        protected SpriteEffects m_SpriteEffects = SpriteEffects.None;
        public SpriteEffects SpriteEffects
        {
            get { return m_SpriteEffects; }
            set { m_SpriteEffects = value; }
        }

        protected SpriteSortMode m_SortMode = SpriteSortMode.Deferred;
        public SpriteSortMode SortMode
        {
            get { return m_SortMode; }
            set { m_SortMode = value; }
        }

        protected BlendState m_BlendState = BlendState.AlphaBlend;
        public BlendState BlendState
        {
            get { return m_BlendState; }
            set { m_BlendState = value; }
        }

        protected SamplerState m_SamplerState = null;
        public SamplerState SamplerState
        {
            get { return m_SamplerState; }
            set { m_SamplerState = value; }
        }

        protected DepthStencilState m_DepthStencilState = null;
        public DepthStencilState DepthStencilState
        {
            get { return m_DepthStencilState; }
            set { m_DepthStencilState = value; }
        }

        protected RasterizerState m_RasterizerState = null;
        public RasterizerState RasterizerState
        {
            get { return m_RasterizerState; }
            set { m_RasterizerState = value; }
        }

        protected Effect m_Shader = null;
        public Effect Shader
        {
            get { return m_Shader; }
            set { m_Shader = value; }
        }

        protected Matrix m_TransformMatrix = Matrix.Identity;
        public Matrix TransformMatrix
        {
            get { return m_TransformMatrix; }
            set { m_TransformMatrix = value; }
        }

        protected Vector2 m_Velocity = Vector2.Zero;
        /// <summary>
        /// Pixels per Second on 2 axis
        /// </summary>
        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        private float m_AngularVelocity = 0;
        /// <summary>
        /// Radians per Second on X Axis
        /// </summary>
        public float AngularVelocity
        {
            get { return m_AngularVelocity; }
            set { m_AngularVelocity = value; }
        }


        public Sprite(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
        { }

        public Sprite(string i_AssetName, Game i_Game, int i_CallsOrder)
            : base(i_AssetName, i_Game, i_CallsOrder)
        { }

        public Sprite(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game, int.MaxValue)
        { }

        public Sprite(string i_AssetName, GameScreen i_GameScreen)
            : base(i_AssetName, i_GameScreen.Game, int.MaxValue)
        {
            m_GameScreen = i_GameScreen;
        }

        /// <summary>
        /// Default initialization of bounds
        /// </summary>
        /// <remarks>
        /// Derived classes are welcome to override this to implement their specific boudns initialization
        /// </remarks>
        protected override void InitBounds()
        {
            if (m_Texture != null)
            {
                m_WidthBeforeScale = m_Texture.Width;
                m_HeightBeforeScale = m_Texture.Height;
            }


            if (m_Position == null)
                m_Position = Vector2.Zero;

            InitSourceRectangle();

            InitOrigins();
        }

        protected virtual void InitOrigins()
        {
        }

        protected virtual void InitSourceRectangle()
        {
            m_SourceRectangle = new Rectangle(0, 0, (int)m_WidthBeforeScale, (int)m_HeightBeforeScale);
        }


        private bool m_UseSharedBatch = true;
        public bool IsUseSharedBatch { get { return m_UseSharedBatch; } }
        protected SpriteBatch m_SpriteBatch;
        public SpriteBatch SpriteBatch
        {
            set
            {
                    m_SpriteBatch = value;
                    m_UseSharedBatch = true;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Animations = new CompositeAnimator(this);
        }

        protected override void LoadContent()
        {
            LoadTextureOrFont();

            if (m_SpriteBatch == null)
            {
                m_SpriteBatch =
                    Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

                if (m_SpriteBatch == null)
                {
                    m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
                    m_UseSharedBatch = false;
                }
            }

            base.LoadContent();
        }

        protected virtual void LoadTextureOrFont()
        {
            m_Texture = Game.Content.Load<Texture2D>(m_AssetName);
        }


        /// <summary>
        /// Basic movement logic (position += velocity * totalSeconds)
        /// </summary>
        /// <param name="gameTime"></param>
        /// <remarks>
        /// Derived classes are welcome to extend this logic.
        /// </remarks>
        public override void Update(GameTime gameTime)
        {
            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.Position += this.Velocity * totalSeconds;
            this.Rotation += this.AngularVelocity * totalSeconds;

            base.Update(gameTime);

            this.Animations.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if (!m_UseSharedBatch)
            {
                if (SaveAndRestoreDeviceState)
                {
                    saveDeviceStates();
                }

                m_SpriteBatch.Begin(
                    SortMode, m_BlendState, SamplerState,
                    DepthStencilState, RasterizerState, Shader, TransformMatrix);
            }

            DrawSpriteBatch();


            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();

                if (SaveAndRestoreDeviceState)
                {
                    restoreDeviceStates();
                }
            }

            base.Draw(gameTime);
        }


        protected virtual void DrawSpriteBatch()
        {
            m_SpriteBatch.Draw(m_Texture, this.PositionForDraw,
                this.SourceRectangle, this.TintColor,
               this.Rotation, this.RotationOrigin, this.Scales,
               SpriteEffects.None, this.LayerDepth);
        }

        #region Collision Handlers
        protected override void DrawBoundingBox()
        {
            // not implemented yet
        }

        public virtual bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;
            ICollidable2D source = i_Source as ICollidable2D;
            if (source != null)
            {
                collided = source.Bounds.Intersects(this.Bounds);
            }

            return collided;
        }

        public virtual void Collided(ICollidable i_Collidable)
        {

        }
        #endregion //Collision Handlers

        public Sprite ShallowClone()
        {
            return this.MemberwiseClone() as Sprite;
        }

        class DeviceStates
        {
            public BlendState BlendState;
            public SamplerState SamplerState;
            public DepthStencilState DepthStencilState;
            public RasterizerState RasterizerState;
        }

        DeviceStates m_SavedDeviceStates = new DeviceStates();
        protected void saveDeviceStates()
        {
            m_SavedDeviceStates.BlendState = GraphicsDevice.BlendState;
            m_SavedDeviceStates.SamplerState = GraphicsDevice.SamplerStates[0];
            m_SavedDeviceStates.DepthStencilState = GraphicsDevice.DepthStencilState;
            m_SavedDeviceStates.RasterizerState = GraphicsDevice.RasterizerState;
        }

        private void restoreDeviceStates()
        {
            GraphicsDevice.BlendState = m_SavedDeviceStates.BlendState;
            GraphicsDevice.SamplerStates[0] = m_SavedDeviceStates.SamplerState;
            GraphicsDevice.DepthStencilState = m_SavedDeviceStates.DepthStencilState;
            GraphicsDevice.RasterizerState = m_SavedDeviceStates.RasterizerState;
        }

        protected bool m_SaveAndRestoreDeviceState = false;
        public bool SaveAndRestoreDeviceState
        {
            get { return m_SaveAndRestoreDeviceState; }
            set { m_SaveAndRestoreDeviceState = value; }
        }

        public bool IsMouseHover(IInputManager i_InputManager)
        {
            bool isMouseCollided = false;
            Rectangle mouseRectangle = new Rectangle(i_InputManager.MouseState.X, i_InputManager.MouseState.Y, 32, 32);
            if (this.Bounds.Intersects(mouseRectangle) && i_InputManager.MouseState.RightButton == ButtonState.Pressed)
            {
                isMouseCollided = true;
                OnRightMouseClick();
            }
            else if (this.Bounds.Intersects(mouseRectangle))
            {
                isMouseCollided = true;
            }
            return isMouseCollided;
        }

        public event EventHandler RightMouseClick;

        protected void OnRightMouseClick()
        {
            if (RightMouseClick != null)
            {
                RightMouseClick(this, EventArgs.Empty);
            }
        }

    }
}