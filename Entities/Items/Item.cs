using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.PrincessDefense.Entities.Events;
using Xemio.PrincessDefense.Entities.Rendering;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Particles;

namespace Xemio.PrincessDefense.Entities.Items
{
    public abstract class Item : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
            this.Renderer = new ItemRenderer(this);

            CollidableComponent collision = new CollidableComponent(this, 10);
            collision.IsSeperating = false;

            this.Components.Add(collision);

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<CollisionEvent>(this.OnCollide);
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the sine offset.
        /// </summary>
        public Vector2 SineOffset { get; private set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name
        {
            get { return "Unnamed"; }
        }
        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        public virtual string ActionName
        {
            get { return "Use"; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Item"/> is stackable.
        /// </summary>
        public bool Stackable
        {
            get { return false; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public virtual ITexture Icon
        {
            get { return null; }
        }
        /// <summary>
        /// Gets the team.
        /// </summary>
        public override Team Team
        {
            get { return Team.None; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the item collides with an entity.
        /// </summary>
        /// <param name="event">The @event.</param>
        protected virtual void OnCollide(CollisionEvent collisionEvent)
        {
            if (collisionEvent.IsIntersectionOf<Item, Player>() &&
                collisionEvent.Get<Item>() == this)
            {
                Player player = collisionEvent.Get<Player>();

                if (this.CanPickup(player))
                {
                    this.OnPickup(player);
                    this.Destroy();
                }
            }
        }
        /// <summary>
        /// Determines whether the specified player can pickup this instance.
        /// </summary>
        /// <param name="player">The player.</param>
        public virtual bool CanPickup(Player player)
        {
            return !player.GetComponent<InventoryComponent>().IsFull;
        }
        /// <summary>
        /// Called when a player picks up the item.
        /// </summary>
        /// <param name="player">The player.</param>
        public virtual void OnPickup(Player player)
        {
            InventoryComponent inventory = player.GetComponent<InventoryComponent>();
            if (inventory != null)
            {
                Sounds.Play(Sounds.Pickup);

                inventory.Items.Add(this);
                this.Destroy();
            }
        }
        /// <summary>
        /// Called when a player activates this item.
        /// </summary>
        /// <param name="player">The player.</param>
        public virtual void OnActivate(Player player)
        {
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            this.SineOffset = new Vector2(0, MathHelper.Sin(this._elapsed / 200.0f) * 5);

            base.Tick(elapsed);
        }
        #endregion
    }
}
