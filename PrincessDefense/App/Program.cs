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
using PrincessDefense.Scenes;
using Xemio.GameLibrary;

namespace PrincessDefense.App
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            MainForm mainForm = new MainForm();

            XGL.Initialize(new GDIGraphicsInitializer());
            XGL.Run(mainForm.Handle, 400, 300, 60);

            SceneManager sceneManager = XGL.GetComponent<SceneManager>();
            sceneManager.Add(new GameMenu());
            sceneManager.Add(new FpsCounter());

            Application.Run(mainForm);
        }
    }
}
