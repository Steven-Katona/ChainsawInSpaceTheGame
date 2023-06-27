using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade1.GameStuff
{
    internal class Animation
    {
        Texture2D texture;
        float frameTime;
        bool isLooping;
        int frameCount;
        int size_offset;

        public Texture2D getTexture() { return texture; }
        public bool getLoopInfo() { return isLooping; }
        public int getFrameCount() { return frameCount; }
        public float getFrametime()
        { 
            return frameTime;
        }
        public int getSize_offset()
        {
            return size_offset;
        }

       
        public Animation(Texture2D texture, float frameTime, bool isLooping, int size_offset, int count)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
            this.size_offset = size_offset;
            double frameC = (size_offset / getTexture().Bounds.Height);
            this.frameCount= count;

          
        }
    }
}
