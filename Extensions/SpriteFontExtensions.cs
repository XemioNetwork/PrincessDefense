using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary;

namespace PrincessDefense.Extensions
{
    public static class SpriteFontExtensions
    {
        #region Methods
        /// <summary>
        /// Renders a shadowed text.
        /// </summary>
        /// <param name="spriteFont">The sprite font.</param>
        /// <param name="message">The message.</param>
        /// <param name="position">The position.</param>
        public static void RenderShadowed(this SpriteFont spriteFont, string message, Vector2 position, float opacity)
        {
            IRenderManager renderManager = XGL.GetComponent<IRenderManager>();

            renderManager.Tint(new Color(0, 0, 0, opacity));
            spriteFont.Render(message, position + new Vector2(0, 1));

            renderManager.Tint(Color.White);
            spriteFont.Render(message, position);
        }
        #endregion
    }
}
