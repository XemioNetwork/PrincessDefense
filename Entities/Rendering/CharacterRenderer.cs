using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using PrincessDefense.Entities.Characters;
using PrincessDefense.Entities.Components;

namespace PrincessDefense.Entities.Rendering
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

        #region Properties
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
            
            if (animation != null && animation.CurrentFrame != null)
            {
                int width = animation.CurrentFrame.Width;
                int height = animation.CurrentFrame.Height;

                Vector2 position = this.Character.Position;

                position -= new Vector2(width * 0.5f, height * 0.5f);

                this.RenderManager.Render(animation.CurrentFrame, position);
            }

            base.Render();
        }
        #endregion
    }
}
