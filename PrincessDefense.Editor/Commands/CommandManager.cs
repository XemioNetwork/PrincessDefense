using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;

namespace Xemio.PrincessDefense.Editor.Commands
{
    public class CommandManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager()
        {
            this._undoStack = new Stack<ICommand>();
            this._redoStack = new Stack<ICommand>();
        }
        #endregion

        #region Fields
        private Stack<ICommand> _undoStack;
        private Stack<ICommand> _redoStack;
        #endregion

        #region Singleton
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static CommandManager Instance
        {
            get { return Singleton<CommandManager>.Value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Execute(ICommand command)
        {
            command.Execute();
            this.Add(command);
        }
        /// <summary>
        /// Adds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Add(ICommand command)
        {
            this._redoStack.Clear();
            this._undoStack.Push(command);
        }
        /// <summary>
        /// Redoes the last undone action.
        /// </summary>
        public void Redo()
        {
            if (this._redoStack.Count > 0)
            {
                ICommand command = this._redoStack.Pop();
                command.Execute();

                this._undoStack.Push(command);
            }
        }
        /// <summary>
        /// Undoes the last action.
        /// </summary>
        public void Undo()
        {
            if (this._undoStack.Count > 0)
            {
                ICommand command = this._undoStack.Pop();
                command.Undo();

                this._redoStack.Push(command);
            }
        }
        #endregion
    }
}
