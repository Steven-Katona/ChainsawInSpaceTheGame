using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace FakeArcade1.GameStuff
{
    
    public struct abstractpane
    {
        public int[] pane;
        int lengthCode;
        public void SetPane(int[] incomingData)
        { 
            pane = incomingData;
            lengthCode = pane.Length;
        }

        public int[] getpane
        {
            get { return pane; }
        }
            
        public int getcode
        {
            get { return lengthCode; }
        }


        // {phy,fire,ice,magic,sick,holy,dark}
    }
        
    
    
}

