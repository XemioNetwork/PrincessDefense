using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.GDIPlus;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary;
using Xemio.PrincessDefense.Scenes;
using Xemio.PrincessDefense.Scenes.Menues;
using Xemio.PrincessDefense.Scenes.Components;
using Xemio.PrincessDefense.Levels;

namespace Xemio.PrincessDefense.App
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            MainForm mainForm = new MainForm();

            XGL.Initialize(new GDIGraphicsInitializer());
            XGL.Run(mainForm.Handle, 400, 300, 60);

            Art.LoadContent();
            Sounds.LoadContent();

            SceneManager sceneManager = XGL.GetComponent<SceneManager>();
            sceneManager.Add(new GameMenu());
            sceneManager.Add(new FPSCounter());

            Application.Run(mainForm);
        }
    }
}
