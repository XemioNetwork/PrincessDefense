﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.PrincessDefense.Editor.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        void Execute();
        /// <summary>
        /// Undoes this command.
        /// </summary>
        void Undo();
    }
}
