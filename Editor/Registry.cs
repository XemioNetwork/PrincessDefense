using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.PrincessDefense.Editor
{
    public static class Registry
    {
        #region Fields
        private static Dictionary<string, string> _commandMappings;
        private static Dictionary<string, string> _fileMappings;
        #endregion
        
        #region Methods
        /// <summary>
        /// Gets the command according to the specified filename.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetCommand(string fileName)
        {
            if (_commandMappings.ContainsKey(fileName))
            {
                return _commandMappings[fileName];
            }

            return string.Empty;
        }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public static string GetFileName(string command)
        {
            if (_fileMappings.ContainsKey(command))
            {
                return @"Resources\objects\" + _fileMappings[command];
            }

            return string.Empty;
        }
        /// <summary>
        /// Loads this instance.
        /// </summary>
        public static void Load()
        {
            string content = File.ReadAllText(@"Resources\objects\registry.txt", Encoding.Default);

            content = content.Replace("\t", string.Empty);
            content = content.Replace("\r", string.Empty);

            string[] lines = content.Split('\n');

            _commandMappings = new Dictionary<string, string>();
            _fileMappings = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                string[] segments = line.Split(' ');

                switch (segments[0])
                {
                    case "OBJECT":
                        string fileName = segments[1];
                        string command = segments[2];

                        _commandMappings.Add(fileName, command);
                        _fileMappings.Add(command, fileName);
                        break;
                }
            }
        }
        #endregion
    }
}
