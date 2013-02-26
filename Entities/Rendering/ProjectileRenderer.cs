using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using PrincessDefense.Entities.Components;

namespace PrincessDefense.Entities.Rendering
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
            this._textures = new Dictionary<string, ITexture>();

            ITextureFactory factory = this.RenderManager.GraphicsDevice.TextureFactory;
            this._textures.Add("Up", factory.CreateTexture(@"Resources\projectiles\arrowUp.png"));
            this._textures.Add("Left", factory.CreateTexture(@"Resources\projectiles\arrowLeft.png"));
            this._textures.Add("Right", factory.CreateTexture(@"Resources\projectiles\arrowRight.png"));
            this._textures.Add("Down", factory.CreateTexture(@"Resources\projectiles\arrowDown.png"));
        }
        #endregion

        #region Fields
        private Dictionary<string, ITexture> _textures;
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
            ITexture texture = this._textures[this.Projectile.Direction.ToString()];

            this.RenderManager.Tint(new Color(0, 0, 0, 0.5f));
            this.RenderManager.Render(texture, this.Projectile.Position);

            this.RenderManager.Tint(Color.White);
            this.RenderManager.Render(texture, this.Projectile.Position - new Vector2(0, this.Projectile.GroundDistance));
        }
        #endregion
    }
}
