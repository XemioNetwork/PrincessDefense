using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using PrincessDefense.Entities.Rendering;
using PrincessDefense.Entities.Components;

namespace PrincessDefense.Entities
{
    public class Projectile : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public Projectile(Direction direction)
        {
            this.Renderer = new ProjectileRenderer(this);

            CollidableComponent collision = new CollidableComponent(this, 5);
            this.Components.Add(collision);

            this._gravity = 0;

            this.Direction = direction;
            this.GroundDistance = 30.0f;
            this.Speed = 9.0f;

            switch (direction)
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
        /// Gets or sets the speed.
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// Gets or sets the ground distance.
        /// </summary>
        public float GroundDistance { get; set; }
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public Direction Direction { get; private set; }
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
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            switch (this.Direction)
            {
                case Direction.Up:
                    this.Position += new Vector2(0, -1) * this.Speed;
                    break; 
                case Direction.Right:
                    this.Position += new Vector2(1, 0) * this.Speed;
                    break;
                case Direction.Left:
                    this.Position += new Vector2(-1, 0) * this.Speed;
                    break;
                case Direction.Down:
                    this.Position += new Vector2(0, 1) * this.Speed;
                    break;
            }

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
