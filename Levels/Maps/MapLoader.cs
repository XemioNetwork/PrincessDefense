using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.PrincessDefense.Entities.Environment;
using Xemio.PrincessDefense.Levels.Maps;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.PrincessDefense.Levels
{
    public static class MapLoader
    {
        #region Methods
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void Load(World world, string fileName)
        {
            EventManager eventManager = XGL.GetComponent<EventManager>();
            GenericLinker<string, IMapCommand> linker = new AutomaticLinker<string, IMapCommand>();

            if (File.Exists(fileName))
            {
                string content = File.ReadAllText(fileName, Encoding.Default);
                string[] lines = content.Split('\n');

                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
                    {
                        string[] segments = line.Split(' ');
                        IMapCommand command = linker.Resolve(segments[0]);

                        if (command != null)
                        {
                            string[] arguments = new string[segments.Length - 1];

                            for (int i = 1; i <= arguments.Length; i++)
                            {
                                arguments[i - 1] = segments[i];
                            }

                            command.Execute(world, arguments);
                        }
                        else
                        {
                            eventManager.Send(new LoggingEvent(LoggingLevel.Exception, "Invalid command"));
                        }
                    }
                }
            }
        }
        #endregion
    }
}
