using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade1.GameStuff
{
    internal class Mainmenu
    {
        private int[] possibleSelection;
        private int currentSelection = 0;
        public Mainmenu(IServiceProvider service, GraphicsDeviceManager _graphics, Texture2D[] selections) 
        { 
            possibleSelection = new int[selections.Length];
            
            for(int setup_dummy_value = 0; setup_dummy_value < selections.Length; setup_dummy_value++)
            {
                possibleSelection[setup_dummy_value] = setup_dummy_value;
            }
            
        }

        public void Update(KeyboardState keys, GameTime gameTime)
        {

        }
    }
}
