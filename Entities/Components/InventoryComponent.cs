using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Items;

namespace Xemio.PrincessDefense.Entities.Components
{
    public class InventoryComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryComponent"/> class.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="capacity">The capacity.</param>
        public InventoryComponent(Character character, int capacity) : base(character)
        {
            this.Items = new List<Item>();
            this.Capacity = capacity;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the items.
        /// </summary>
        public List<Item> Items { get; private set; }
        /// <summary>
        /// Gets or sets the capacity.
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// Gets a value indicating whether this inventory is full.
        /// </summary>
        public bool IsFull
        {
            get { return this.Items.Count >= this.Capacity; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Activates the specified item index.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        public void Activate(int itemIndex)
        {
            if (this.Items.Count > itemIndex)
            {
                this.Items[itemIndex].OnActivate(Player.Instance);
                this.Items.RemoveAt(itemIndex);
            }
        }
        #endregion
    }
}
