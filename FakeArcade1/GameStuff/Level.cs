using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using System.Runtime.CompilerServices;

namespace FakeArcade1.GameStuff
{
    
    internal class Level : IDisposable
    {
        private Player mainCharacter;
        private static List<Sprite> enemy_squad;
        private static List<Sprite> to_be_Removed;
        private static List<Sprite> to_be_Added;
        ContentManager content;
        private double[] time_current;
        private double[] target_time;
        public bool level_done = false;
        bool[] more_to_spawn;
        bool final_stretch = false;
        Queue<int>[] extra_Spawns;
        bool check_Facing = false;
        int maxWidth;
        int maxHeight;
        Texture2D[] background = new Texture2D[3];
        double backgroundSpeed;
        double backgroundTimer;
        int backgroundCounter;
        private bool resetLevel = false;
        Rectangle backgroundRect;
        int spawnSettings;
        double wait_time;









        public Level(IServiceProvider service, int[] monster_data, GraphicsDeviceManager _graphics, int x, int y, int spawnMultiplier, float ratio)
        {
            content = new(service , "Content");
            Texture2D chainsaw;
            chainsaw = content.Load<Texture2D>("realSaw");
            enemy_squad = new List<Sprite>();
            to_be_Removed = new List<Sprite>();
            to_be_Added= new List<Sprite>();
            maxHeight = _graphics.PreferredBackBufferHeight;
            maxWidth= _graphics.PreferredBackBufferWidth;
            background[0] = content.Load<Texture2D>("page1");
            background[1] = content.Load<Texture2D>("page2");
            background[2] = content.Load<Texture2D>("page3");
            backgroundSpeed = .70d;
            backgroundTimer = 0d;
            backgroundCounter = 0;
            backgroundRect = new(0,0,maxWidth,maxHeight);
            spawnSettings = spawnMultiplier;

            extra_Spawns = new Queue<int>[spawnSettings];
            for(int initilize = 0; initilize < spawnSettings; initilize++)
            {
                extra_Spawns[initilize] = new Queue<int>();
            }

            more_to_spawn = new bool[spawnSettings];
            target_time = new double[spawnSettings];
            time_current = new double[spawnSettings];
            wait_time = 7.0;


            foreach (int item in monster_data)
            {

                for(int extra = 0; extra < spawnSettings; extra++)
                {    
                    int value = item;
                    extra_Spawns[extra].Enqueue(value);
                }

            }

            for (int spawn = 0; spawn < spawnSettings; spawn++)
            {
                target_time[spawn] = extra_Spawns[spawn].Dequeue() + spawn;
                more_to_spawn[spawn] = true;
            }

            mainCharacter = new Player(chainsaw, x, y, 160, 64, .20f, 3);
            Texture2D weapon = content.Load<Texture2D>("theWeapon_sheet");
            mainCharacter.setUpWeapon(weapon, maxWidth, maxHeight);
        }

        public Player getPlayer()
        {
            return mainCharacter;
        }

       

        public ContentManager GetContent() { return content; }

        public bool RestartLevel()
        {
            return resetLevel;
        }

