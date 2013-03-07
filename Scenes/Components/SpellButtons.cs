using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Scenes.Components
{
    public class SpellButtons : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpellButtons"/> class.
        /// </summary>
        public SpellButtons()
        {
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Renders the scene.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.RenderManager.Render(Art.Buttons, new Vector2(310, 10));
        }
        #endregion
    }
}
