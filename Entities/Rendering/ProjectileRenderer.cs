using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Components;

namespace Xemio.PrincessDefense.Entities.Rendering
{
    public class ProjectileRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileRenderer"/> class.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        public ProjectileRenderer(Projectile projectile) : base(projectile)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the projectile.
        /// </summary>
        public Projectile Projectile
        {
            get { return this.Entity as Projectile; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            ITexture texture = null;
            switch (this.Projectile.Facing)
            {
                case Direction.Up:
                    texture = Art.Arrow[0];
                    break;
                case Direction.Down:
                    texture = Art.Arrow[1];
                    break;
                case Direction.Left:
                    texture = Art.Arrow[2];
                    break;
                case Direction.Right:
                    texture = Art.Arrow[3];
                    break;
            }

            this.RenderManager.Tint(new Color(0, 0, 0, 0.5f));
            this.RenderManager.Render(texture, this.Projectile.Position);

            this.RenderManager.Tint(Color.White);
            this.RenderManager.Render(texture, this.Projectile.Position - new Vector2(0, this.Projectile.GroundDistance));
        }
        #endregion
    }
}
