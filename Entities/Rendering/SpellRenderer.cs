using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.PrincessDefense.Entities.Spells;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Entities.Rendering
{
    public class SpellRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpellRenderer"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public SpellRenderer(BaseEntity entity) : base(entity)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            AnimationComponent animation = this.Entity.GetComponent<AnimationComponent>();
            
            if (animation != null && animation.Frame != null)
            {
                int width = animation.Frame.Width;
                int height = animation.Frame.Height;

                Vector2 position = this.Entity.Position;
                position -= new Vector2(width * 0.5f, height * 0.5f);

                this.RenderManager.Render(animation.Frame, position);
            }
        }
        #endregion
    }
}
