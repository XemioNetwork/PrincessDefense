using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Items;
using Xemio.PrincessDefense.Extensions;

namespace Xemio.PrincessDefense.Scenes.Components
{
    public class ItemButtons : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemButtons"/> class.
        /// </summary>
        public ItemButtons()
        {
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            InventoryComponent inventory = Player.Instance.GetComponent<InventoryComponent>();
            if (this.Keyboard.IsKeyPressed(Keys.D1))
            {
                inventory.Activate(0);
            }
            if (this.Keyboard.IsKeyPressed(Keys.D2))
            {
                inventory.Activate(1);
            }
        }
        /// <summary>
        /// Renders the item.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="position">The position.</param>
        private void RenderItem(int itemIndex, Vector2 position)
        {
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;
            InventoryComponent inventory = Player.Instance.GetComponent<InventoryComponent>();

            string text = inventory.Items[itemIndex].ActionName + " " + inventory.Items[itemIndex].Name;
            Vector2 fontSize = Art.Font.MeasureString(text);

            renderManager.Tint(new Color(0, 0, 0, 0.4f));
            renderManager.Render(inventory.Items[itemIndex].Icon, position + new Vector2(0, 1));

            renderManager.Tint(Color.White);
            renderManager.Render(inventory.Items[itemIndex].Icon, position);

            Art.Font.RenderShadowed(text, position + new Vector2(-fontSize.X - 10, 16 - fontSize.Y * 0.5f), 0.7f);
        }
        /// <summary>
        /// Renders the scene.
        /// </summary>
        public override void Render()
        {
            InventoryComponent inventory = Player.Instance.GetComponent<InventoryComponent>();
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;

            renderManager.Render(Art.Buttons, new Vector2(310, 10));

            if (inventory.Items.Count >= 1)
            {
                this.RenderItem(0, new Vector2(317, 17));
            }
            if (inventory.Items.Count >= 2)
            {
                this.RenderItem(1, new Vector2(349, 49));
            }
        }
        #endregion
    }
}
