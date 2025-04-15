using System.Collections.Generic;
using System.Text;

namespace Quake3MapImporter
{
    public class FaceLump
    {
        public Face[] Faces { get; set; }

        public FaceLump(int faceCount)
        {
            Faces = new Face[faceCount];
        }
    }
}