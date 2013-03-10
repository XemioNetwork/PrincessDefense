using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering.GDIPlus;
using Xemio.GameLibrary.Game;
using Xemio.PrincessDefense.Editor.Scenes;

namespace Xemio.PrincessDefense.Editor.App
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();

            XGL.Initialize(new GDIGraphicsInitializer());
            XGL.Run(mainForm.Surface, 640, 640, 30);

            Registry.Load();
            Art.LoadContent();

            SceneManager sceneManager = XGL.GetComponent<SceneManager>();
            sceneManager.Add(new EditorScene());

            Application.Run(mainForm);
        }
    }
}
