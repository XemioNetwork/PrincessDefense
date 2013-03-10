using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Rendering;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Particles;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Entities.Spells
{
    public class LightningclawSpell : Spell
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LightningclawSpell"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public LightningclawSpell(Player owner) : base(owner)
        {
            this.Renderer = new SpellRenderer(this);

            AnimationComponent animation = new AnimationComponent(this);
            animation.Add(Art.LightningclawSpell);

            animation.PlayAnimation("Spell");

            CollidableComponent collision = new CollidableComponent(this, 40);
            collision.IsStatic = true;

            DamageComponent damage = new DamageComponent(this, 1);
            damage.DamageOnImpact = true;

            this.Components.Add(animation);
            this.Components.Add(collision);
            this.Components.Add(damage);

            Sounds.Play(Sounds.Lightningclaw, this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the team.
        /// </summary>
        public override Team Team
        {
            get { return Team.Princess; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Ticks the specified elapsed.
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
            if (animation.Instance != null &&
                animation.Frame == animation.Instance.Animation.Sheet.Textures.ElementAt(9))
            {
                ExplosionEmitter explosion = new ExplosionEmitter();
                explosion.Position = this.Position + new Vector2(0, 16);
                explosion.ParticleCount = 3;

                this.Environment.Add(explosion);
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
