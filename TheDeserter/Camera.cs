using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    /// <summary>
    /// The Camera Controller
    /// </summary>
    class Camera
    {
        #region Variables
        protected float _zoom; //The zoom on the camera
        private Matrix _transform; // The transformation matrix
        private Vector2 _position; //The position of the camera
        #endregion
        #region Properties
        //Zooming Property
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }
        //Position Property
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        //Transform Property
        public Matrix Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }
        #endregion
        /// <summary>
        /// Camera controller
        /// </summary>
        public Camera()
        {
            _zoom = 2.5f;
            _position = Vector2.Zero;
        }

        #region Functions
        /// <summary>
        /// Main update function of the camera
        /// </summary>
        /// <param name="graphicsDevice">the graphics device in the game</param>
        public void Update(GraphicsDevice graphicsDevice)
        {
            // Setting the transform matrix according to the zoom and position
            Transform = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
            //Setting the position of the camerat to follow the character and be centered
            Position = new Vector2
                (
                (Player.Instance.Position.X + Constants.TileSize) * Zoom  - World.MapWidth * Constants.TileSize / 2, 
                (Player.Instance.Position.Y + Constants.TileSize) * Zoom - World.MapHeight * Constants.TileSize / 2
                );

            
            //Making sure that the camera cant move outside the world
            if (Position.X < 0)
                Position = new Vector2(0, Position.Y);
            if (Position.Y < 0)
                Position = new Vector2(Position.X, 0);
            if (Position.X + World.MapWidth * Constants.TileSize > World.MapWidth * Constants.TileSize * Zoom)
                Position = new Vector2(World.MapWidth * Constants.TileSize * (Zoom - 1), Position.Y);
            if (Position.Y + World.MapHeight * Constants.TileSize > World.MapHeight * Constants.TileSize * Zoom)
                Position = new Vector2(Position.X, World.MapHeight * Constants.TileSize * (Zoom - 1));
        }
        #endregion
    }
}
