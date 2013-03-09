using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Rendering;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Entities.Spells
{
    public class FireLionSpell : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FireLionSpell"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="direction">The direction.</param>
        public FireLionSpell(Player owner, Vector2 velocity, Direction direction)
        {
            this.Renderer = new FireLionRenderer(this);

            this._owner = owner;
            this._velocity = velocity;

            AnimationComponent animation = new AnimationComponent(this);
            animation.Add(Art.FireLion);
            animation.PlayAnimation(DirectionHelper.GetAnimationName(direction));

            CollidableComponent collision = new CollidableComponent(this, 40);
            collision.IsStatic = true;

            DamageComponent damage = new DamageComponent(this, 1);
            damage.DamageOnContact = true;

            this.Components.Add(animation);
            this.Components.Add(collision);
            this.Components.Add(damage);

            Sounds.FireLion.Play();
        }
        #endregion

        #region Fields
        private Player _owner;
        private Vector2 _velocity;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public Direction Direction { get; set; }
        /// <summary>
        /// Gets the team.
        /// </summary>
        public override Team Team
        {
            get { return Team.Princess; }
        }
        /// <summary>
        /// Gets the owner.
        /// </summary>
        public override BaseEntity Owner
        {
            get { return this._owner; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            AnimationComponent animation = this.GetComponent<AnimationComponent>();
            if (animation.Instance != null &&
                animation.Frame == animation.Instance.Animation.Sheet.Textures.Last())
            {
                this.Destroy();
            }

            this.Position += this._velocity;
            base.Tick(elapsed);
        }
        #endregion
    }
}
