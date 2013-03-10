using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Editor.Entities;
using Xemio.GameLibrary.Entities;

namespace Xemio.PrincessDefense.Editor.Commands
{
    public class CreateEntityCommand : ICommand
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEntityCommand"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="entity">The entity.</param>
        public CreateEntityCommand(EntityEnvironment environment, MapEntity entity)
        {
            this.Environment = environment;
            this.Entity = entity;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public MapEntity Entity { get; private set; }
        #endregion

        #region ICommand Member
        /// <summary>
        /// Executes this command.
        /// </summary>
        public void Execute()
        {
            this.Environment.Add(this.Entity);
        }
        /// <summary>
        /// Undoes this command.
        /// </summary>
        public void Undo()
        {
            this.Environment.Remove(this.Entity);
        }
        #endregion
    }
}
