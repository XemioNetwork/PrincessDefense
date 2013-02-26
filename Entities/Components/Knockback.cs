using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PrincessDefense.Entities.Components
{
    public class Knockback
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Knockback"/> class.
        /// </summary>
        public Knockback(Type type) : this(type, 0)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Knockback"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="strength">The strength.</param>
        public Knockback(Type type, float strength)
        {
            this.Type = type;
            this.Strength = strength;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public Type Type { get; private set; }
        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        public float Strength { get; set; }
        #endregion
    }
    public class Knockback<T> : Knockback
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Knockback&lt;T&gt;"/> class.
        /// </summary>
        public Knockback() : base(typeof(T))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Knockback&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="strength">The strength.</param>
        public Knockback(float strength) : base(typeof(T), strength)
        {
        }
        #endregion
    }
}
