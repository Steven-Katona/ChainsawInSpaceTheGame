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
        Texture2D backgroundTile;
        bool currentlyPressed = false;
        //bool selectionMade = false;
        public bool exitGame { get; set; }
        public bool startGame { get; set; }
        Keys currentKey;
        Vector2 startingLocation;
        Vector2[] menuLocations;
        Animation myCursor;
        AnimationLogic cursorDraw;
        int maxW;
        int maxH;
        float ratioScale;
        public int spawning { get; set; }
        Vector2 backgroundStart = new(0);
        Vector2 centerofScreen;
        public Mainmenu(Texture2D[] selections, float ratio, int maxWidth, int maxHeight, Texture2D cursor, Texture2D background) 
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
            myCursor = new(cursor, .20f, true, 160 ,3);
            cursorDraw= new();
            cursorDraw.animationPlay(myCursor);

            menuLocations = new Vector2[6];

            menuLocations[0] = new Vector2(maxW * .10f, maxH * .10f);
            menuLocations[1] = new Vector2(maxW * .10f, maxH * .30f);
            menuLocations[2] = new Vector2(maxW * .10f, maxH * .50f);
            menuLocations[3] = new Vector2(maxW * .40f, maxH * .30f);
            menuLocations[4] = new Vector2(maxW * .40f, maxH * .50f);
            menuLocations[5] = new Vector2(maxW * .40f, maxH * .70f);

            centerofScreen = new(maxW - (maxW / 2.0f), maxH - (maxH / 2.0f));
            backgroundTile = background;
            ratioScale = (maxH * maxW) / (backgroundTile.Width * backgroundTile.Height);
        }

        public void Update(KeyboardState keys, GameTime gameTime)
        {
            if ((keys.IsKeyDown(Keys.NumPad2) || (keys.IsKeyDown(Keys.Down))) && !currentlyPressed)
            {
                if (currentSelection < (startingChoice + possibleChoices))
                {
                    currentSelection = (currentSelection += 1);
                    currentlyPressed = true;

                    if (keys.IsKeyDown(Keys.NumPad2))
                        currentKey = Keys.NumPad2;
                    else
                    {
                        currentKey = Keys.Down;
                    }
                }
            }

            if((keys.IsKeyDown(Keys.NumPad8) || (keys.IsKeyDown(Keys.Up))) && !currentlyPressed)
            {
                if (currentSelection > startingChoice)
                {
                    currentSelection -= 1;
                    currentlyPressed = true;

                    if(keys.IsKeyDown(Keys.NumPad8))
                        currentKey = Keys.NumPad8;
                    else
                    {
                        currentKey = Keys.Up;
                    }
                }
            }

            if(keys.IsKeyDown(Keys.Enter) && !currentlyPressed)
            {
                (int, int) options = getSelection(currentSelection);
                possibleChoices = options.Item2;
                startingChoice = options.Item1;
                currentSelection = startingChoice;
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
            int locations = 0;

            spriteBatch.Draw(backgroundTile, backgroundStart, new Rectangle(0,0,maxW, maxH), Color.White, 0f, centerofScreen, 3, SpriteEffects.None, 0f);

            for(int texture = startingChoice; texture < startingChoice + possibleChoices; texture++)
            {
                spriteBatch.Draw(menuItems[texture], menuLocations[locations], Color.White); // draws only the items that are within 
                if (texture == currentSelection)
                {
                    cursorDraw.Draw(gameTime, spriteBatch, menuLocations[locations], SpriteEffects.None);
                }
                locations++;
            }


        }

        public (int, int) getSelection(int selection) //menu is represented as an array of textures that are selected and displayed using this select statement.
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
                    displayValue = 3;
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
         
                    break;

                case 13:
                    returnValue = 0;
                    displayValue = 4;
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
