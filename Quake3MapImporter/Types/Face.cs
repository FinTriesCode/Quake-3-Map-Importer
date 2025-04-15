using UnityEngine;
using System.Collections.Generic;

namespace Quake3MapImporter
{
    public class Face
    {
        public Face()
        {
            lm_vecs = new float[][] 
            {
                new float[] {0, 0, 0},
                new float[] {0, 0, 0}
            };
        }
        
        public int texture;
        public int effect;
        public int type;
        public int vertex;
        public int n_vertices;
        public int meshvert;
        public int n_meshvert;
        public int lm_index;
        public int[] lm_start = new int[2];
        public int[] lm_size = new int[2];
        public float[] lm_origin = new float[3];
        public float[][] lm_vecs;
        public float[] normal = new float[3];
        public int[] size = new int[2];
    }
}