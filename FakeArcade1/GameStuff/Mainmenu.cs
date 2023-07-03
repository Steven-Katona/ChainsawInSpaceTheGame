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
        int possibleChoices = 4;
        int startingChoice = 0;
        private int[] possibleSelection;
        private int currentSelection = 0;
        Texture2D[] menuItems;
        bool currentlyPressed = false;
        bool selectionMade = false;
        public bool exitGame { get; set; }
        public bool startGame { get; set; }
        Keys currentKey;
        Vector2 startingLocation;
        int maxW;
        int maxH;
        int spawning { get; set; }
        public Mainmenu(Texture2D[] selections, float ratio, int maxWidth, int maxHeight) 
        { 
            possibleSelection = new int[selections.Length];
            
            for(int setup_dummy_value = 0; setup_dummy_value < selections.Length; setup_dummy_value++)
            {
                possibleSelection[setup_dummy_value] = setup_dummy_value;
            }
            maxW = maxWidth;
            maxH = maxHeight;
            spawning = 1;
            possibleSelection = new int[possibleChoices];
            menuItems = selections;
            startingLocation = new(maxW * .5f,0);
            exitGame = false;
            startGame = false;
            Keys currentKey = Keys.None;
        }

        public void Update(KeyboardState keys, GameTime gameTime)
        {
            if(keys.IsKeyDown(Keys.NumPad2) && !currentlyPressed)
            {
                currentSelection = (currentSelection += 1) % possibleChoices;
                currentlyPressed = true;
                currentKey = Keys.NumPad2;
            }

            if(keys.IsKeyDown(Keys.NumPad8) && !currentlyPressed)
            {
                if(currentSelection > 0)
                {
                    currentSelection = (currentSelection -= 1) % possibleChoices + startingChoice;
                }
                else
                {
                    currentSelection = possibleChoices - 1;
                }
                currentlyPressed = true;
                currentKey = Keys.NumPad8;
            }

            if(keys.IsKeyDown(Keys.Enter) && !currentlyPressed)
            {
                (int, int) options = getSelection(currentSelection);
                possibleChoices = options.Item2;
                startingChoice = options.Item1;
                currentlyPressed = true;
                currentKey = Keys.Enter;
            }

            if(keys.IsKeyUp(currentKey))
            {
                currentlyPressed = false;
            }



            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float ratio, GraphicsDevice _graphics)
        {
            
            for(int texture = startingChoice; texture < possibleChoices; texture++)
            {

                spriteBatch.Draw(menuItems[texture], new Vector2(startingLocation.X - menuItems[texture].Width/2.0f, startingLocation.Y - menuItems[texture].Height/2.0f), Color.White);
                startingLocation.Y += maxH * .20f;
            }

            startingLocation.Y = maxH * .20f;

            Color[] data = new Color[menuItems[currentSelection].Width * menuItems[currentSelection].Height];
            int maxDimension = menuItems[currentSelection].Width * menuItems[currentSelection].Height;
            Texture2D rectText = new Texture2D(_graphics, menuItems[currentSelection].Width, menuItems[currentSelection].Height);
            for (int i = 0; i < data.Length; i++)
            {
                if( (i % menuItems[currentSelection].Width == 0) || (i % menuItems[currentSelection].Width == menuItems[currentSelection].Width - 1))
                {
                    data[i] = Color.Yellow;
                }
                //(i <= menuItems[currentSelection].Width) || (i >= (menuItems[currentSelection].Width)) ||
            }

            rectText.SetData(data);
            var ColorPos = new Vector2(menuItems[currentSelection].Bounds.X, menuItems[currentSelection].Bounds.Y);



            spriteBatch.Draw(rectText, ColorPos, Color.White);
        }

        public (int, int) getSelection(int selection)
        {
            int returnValue = 0;
            int displayValue = 4;
            switch (selection)
            {
                case 0:

                    returnValue = 1;
                    displayValue = 0;
                    startGame = true;
                    break;

                case 1:

                    returnValue = 4;
                    displayValue = 6;
                    break;

                case 2:
                    
                    returnValue = 10;
                    displayValue = 4;
                    break;

                case 3:

                    returnValue = 2;
                    displayValue = 0;
                    exitGame = true;
                    break;

                case 4:
                    returnValue = 0;
                    displayValue = 4;
                    spawning = 1;
                    break;

                case 5:
                    returnValue = 0;
                    displayValue = 4;
                    spawning = 2;
                    break;

                case 6:
                    returnValue = 0;
                    displayValue = 4;
                    spawning = 3;
                    break;

                case 7:
                    returnValue = 0;
                    displayValue = 4;
                    spawning = 4;
                    break;

                case 8:
                    returnValue = 0;
                    displayValue = 4;
                    spawning = 5;
                    break;

                case 9:
                    returnValue = 0;
                    displayValue = 4;
                    spawning = 6;
                    break;

                case 10:
                    returnValue = 0;
                    displayValue = 4;
                    spawning = 1;
                    break;

                case 13:
                    returnValue = 0;
                    displayValue = 4;
                    spawning = 1;
                    break;











                default:
                    returnValue = startingChoice;
                    displayValue = possibleChoices;
                    break;
            }

            return (returnValue, displayValue);
        }

        public int[] resizeSelection(int resize)
        {
            int[] selection = new int[resize];
            return selection;
        }


    }
}
