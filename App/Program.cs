using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Xna;
using Xemio.GameLibrary.Rendering.GDIPlus;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary;
using Xemio.PrincessDefense.Scenes;
using Xemio.PrincessDefense.Scenes.Menues;

namespace Xemio.PrincessDefense.App
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            MainForm mainForm = new MainForm();

            XGL.Initialize(new XnaGraphicsInitializer());
            XGL.Run(mainForm.Handle, 400, 300, 60);

            SceneManager sceneManager = XGL.GetComponent<SceneManager>();
            sceneManager.Add(new GameMenu());
            sceneManager.Add(new FpsCounter());

            Art.LoadContent();
            Application.Run(mainForm);
        }
    }
}
