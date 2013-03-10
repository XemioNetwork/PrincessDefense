using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Attributes;

namespace Xemio.PrincessDefense.Entities.Rendering
{
    public class CharacterRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterRenderer"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public CharacterRenderer(Entity entity) : base(entity)
        {
        }
        #endregion

        #region Fields
        private IPen _healthPen;
        private IBrush _shadowBrush;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the opacity.
        /// </summary>
        public virtual float Opacity
        {
            get { return 1.0f; }
        }
        /// <summary>
        /// Gets the character.
        /// </summary>
        public Character Character
        {
            get { return this.Entity as Character; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            AnimationComponent animation = this.Entity.GetComponent<AnimationComponent>();
            HealthComponent health = this.Entity.GetComponent<HealthComponent>();
            CollidableComponent collision = this.Entity.GetComponent<CollidableComponent>();
            
            if (this._shadowBrush == null)
            {
                this._shadowBrush = this.Geometry.Factory.CreateSolid(new Color(0, 0, 0, 0.2f));
                this._healthPen = this.Geometry.Factory.CreatePen(new Color(0.6f, 0.1f, 0, 0.8f), 6);
            }

            if (animation != null && animation.Frame != null)
            {
                int width = animation.Frame.Width;
                int height = animation.Frame.Height;

                Vector2 position = this.Character.Position;
                float hurtPercentage = health.HurtPercentage;

                position -= new Vector2(width * 0.5f, height * 0.5f);

                this.Geometry.FillEllipse(this._shadowBrush,
                    new Rectangle(
                        animation.Frame.Width * 0.5f - 15,
                        animation.Frame.Height - 15,
                        28, 18) + position);

                this.Geometry.FillEllipse(this._shadowBrush,
                    new Rectangle(
                        animation.Frame.Width * 0.5f - 10,
                        animation.Frame.Height - 10,
                        20, 10) + position);

                if (this.Opacity < 1.0f)
                {
                    this.RenderManager.Tint(new Color(1, 1, 1, this.Opacity));
                }

                this.RenderManager.Render(animation.Frame, position);

                if (health.IsHurt)
                {
                    this.RenderManager.Tint(new Color(1.0f, 0, 0.1f, 0.8f - hurtPercentage * 0.8f));
                    this.RenderManager.Render(
                        animation.Frame,
                        position);
                }

                //this.Geometry.DrawCircle(Color.Red, this.Character.Position, collision.Radius);
                this.RenderManager.Tint(Color.White);
            }

            base.Render();
        }
        #endregion
    }
}
