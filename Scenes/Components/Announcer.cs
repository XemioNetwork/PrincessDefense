using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Scenes.Components
{
    public class Announcer : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Announcer"/> class.
        /// </summary>
        public Announcer()
        {
            this.TimePerLetter = 50;
            this.DisplayTime = 3000;
            this.Visible = true;
        }
        #endregion

        #region Fields
        private string _message;

        private float _elapsed;
        private float _elapsedDisplayTime;

        private int _displayLength;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message
        {
            get
            {
                if (this._message == null)
                {
                    return string.Empty;
                }

                return this._message.Substring(0, this._displayLength);
            }
        }
        /// <summary>
        /// Gets or sets the time per letter.
        /// </summary>
        public float TimePerLetter { get; set; }
        /// <summary>
        /// Gets or sets the display time.
        /// </summary>
        public float DisplayTime { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Announcer"/> is visible.
        /// </summary>
        public bool Visible { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Announces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Announce(string message)
        {
            this._message = message;

            this._elapsedDisplayTime = 0;
            this._elapsed = 0;
            this._displayLength = 0;

            this.Visible = true;
        }
        /// <summary>
        /// Sets the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SetMessage(string message)
        {
            this._message = message;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this._message != null)
            {
                this._elapsedDisplayTime += elapsed;

                if (this._elapsedDisplayTime < this.DisplayTime)
                {
                    this._elapsed += elapsed;
                    while (this._elapsed >= this.TimePerLetter && this.TimePerLetter > 0)
                    {
                        this._displayLength++;
                        this._elapsed -= this.TimePerLetter;

                        if (this._displayLength > this._message.Length)
                        {
                            this._displayLength = this._message.Length;
                        }
                    }
                }
                else
                {
                    this.Visible = false;
                }

                if (this.TimePerLetter == 0)
                {
                    this._displayLength = this._message.Length;
                }
            }
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            if (this.Visible)
            {
                IRenderManager renderManager = this.GraphicsDevice.RenderManager;
                Vector2 position = this.GraphicsDevice.DisplayMode.Center - Art.Font.MeasureString(this.Message) * 0.5f;

                renderManager.Tint(new Color(0, 0, 0, 0.6f));
                Art.Font.Render(this.Message, position + new Vector2(0, 1));

                renderManager.Tint(Color.White);
                Art.Font.Render(this.Message, position);
            }
        }
        #endregion
    }
}
