using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapCreator.Rendering.Display;

namespace MapCreator.Wolf3D
{
    static class UI
    {
        //TODO add texture as a test
        public static float[] DrawUI()
        {
            float[] UIvert =
            {
                    530, DisplayManager.WindowSize.Y,                            .6f,.4f,.1f,
                    530, DisplayManager.WindowSize.Y-50,                         .6f,.4f,.1f,
                    DisplayManager.WindowSize.X, DisplayManager.WindowSize.Y-50, .6f,.4f,.1f,

                    DisplayManager.WindowSize.X, DisplayManager.WindowSize.Y-50, .6f,.4f,.1f,
                    DisplayManager.WindowSize.X, DisplayManager.WindowSize.Y,    .6f,.4f,.1f,
                    530, DisplayManager.WindowSize.Y,                            .6f,.4f,.1f,
            };
            return UIvert;
        }
    }
}
