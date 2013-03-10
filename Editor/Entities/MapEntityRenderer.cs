using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Editor.Entities
{
    public class MapEntityRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEntityRenderer"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public MapEntityRenderer(MapEntity entity) : base(entity)
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the map entity.
        /// </summary>
        public MapEntity MapEntity
        {
            get { return this.Entity as MapEntity; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this.RenderManager.Render(
                this.MapEntity.Texture,
                this.MapEntity.Position - new Vector2(
                    this.MapEntity.Texture.Width * 0.5f,
                    this.MapEntity.Texture.Height * 0.5f));
        }
        #endregion
    }
}
