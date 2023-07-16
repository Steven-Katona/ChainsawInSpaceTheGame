using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade1.GameStuff
{
    internal class Debris : Sprite //This class draws the enemies that are defeated, falling off screen.
    {
        protected int speed;
        protected (float, float) moving;
        public int maxH;
        public Debris(Texture2D thisTexture, int x, int y, int width, int height, float frame_speed, int count, bool face, int maxHeight) : base(thisTexture, x, y, width, height, frame_speed, count)
        {
            if(face)
            {
                moving = (.25f,-1);
            }
            else
            {
                moving = (-.25f, -1);
            }

            maxH= maxHeight;
        }

        public override void Update(GameTime gameTime)
        {
            (float, float) newMovement = (moving.Item1 * speed * (float)gameTime.ElapsedGameTime.TotalSeconds, moving.Item2 * speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            move_Position(newMovement);
            speed -= 10;
            if(getPosition().Y > maxH)
            {
                setDead();
            }
        }
    }
}