        public bool areEnemiesRemaining()
        {
            bool any_more_monsters = false;


            foreach(var spawner in more_to_spawn)
            {
                if(spawner == true)
                {
                    any_more_monsters = true;
                }
            }


            if (enemy_squad.Count == 0 && !any_more_monsters)
                return false;
            else
                return true;
        }

   
        public void Update(GameTime gameTime,KeyboardState keyboardStat)
        {
            int finale = 0;
            for (int spawn = 0; spawn < spawnSettings; spawn++)
            {
                time_current[spawn] += (double)gameTime.ElapsedGameTime.TotalSeconds;
                finale += extra_Spawns[spawn].Count();

                if (time_current[spawn] > target_time[spawn] && more_to_spawn[spawn])
                {
                    Dynamic_Monster_Spawn(extra_Spawns[spawn].Dequeue(), extra_Spawns[spawn].Dequeue(), extra_Spawns[spawn].Dequeue());

                    if (extra_Spawns[spawn].Count() != 0)
                    {
                        target_time[spawn] = extra_Spawns[spawn].Dequeue() + spawn;
                        time_current[spawn] = 0;
                    }
                    else
                    {

                        more_to_spawn[spawn] = false;
                    }
                }
            }

            if(mainCharacter.Dead() && keyboardStat.IsKeyDown(Keys.R))
            {
                resetLevel = true;
            }

            if(mainCharacter.Dead() && mainCharacter.getPosition().X <= maxWidth && mainCharacter.getPosition().X >= 0)
            {
                mainCharacter.setPosition(-500, -500);
                mainCharacter.moveHurtBox((-500.0f, -500.0f));
            }


            if (!check_Facing.Equals(mainCharacter.getFacing()) && !mainCharacter.Dead())
            {
                if(mainCharacter.getFacing().Equals(true))
                {
                    Animation newSaw = new(content.Load<Texture2D>("realSawLeft"), .20f, true, 160, 3);
                    mainCharacter.givePlayerAnimation(newSaw);
                    check_Facing = true;
                }
                else
                {
                    Animation newSaw = new(content.Load<Texture2D>("realSaw"), .20f, true, 160, 3);
                    mainCharacter.givePlayerAnimation(newSaw);
                    check_Facing = false;
                }
            }


            if (finale == 0 && enemy_squad.Count == 0)
            {
                if(wait_time > 0)
                {
                    wait_time -= gameTime.ElapsedGameTime.TotalSeconds; // let some time pass before entering the next level
                }
                else
                {
                    final_stretch = true;
                }


                if (final_stretch)
                {
                    level_done = true;
                }
            }







            mainCharacter.Update(gameTime, keyboardStat, maxWidth, maxHeight);
            foreach (Sprite enemy in enemy_squad)
            {
                if (enemy as Projectile != null) // projectile hitbox logic
                {
                    Projectile bull = (Projectile)enemy;
                    if (bull.getBounds().Intersects(mainCharacter.getBounds()) || bull.getBounds().Intersects(mainCharacter.getHurtBox()))
                    {
                        mainCharacter.setDead();
                    }



                    if (bull.checkOutOfBounds())
                    {
                        to_be_Removed.Add(bull);
                    }
                }


                if(enemy as Enemy != null)
                {
                    Enemy this_enemy = (Enemy)enemy;

                    
                    if(this_enemy.getBounds().Intersects(mainCharacter.theWeapon.getBounds()))
                    {
                        this_enemy.setDead();
                        if (this_enemy.getDeadTexture() != null)
                        {
                            to_be_Added.Add(new Debris(this_enemy.getDeadTexture(), (int)this_enemy.getPosition().X, (int)this_enemy.getPosition().Y, this_enemy.getDeadTexture().Width, this_enemy.getDeadTexture().Height, 50f, 2, mainCharacter.getFacing(), maxHeight));
                        }
                        mainCharacter.killCount++;
                    }

                    if (this_enemy.willEnemyShoot())
                    {
                        Bullet_Spawn((int)this_enemy.getPosition().X, (int)this_enemy.getPosition().Y);
                        this_enemy.resetShoot();
                    }

                    if (this_enemy.getBounds().Intersects(mainCharacter.getBounds()) && !mainCharacter.inCombat()) //general hitbox logic
                    {
                        mainCharacter.setDead();
                    }

                    if (this_enemy.getBounds().Intersects(mainCharacter.getHurtBox()))
                    { 
                        this_enemy.setDead();
                        if (this_enemy.getDeadTexture() != null)
                        {
                            to_be_Added.Add(new Debris(this_enemy.getDeadTexture(), (int)this_enemy.getPosition().X, (int)this_enemy.getPosition().Y, this_enemy.getDeadTexture().Width, this_enemy.getDeadTexture().Height, 50f, 2, mainCharacter.getFacing(), maxHeight));
                        }
                        mainCharacter.killCount++;
                    }

                       

                    if (this_enemy.pre_update_logic_options == 1)
                    {
                        this_enemy.preUpdateLogic(gameTime, mainCharacter);
                    }
                    else if(this_enemy.pre_update_logic_options == 2)
                    {
                        this_enemy.preUpdateLogic(gameTime, content, mainCharacter);
                    }
                }


                if(enemy.Dead())
                {
                    to_be_Removed.Add(enemy);
                }

                enemy.Update(gameTime);
            }   //end of enemy List foreach!


            foreach (Sprite trash in to_be_Removed)
            {
               enemy_squad.Remove(trash);
            }

            foreach (Sprite spawn in to_be_Added)
            {
                enemy_squad.Add(spawn);
            }

            to_be_Removed.Clear();
            to_be_Added.Clear();
        }

