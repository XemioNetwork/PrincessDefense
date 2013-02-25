using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Common.Randomization;
using PrincessDefense.Entities.Characters;

namespace PrincessDefense.Entities.Components
{
    public class PrincessBehavior : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PrincessBehavior"/> class.
        /// </summary>
        /// <param name="princess">The princess.</param>
        public PrincessBehavior(Princess princess) : base(princess)
        {
            this._random = new RandomProxy();
        }
        #endregion

        #region Fields
        private IRandom _random;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the princess.
        /// </summary>
        public Princess Princess
        {
            get { return this.Entity as Princess; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            TargetingBehavior behavior = this.Princess.GetComponent<TargetingBehavior>();

            if (behavior.Target == null)
            {
                bool changeFacing = this._random.NextBoolean(1 / 40.0);
                bool walk = this._random.NextBoolean(0.8);

                if (changeFacing)
                {
                    Direction direction = (Direction)this._random.Next(0, 9);
                    this.Princess.Facing = direction;
                }
                if (walk)
                {
                    this.Princess.Walk(this.Princess.Facing);
                }
            }
        }
        #endregion
    }
}
