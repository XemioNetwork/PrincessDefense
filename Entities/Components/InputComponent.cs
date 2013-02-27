using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Characters;

namespace Xemio.PrincessDefense.Entities.Components
{
    public class InputComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputComponent"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public InputComponent(Player player) : base(player)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the player.
        /// </summary>
        public Player Player
        {
            get { return this.Entity as Player; }
        }
        /// <summary>
        /// Gets the keyboard.
        /// </summary>
        public KeyListener Keyboard
        {
            get { return XGL.GetComponent<KeyListener>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            bool walkLeft = this.Keyboard.IsKeyDown(Keys.A);
            bool walkRight = this.Keyboard.IsKeyDown(Keys.D);
            bool walkUp = this.Keyboard.IsKeyDown(Keys.W);
            bool walkDown = this.Keyboard.IsKeyDown(Keys.S);

            bool shootLeft = this.Keyboard.IsKeyDown(Keys.Left);
            bool shootRight = this.Keyboard.IsKeyDown(Keys.Right);
            bool shootUp = this.Keyboard.IsKeyDown(Keys.Up);
            bool shootDown = this.Keyboard.IsKeyDown(Keys.Down);

            if (walkLeft && walkUp)
            {
                this.Player.Walk(Direction.LeftUp);
            }
            else if (walkUp && walkRight)
            {
                this.Player.Walk(Direction.UpRight);
            }
            else if (walkDown && walkLeft)
            {
                this.Player.Walk(Direction.DownLeft);
            }
            else if (walkRight && walkDown)
            {
                this.Player.Walk(Direction.RightDown);
            }
            else if (walkLeft)
            {
                this.Player.Walk(Direction.Left);
            }
            else if (walkRight)
            {
                this.Player.Walk(Direction.Right);
            }
            else if (walkDown)
            {
                this.Player.Walk(Direction.Down);
            }
            else if (walkUp)
            {
                this.Player.Walk(Direction.Up);
            }

            BowComponent bowComponent = this.Player.GetComponent<BowComponent>();
            if (shootLeft)
            {
                bowComponent.Shoot(Direction.Left);
            }
            if (shootRight)
            {
                bowComponent.Shoot(Direction.Right);
            }
            if (shootUp)
            {
                bowComponent.Shoot(Direction.Up);
            }
            if (shootDown)
            {
                bowComponent.Shoot(Direction.Down);
            }
        }
        #endregion
    }
}