        public void Dynamic_Monster_Spawn(int which_monster, int y_loc, int trigger_control)
        {
            int steps;
            switch (which_monster) 
            {
                case 0:
                    Texture2D smake = content.Load<Texture2D>("snake_sheet");
                    Texture2D deadsmake = content.Load<Texture2D>("deadSnake");
                    Snake snk = new (smake, (int)Math.Ceiling(maxWidth + 255 / 2.0f), (int)Math.Ceiling(y_loc * 0.01f * maxHeight), 255, 64, (int)Math.Ceiling(trigger_control * maxWidth * 0.01f), 3, 0.20f, new Vector2(mainCharacter.getPosition().X, mainCharacter.getPosition().Y), maxWidth, maxHeight);
                    snk.setDeathTexture(deadsmake);
                    to_be_Added.Add(snk);
                    break;//use x_max and y_max as the generic starting points for the monster but use a more robust method to spawn

                case 1:
                    Texture2D smull = content.Load<Texture2D>("planet_battery_sheet");
                    Texture2D deadsmull = content.Load<Texture2D>("deadPlanet");
                    Skull skl = new (smull, (int)Math.Ceiling(maxWidth + 175 / 2.0f), (int)Math.Ceiling(y_loc * 0.01f * maxHeight), 175, 175, (int)Math.Ceiling(trigger_control * maxWidth * 0.01f), 5, 0.20f, new Vector2(mainCharacter.getPosition().X, mainCharacter.getPosition().Y), maxWidth, maxHeight);
                    skl.setDeathTexture(deadsmull);
                    to_be_Added.Add(skl);
                    break;

                case 2:
                    Texture2D smip = content.Load<Texture2D>("export_spike_ship");
                    Texture2D deadsmip = content.Load<Texture2D>("deadShip");
                    Ship shp = new(smip, (int)Math.Ceiling(maxWidth + 160 / 2.0f), (int)Math.Ceiling(y_loc * 0.01f * maxHeight), 160, 130, (int)Math.Ceiling(trigger_control * maxWidth * 0.01f), 4, 0.20f, new Vector2(mainCharacter.getPosition().X, mainCharacter.getPosition().Y), maxWidth, maxHeight);
                    shp.setDeathTexture(deadsmip);
                    to_be_Added.Add(shp);
                    break; 
                case 3:
                    Texture2D cat = content.Load<Texture2D>("cat_flower");
                    Texture2D deadCat = content.Load<Texture2D>("deadCat");
                    Cat catz = new(cat, (int)Math.Ceiling(maxWidth + 175 / 2.0f), (int)Math.Ceiling(y_loc * 0.01f * maxHeight), 175, 225, (int)Math.Ceiling(trigger_control * maxWidth * 0.01f), 6, 0.20f, new Vector2(mainCharacter.getPosition().X, mainCharacter.getPosition().Y), maxWidth, maxHeight);
                    catz.setDeathTexture(deadCat);
                    to_be_Added.Add(catz);
                    break;
               case 4:
                    steps = trigger_control;
                    for(int x = 0; x < y_loc; x++)
                    {
                        Dynamic_Monster_Spawn(0, steps, 0);
                        steps += trigger_control;
                        steps = steps % 90;
                        if (steps < 10)
                        {
                            steps = 10;
                        }
                    }
                    break;
                case 5:
                     steps = trigger_control;
                    for(int x = 0; x < y_loc; x++)
                    {
                        Dynamic_Monster_Spawn(1, steps, trigger_control);
                        steps += trigger_control;
                        steps = steps % 90;
                        if(steps < 10)
                        {
                            steps = 10;
                        }
                    }

                    break;

                
                case 6:
                    steps = trigger_control;
                    for (int x = 0; x < y_loc; x++)
                    {
                        Dynamic_Monster_Spawn(2, steps, trigger_control);
                        steps += trigger_control;
                        steps = steps % 90;
                        if (steps < 10)
                        {
                            steps = 10;
                        }
                    }
                    break;

                case 7:
                    steps = trigger_control;
                    for (int x = 0; x < y_loc; x++)
                    {
                        int which = x % 4;
                        Dynamic_Monster_Spawn(which, steps, trigger_control);
                        steps += trigger_control;
                        steps = steps % 90;
                        if (steps < 10)
                        {
                            steps = 10;
                        }
                    }
                    break;

                default: break; 
            }
        }

