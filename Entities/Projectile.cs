using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Rendering;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Attributes;

namespace Xemio.PrincessDefense.Entities
{
    public class Projectile : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        /// <param name="character">The player.</param>
        /// <param name="facing">The facing.</param>
        public Projectile(Character character, Direction facing) : this(character, facing, character.GetDirection())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        /// <param name="character">The player.</param>
        /// <param name="facing">The direction.</param>
        /// <param name="vector">The vector.</param>
        public Projectile(Character character, Direction facing, Vector2 vector)
        {
            this.Renderer = new ProjectileRenderer(this);

            this.Character = character;
            this.Facing = facing;
            this.Vector = vector;
            
            this._gravity = 0;
            this.GroundDistance = 30.0f;
            this.Speed = 9.0f;

            CollidableComponent collision = new CollidableComponent(this, 5);

            HealthComponent health = new HealthComponent(this);
            health.SetHealth(0);

            DamageComponent damage = new DamageComponent(
                this, character.GetComponent<DamageComponent>().Damage);

            this.Components.Add(collision);
            this.Components.Add(health);
            this.Components.Add(damage);

            switch (facing)
            {
                case Direction.Right:
                    collision.Offset = new Vector2(31, 0);
                    break;
                case Direction.Down:
                    collision.Offset = new Vector2(0, 31);
                    break;
            }

            collision.Offset -= new Vector2(0, this.GroundDistance);
        }
        #endregion

        #region Fields
        private float _gravity;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the player.
        /// </summary>
        public Character Character { get; private set; }
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// Gets or sets the ground distance.
        /// </summary>
        public float GroundDistance { get; set; }
        /// <summary>
        /// Gets the facing.
        /// </summary>
        public Direction Facing { get; private set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public Vector2 Vector { get; set; }
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
            get { return this.Character; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Position += this.Vector * this.Speed;

            this._gravity += 0.03f;
            this.GroundDistance -= this._gravity;

            CollidableComponent collision = this.GetComponent<CollidableComponent>();
            collision.Offset += new Vector2(0, this._gravity);

            if (this.GroundDistance <= 0.0f)
            {
                this.Destroy();
            }
        }
        #endregion
    }
}
