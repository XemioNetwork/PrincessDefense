using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.PrincessDefense.Entities.Items;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Entities.Rendering
{
    public class ItemRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemRenderer"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public ItemRenderer(Item item) : base(item)
        {
        }
        #endregion

        #region Fields
        private IBrush _shadowBrush;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the item.
        /// </summary>
        public Item Item
        {
            get { return this.Entity as Item; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this item.
        /// </summary>
        public override void Render()
        {
            if (this._shadowBrush == null)
            {
                this._shadowBrush = this.Geometry.Factory.CreateSolid(new Color(0, 0, 0, 0.4f));
            }

            Vector2 offset = new Vector2(0, 20);
            Vector2 center = new Vector2(this.Item.Icon.Width, this.Item.Icon.Height) * 0.5f;

            this.Geometry.FillEllipse(
                this._shadowBrush,
                new Rectangle(-12, -5, 24, 10) + offset + this.Item.Position);

            this.RenderManager.Render(
                this.Item.Icon,
                this.Item.Position - center + this.Item.SineOffset);
        }
        #endregion
    }
}