        public void Bullet_Spawn(int x, int y)
        {
            
            Texture2D bullet = content.Load<Texture2D>("bullet_sprite_sheet");
            Projectile dangerous_bullet = new Projectile(bullet, x, y, 78, 64, 0, 4, .20f, new Vector2((float)mainCharacter.getPosition().X, (float)mainCharacter.getPosition().Y), maxWidth, maxHeight);
            dangerous_bullet.myAnimationPlayer.setDrawPriority(1.0f);
            to_be_Added.Add(dangerous_bullet);
                
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice grap)
        {

            spriteBatch.Draw(background[backgroundCounter], backgroundRect, Color.White);
            backgroundTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (backgroundTimer > backgroundSpeed)
            {
                backgroundCounter += 1;
                backgroundCounter = backgroundCounter % background.Length;
                backgroundTimer = 0;
            }//Basic background logic. Only supports one background.


            if (!mainCharacter.Dead())
            {
                mainCharacter.Draw(gameTime, spriteBatch);
            }//I character is alive draw them to the screen!

            if (mainCharacter.activeWeapon || !mainCharacter.activeWeapon)
            {
                mainCharacter.theWeapon.Draw(gameTime, spriteBatch);
            }

            foreach (Sprite snk in enemy_squad)
            {
                
                 

                snk.Draw(gameTime, spriteBatch);
                
                


                /*Color[] data = new Color[snk.getBounds().Width * snk.getBounds().Height];
                int maxDimension = snk.getBounds().Width * snk.getBounds().Height;
                Texture2D rectText = new Texture2D(grap , snk.getBounds().Width, snk.getBounds().Height);

                for (int i = 0; i < data.Length; i++)
                {
                    if ((i <= snk.getBounds().Width) || (i >= (maxDimension - snk.getBounds().Width)) || (i % snk.getBounds().Width == 0) || (i % snk.getBounds().Width == snk.getBounds().Width - 1))
                    {
                        data[i] = Color.Yellow;
                    }
                }

                rectText.SetData(data);
                var ColorPos = new Microsoft.Xna.Framework.Vector2(snk.getBounds().X, snk.getBounds().Y);


                //_spriteBatch.Draw(player.myAnimation.Draw());
                spriteBatch.Draw(rectText, ColorPos, Color.White);*/
            }

        }
        
        public void Dispose()
        {
            GetContent().Unload();
        }
    }
}
