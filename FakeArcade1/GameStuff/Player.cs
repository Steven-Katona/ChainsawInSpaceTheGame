using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.CompilerServices;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace FakeArcade1.GameStuff
{
    internal class Player : Sprite
    {
        bool combat = false;
        float chainSpd = 200;
        Microsoft.Xna.Framework.Point direction;
        
        Vector2 return_point;
        bool rebound = false;
        int variable_speed;
        Keys go_up = Keys.NumPad8;
        Keys go_down = Keys.NumPad2;
        Keys go_left = Keys.NumPad4;
        Keys go_right = Keys.NumPad6;
        Keys go_up2 = Keys.Up;
        Keys go_down2 = Keys.Down;
        Keys go_left2 = Keys.Left;
        Keys go_right2 = Keys.Right;
        Keys lastpressed;
        Dictionary<Keys, (int, int)> where_to_go;
        (int, int) current_movement;
        double previous_length;
        bool facing = false;
        private Rectangle hurtBox;
        private double cooldown;
        public int killCount { get; set; }
        public bool activeWeapon { get; set; }
        public Projectile theWeapon { get; set; }


        public int getKillCount()
        { return killCount; }


        public void generic_To_Do()
        {
            variable_speed = 4;
            current_movement = (0, 0);
            where_to_go = new Dictionary<Keys, (int, int)>
            {
                { go_up, (0, -1) },
                { go_down, (0, 1) },
                { go_right, (1, 0) },
                { go_left, (-1, 0) },
                { go_up2, (0, -1) },
                { go_down2, (0, 1) },
                { go_right2, (1, 0) },
                { go_left2, (-1, 0) }

            };

            lastpressed = go_right;
            direction = new Point(0, 1);
            setRectangle(new Rectangle((int)getPosition().X - 52, (int)getPosition().Y - (int)getHeight() / 2, 30, 30));
            hurtBox = new((int)getPosition().X - 22, (int)getPosition().Y - (int)getHeight() / 2, 84, 35);
            cooldown = 0;
            killCount= 0;
            activeWeapon= false;
        }

        public void setUpWeapon(Texture2D weaponImage, int maxWidth, int maxHeight)
        {
            theWeapon = new(weaponImage, -500, -500, 175, 175, 0, 4, .20f, new(0,0), maxWidth, maxHeight);
            theWeapon.setSpeed(0);
        }
     

        public Player(Texture2D thisTexture, Rectangle bounds) : base(thisTexture,bounds)
        {
            generic_To_Do();
        }

        public Player(Texture2D thisTexture, int x, int y, int realWidth, int realHeight, float frame_speed, int count) : base(thisTexture, x,y,realWidth,realHeight, frame_speed, count)
        {
            //scl = .8;
            generic_To_Do();
        }

        public bool getFacing()
        {
            return facing;
        }

        public void setFacing(bool facing)
        {
            this.facing = facing;
        }

        public void setDirection(Point newDirection)
        {
            this.direction = newDirection;
        }

        public bool inCombat()
        {
            return combat;
        }

        public double getAttackStatus()
        {
            return cooldown;
        }
        public override void Update(GameTime gameTime) { }
        public void Update(GameTime gameTime, KeyboardState keyboardState, int maxWidth, int maxHeight)
        {

            if (!combat)
            {
                cooldown = cooldown -= gameTime.ElapsedGameTime.TotalSeconds;

                if (keyboardState.IsKeyDown(go_right) || keyboardState.IsKeyDown(go_right2))
                {
                    where_to_go.TryGetValue(go_right, out current_movement);

                    if(lastpressed == go_left)
                    {
                        facing = false; //if facing is false, we are going right
                    }

                    lastpressed = go_right;
                }

                if (keyboardState.IsKeyDown(go_left) || keyboardState.IsKeyDown(go_left2))
                {
                    where_to_go.TryGetValue(go_left, out current_movement);

                    if (lastpressed == go_right)
                    {
                        facing = true; //if facing is true, we are going left
                    }

                    lastpressed = go_left;
                }

                if (keyboardState.IsKeyDown(go_up) || keyboardState.IsKeyDown(go_up2))
                {
                    where_to_go.TryGetValue(go_up, out current_movement);
                }

                if (keyboardState.IsKeyDown(go_down) || keyboardState.IsKeyDown(go_down2))
                {
                    where_to_go.TryGetValue(go_down, out current_movement);
                }

                if (keyboardState.GetPressedKeyCount() >= 2)
                {
                    Keys[] currentPressed = keyboardState.GetPressedKeys();
                    (int, int) move_buffer = (0,0);

                    foreach (Keys ke in currentPressed)
                    {
                        
                        where_to_go.TryGetValue(ke, out current_movement);
                        move_buffer = (move_buffer.Item1 + current_movement.Item1, move_buffer.Item2  + current_movement.Item2);
                        current_movement = move_buffer;
                    }
                }

                if(lastpressed == go_right || lastpressed == go_right2) 
                { 
                    setRectangle(new Rectangle((int)getPosition().X - 52, (int)getPosition().Y - (int)getHeight() / 2, 30, 30));
                    setHurtBox(new Rectangle((int)getPosition().X - 22, (int)getPosition().Y - (int)getHeight() / 2, 84, 35));
                }
                if(lastpressed == go_left || lastpressed == go_left2) 
                {
                    setRectangle(new Rectangle((int)getPosition().X + 22, (int)getPosition().Y - (int)getHeight() / 2, 30, 30));
                    setHurtBox(new Rectangle((int)getPosition().X - 56, (int)getPosition().Y - (int)getHeight() / 2, 84, 35));
                }

                if(keyboardState.IsKeyDown(Keys.W) && killCount >= 10 && !activeWeapon)
                {
                    killCount = killCount - 10;
                    activeWeapon = true;
                    theWeapon.setPosition((int)this.getPosition().X, (int)this.getPosition().Y);
                    //Vector2 testMe = theWeapon.getPosition();
                    if(current_movement != (0,0))
                    {
                        theWeapon.movementDirection = current_movement;
                    }
                    else
                    {
                        if(facing)
                        {
                            theWeapon.movementDirection = (-1,0);
                        }
                        else
                        {
                            theWeapon.movementDirection = (1,0);
                        }
                    }
                    
                    theWeapon.setSpeed(800);

                }





                if (!((getPosition().X + current_movement.Item1 * chainSpd * (float)gameTime.ElapsedGameTime.TotalSeconds) > maxWidth) && !(getPosition().Y + (current_movement.Item2 * chainSpd * (float)gameTime.ElapsedGameTime.TotalSeconds) > maxHeight))
                {
                    do_Movement_set_Direction(current_movement, gameTime);
                }

                if (keyboardState.IsKeyDown(Keys.Q) && cooldown <= 0)
                {
                    combat = true;
                    return_point = new((int)getPosition().X, (int)getPosition().Y);
                    current_movement = (0, 0);
                }

                current_movement = (0,0);


            }
            else
            {
                Vector2 position_check = getPosition();
                if (!rebound &&(position_check.X > 0 && position_check.X < maxWidth) && (position_check.Y > 0 && position_check.Y < maxHeight))
                {
                    moveAllBoxes((0 + direction.X * chainSpd * variable_speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 3, 0 + direction.Y * chainSpd * variable_speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 3));
                }
                else
                {
                    rebound = true;
                    previous_length = (Math.Sqrt(Math.Pow(return_point.X - position_check.X, 2) + Math.Pow(return_point.Y - position_check.Y, 2)));
                }

                if(rebound)
                {
                    moveAllBoxes((0 + direction.X * chainSpd * -variable_speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0 + direction.Y * chainSpd * -variable_speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
                    position_check = getPosition();
                    double current_current = Math.Sqrt(Math.Pow(return_point.X - position_check.X, 2) + Math.Pow(return_point.Y - position_check.Y, 2));

                    if (current_current <= previous_length)
                    {
                        previous_length = current_current;
                    }
                    else
                    {
                        combat = false;
                        rebound = false;
                        previous_length = 0;
                        cooldown = 3;
                    }
                }

            }

            if(Dead())
            {
                chainSpd = 0; // you can not move if you are dead!
            }

            if (activeWeapon)
            {
                theWeapon.Update(gameTime);

                if (theWeapon.checkOutOfBounds())
                {

                    theWeapon.setPosition(-500, -500);
                    theWeapon.movementDirection = (0, 0);
                    theWeapon.setSpeed(0);
                    activeWeapon = false;
                    theWeapon.outOfBounds = false;
                }

            }
        }




        public Rectangle getHurtBox()
        {
            return hurtBox;
        }

        public void moveHurtBox((float, float) movement)
        {
            this.hurtBox.X += (int)Math.Ceiling(movement.Item1);
            this.hurtBox.Y += (int)Math.Ceiling(movement.Item2);
        }

        public void setHurtBox(Rectangle box)
        {
            hurtBox = box;
        }


        public void moveAllBoxes((float,float) movement)
        {
            moveHurtBox(movement);
            move_Position(movement);
            
        }

        public void do_Movement_set_Direction((int,int) where, GameTime gameTime)
        {
            
            (float, float) moving = new(where.Item1 * chainSpd * (float)gameTime.ElapsedGameTime.TotalSeconds, where.Item2 * chainSpd * (float)gameTime.ElapsedGameTime.TotalSeconds);
            moveAllBoxes(moving);
            
            
            if (!where.Equals((0, 0)))
            {
                setDirection(new(where.Item1, where.Item2));
            }
        }      

    }
}
   
