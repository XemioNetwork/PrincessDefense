using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Components;
using Xemio.PrincessDefense.Entities;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Characters;

namespace Xemio.PrincessDefense.Entities.Environment
{
    public class Camera
    {
        #region Properties
        /// <summary>
        /// Gets or sets the min.
        /// </summary>
        public Vector2 Min { get; set; }
        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        public Vector2 Max { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public Character Target { get; set; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.GetComponent<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the display offset.
        /// </summary>
        public Vector2 DisplayOffset { get; private set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Moves the camera instantly.
        /// </summary>
        /// <param name="position">The position.</param>
        public void MoveTo(Vector2 position)
        {
            this.Position = position;
            this.DisplayOffset = position;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            if (this.Target != null && !this.Target.IsDestroyed)
            {
                this.Position = this.Target.Position;
            }

            Vector2 screenCenter = this.GraphicsDevice.DisplayMode.Center;
            Vector2 min = this.Min + screenCenter;
            Vector2 max = new Vector2(this.Max.X - screenCenter.X, this.Max.Y - screenCenter.Y);
            
            this.DisplayOffset += (this.Position - this.DisplayOffset) * 0.15f;
            this.DisplayOffset = Vector2.Clamp(this.DisplayOffset, min, max);
        }
        #endregion
    }
}
