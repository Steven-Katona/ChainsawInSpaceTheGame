using FakeArcade1.GameStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Globalization;
using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace FakeArcade1
{
    public class Game1 : Game
    {


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        RenderTarget2D _nativeTarget;
        Rectangle _nativeRectangle;
        Rectangle boxingRect;
        private Level currentLevel;
        bool gameover = false;

        int[][] levels = new int[10][];

        Texture2D success;
        Texture2D defeat;
        Texture2D gameend;
        Texture2D pushQ;
        Texture2D pushW;

        Rectangle sucRec;
        Rectangle defRec;
        Rectangle gamRec;
        Rectangle queRec;
        Rectangle wueRec;
        

        int[] currentLeveldata;
        int levelCounter = 0;
        int levelCount;

        // maxw code? maxh code? 
        int snk = 0;
       
     

        
        int skl = 1;
        int shp = 2;
        int cat = 3;

        int inst = 0;
        int quic = 1;
        int fast = 2;
        int slow = 5;
        
        float WindowAspect { get { return Window.ClientBounds.Width / Window.ClientBounds.Height; } }
        float nativeAspect;
        int gameWidth = 1600;
        int gameHeight = 900;
        double gameAspect;
        int maxHeight;
        int maxWidth;
        float widthScale;
        float heightScale;
        float gameScale;

        float xOffset;
        float yOffset;
        Matrix offsets;

        public Game1()
        {
            levels[0] = new int[] { slow, 4, 5, 20,  fast, skl, 50, 50 , inst, snk, 10, 0,  inst, snk, 20, 0,  inst, snk, 30, 0, fast, snk, 90, 0,  inst, snk, 80, 0,  inst, snk, 70, 0,  quic, skl, 85, 50};
            levels[1] = new int[] { slow, skl, 75, 75, inst, skl, 25, 75, fast, skl, 50, 75, fast, snk, 70, 0, inst, snk, 50, 0, inst, snk, 25, 0, inst, snk, 35, 0, inst, snk, 85, 0, fast, skl, 50, 75, inst, skl, 75, 75, quic, skl, 50, 80, inst, skl, 30, 85, inst, skl, 75, 90, quic, shp, 15, 90 };
            levels[2] = new int[] { slow, snk, 10, 0, inst, snk, 20, 0, inst, snk, 30, 0, inst,snk, 40, 0, quic, snk, 50, 0, inst, snk, 60, 0,  inst, snk, 70, 0,  inst, snk, 80, 0 ,  inst, skl, 25, 80,  inst, snk, 80, 0,  inst, snk, 90,  0, fast, skl, 50, 80, inst, skl, 25, 80, quic, snk, 10, 0 , inst, snk, 20, 0, inst, snk, 30, 0, inst, snk, 40, 0, quic, snk, 50, 0, inst, snk, 60, 0, inst, snk, 70, 0, inst, snk, 80, 0, quic, shp, 15, 90 };
            levels[3] = new int[] { fast, shp, 50, 50, quic, shp, 40, 60, quic, shp, 50, 80, quic, shp, 60, 90, quic, shp, 70, 50, quic, shp, 20, 30, quic, shp, 80, 30, quic, shp, 20, 30, quic, shp, 80, 30 , quic, snk, 40, 0, quic, snk, 55, 0, quic, snk, 70, 0 };
            levels[4] = new int[] { fast, shp, 50, 50, inst, snk, 20, 0, inst, snk, 30, 0, inst, snk, 40, 0, fast, shp, 40, 50, inst, shp, 60, 50, inst, skl, 80, 90, fast, snk, 50, 0, inst, snk, 60, 0, inst, snk, 70, 0, quic, shp, 50, 50, inst, shp, 40, 50, inst, shp, 60, 50, quic, snk, 80, 0, inst, snk, 90, 0, inst, skl, 20, 90, quic, shp, 50, 50, inst, shp, 40, 50, inst, shp, 60, 50 };
            levels[5] = new int[] { fast, cat, 50, 50, inst, cat, 40, 40, inst, cat, 30, 30, inst, snk, 60, 0, inst, snk, 70, 0, inst, snk, 80, 0, quic, 6, 5, 80 , quic, 4, 8, 10, quic, 4, 8, 10, quic, 6, 8, 10, inst, cat, 50, 95};
            levels[6] = new int[] { fast, cat, 15, 90, inst, skl, 50, 50, inst, skl, 60, 50, inst, skl, 70, 50, quic, snk, 10, 0, inst, cat, 85, 90, inst, cat, 15, 85, slow, 6, 7, 15, slow, 6, 7, 15, slow, 6, 8, 25};
            levels[7] = new int[] { slow, skl, 75, 75, inst, skl, 25, 75, fast, 4, 8, 10, fast, skl, 50, 80, inst, cat, 15, 90, fast, 4, 8, 10, fast, 5, 10, 60, 3, 4, 8, 10 };
            levels[8] = new int[] { fast, 7, 10, 10 , fast, 7, 10, 10 , fast, 7, 10, 10 , fast, shp, 50, 50, quic, shp, 40, 60, inst, skl, 50, 90, inst, cat, 85, 80 };
            levels[9] = new int[] { fast, 7, 20, 50, quic, shp, 40, 60, slow, 7, 20, 50, fast, 4, 8, 10, fast, 4, 8, 10, fast, 7, 30, 60 };

            levelCount = levels.Length;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            xOffset = 0;
            yOffset = 0;
            gameAspect = gameWidth / gameHeight;
            maxWidth = _graphics.PreferredBackBufferWidth;
            maxHeight = _graphics.PreferredBackBufferHeight;
            
            widthScale = maxWidth / gameWidth;
            heightScale = maxHeight / gameHeight;
            
            
            

            if(WindowAspect < gameAspect)
            {
                gameScale = heightScale;
                yOffset = (maxHeight - gameHeight * heightScale) / 2.0f;

            }
            else
            {
                gameScale = widthScale;
                xOffset = (maxWidth - gameWidth * widthScale) / 2.0f;
            }

            offsets = Matrix.CreateScale(gameScale) * Matrix.CreateTranslation(xOffset, yOffset, 0f);
            


            currentLeveldata = levels[levelCounter];
            currentLevel = new(Services, currentLeveldata , _graphics, (int)Math.Floor(maxWidth * .50d), (int)Math.Floor(maxHeight* .50d)); //fix this shit yo!
            
            Window.AllowUserResizing= true;

            _nativeRectangle = new Rectangle(0, 0, maxWidth, maxHeight);
            //nativeAspect = _nativeRectangle.Width / _nativeRectangle.Height;    
            _graphics.PreferredBackBufferWidth = _nativeRectangle.Width;
            _graphics.PreferredBackBufferHeight = _nativeRectangle.Height;
            _graphics.SynchronizeWithVerticalRetrace= true;
            _graphics.HardwareModeSwitch = true; //borderless window fullscreen
            _graphics.ApplyChanges();
            
            _nativeTarget = new RenderTarget2D(GraphicsDevice, _nativeRectangle.Width, _nativeRectangle.Height);
            boxingRect = _nativeRectangle;

            

            base.Initialize();

            
        }

        protected override void LoadContent()
        {

            success = Content.Load<Texture2D>("success");
            defeat = Content.Load<Texture2D>("defeat");
            gameend = Content.Load<Texture2D>("gameover");
            pushQ = Content.Load<Texture2D>("pushit");
            pushW = Content.Load<Texture2D>("pushit");

            sucRec = new((int)Math.Floor(maxWidth*.50d - success.Width / 2.0d), (int)Math.Floor(maxHeight* .50d - success.Height / 2.0d), success.Width, success.Height);
            defRec = new((int)Math.Floor(maxWidth * .50d - defeat.Width / 2.0d), (int)Math.Floor(maxHeight * .50d - defeat.Height / 2.0d), defeat.Width, defeat.Height);
            gamRec = new((int)Math.Floor(maxWidth * .50d - gameend.Width / 2.0d), (int)Math.Floor(maxHeight * .50d - gameend.Height / 2.0d), gameend.Width, gameend.Height);
            queRec = new(0, 0, pushQ.Width, pushQ.Height);
            wueRec = new(pushQ.Width, 0, pushQ.Width, pushQ.Height);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatch.Begin(transformMatrix: offsets);
            _spriteBatch.End();
            //currentLevel = new(Service,levels[levelCounter], _graphics);



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();

            

            if(currentLevel.level_done && levels.Length > levelCounter && !gameover)
            {

                levelCounter += 1;
                if (levelCount == levelCounter)
                {
                    gameover = true;
                }
                else
                {
                    float xpos = currentLevel.getPlayer().getPosition().X;
                    float ypos = currentLevel.getPlayer().getPosition().Y;
                    currentLevel.Dispose();
                    currentLeveldata = levels[levelCounter];
                    currentLevel = new(Services, currentLeveldata, _graphics, (int)xpos, (int)ypos);
                }
            }

            if(currentLevel.getPlayer().Dead() && currentLevel.RestartLevel())
            {
                float xpos = .50f * maxWidth;
                float ypos = .50f * maxHeight;
                currentLevel.Dispose();
                currentLevel = new(Services, currentLeveldata, _graphics, (int)xpos, (int)ypos);
            }

            


            currentLevel.Update(gameTime, kstate);
            
            // TODO: Add your update logic here
            //player.Update(gameTime,Keyboard.GetState(),GamePad.GetState(0), );
            base.Update(gameTime);


            //levelDX . collision detection here?

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_nativeTarget);
            GraphicsDevice.Clear(Color.Blue);
            
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            
            /*Color[] data = new Color[currentLevel.getPlayer().getHurtBox().Width * currentLevel.getPlayer().getHurtBox().Height];
            int maxDimension = currentLevel.getPlayer().getHurtBox().Width * currentLevel.getPlayer().getHurtBox().Height;
            Texture2D rectText = new Texture2D(GraphicsDevice, currentLevel.getPlayer().getHurtBox().Width, currentLevel.getPlayer().getHurtBox().Height);

            for (int i = 0; i < data.Length; i++)
            {
                if((i <= currentLevel.getPlayer().getHurtBox().Width) || (i >= (maxDimension - currentLevel.getPlayer().getHurtBox().Width)) || (i % currentLevel.getPlayer().getHurtBox().Width == 0) || (i % currentLevel.getPlayer().getHurtBox().Width == currentLevel.getPlayer().getHurtBox().Width - 1))
                {
                    data[i] = Color.Yellow;
                }
                
            }

            rectText.SetData(data);
            var ColorPos = new Vector2(currentLevel.getPlayer().getHurtBox().X, currentLevel.getPlayer().getHurtBox().Y);


            //_spriteBatch.Draw(player.myAnimation.Draw());
            _spriteBatch.Draw(rectText, ColorPos, Color.White);*/
            currentLevel.Draw(gameTime, _spriteBatch, GraphicsDevice);

            if (currentLevel.getPlayer().Dead())
            {
                _spriteBatch.Draw(defeat, defRec, Color.White);
            }

            if (!currentLevel.areEnemiesRemaining() && !gameover)
            {
                _spriteBatch.Draw(success, defRec, Color.White);
            }

            if (gameover)
            {
                _spriteBatch.Draw(gameend, gamRec, Color.White);
            }

            if (currentLevel.getPlayer().getAttackStatus() <= 0d && !currentLevel.getPlayer().inCombat())
            {
                _spriteBatch.Draw(pushQ, queRec, Color.White);
            }

            if (!currentLevel.getPlayer().activeWeapon && currentLevel.getPlayer().killCount >= 10)
            {
                _spriteBatch.Draw(pushW, wueRec, Color.White);
            }

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Blue);

            //_spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_nativeTarget, boxingRect, Color.White);
            _spriteBatch.End();



            base.Draw(gameTime);
        }

   
    }
}