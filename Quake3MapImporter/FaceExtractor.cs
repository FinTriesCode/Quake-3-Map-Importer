using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Quake3MapImporter
{
    public class FaceExtractor
    {
        public static List<Face> faceList = new List<Face>();

        public static List<Mesh> ExtractFaces(byte[] bspData)
        {

            //feed in byte data via a stream
            using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(bspData)))
            {
                //skip to faces directory
                binaryReader.ReadBytes(112);

                //read data for face(s) offset and length 
                int faceOffset = binaryReader.ReadInt32();
                int faceLength = binaryReader.ReadInt32();
                
                //relocate reader to offset of desired lump
                binaryReader.BaseStream.Position = faceOffset;

                
                List<Mesh> meshes = new List<Mesh>();
                
                //if steam is within lump
                    //read face data from stream and store in list
                while (binaryReader.BaseStream.Position < faceOffset + faceLength)
                {
                    Face face = ReadFace(binaryReader);
                    faceList.Add(face);
                }

                //process each face
                    //if face is a polygon, generate new mesh using previously stored face data and store result
                foreach (Face face in faceList)
                {
                    if(face.type == 1)
                    {
                        Mesh mesh;
                        mesh = GenerateMeshFromFace(face, binaryReader);
                        
                        meshes.Add(mesh);
                    }
                }

                return meshes;
            }
        }

        private static Face ReadFace(BinaryReader binaryReader)
        {
            Face face = new Face();
            face.texture = binaryReader.ReadInt32();
            face.effect = binaryReader.ReadInt32();
            face.type = binaryReader.ReadInt32();            
            
            face.vertex = binaryReader.ReadInt32();
            face.n_vertices = binaryReader.ReadInt32();
            
            face.meshvert = binaryReader.ReadInt32();
            face.n_meshvert = binaryReader.ReadInt32();
            
            face.lm_index = binaryReader.ReadInt32();
            face.lm_start[0] = binaryReader.ReadInt32();
            face.lm_start[1] = binaryReader.ReadInt32();
            
            face.lm_size[0] = binaryReader.ReadInt32();
            face.lm_size[1] = binaryReader.ReadInt32();
            
            face.lm_origin[0] = binaryReader.ReadSingle();
            face.lm_origin[1] = binaryReader.ReadSingle();
            face.lm_origin[2] = binaryReader.ReadSingle();

            for (int i = 0; i < face.lm_vecs.Length; i++)
            {
                for (int j = 0; j < face.lm_vecs[i].Length; j++)
                {
                    face.lm_vecs[i][j] = binaryReader.ReadSingle();
                }
            }

            face.normal[0] = binaryReader.ReadSingle();
            face.normal[1] = binaryReader.ReadSingle();
            face.normal[2] = binaryReader.ReadSingle();
            
            face.size[0] = binaryReader.ReadInt32();
            face.size[1] = binaryReader.ReadInt32();
        
            return face;
        }
        
        private static Mesh GenerateMeshFromFace(Face face, BinaryReader binaryReader)
        {
            List<Vector3> verticesList = new List<Vector3>();
            List<int> triangles = new List<int>();
            
            //relocate to desired lump in data stream (+ 8 to skip directory header)
            binaryReader.BaseStream.Seek(8+80,SeekOrigin.Begin); //
            
            //store offset
            int faceOffset = binaryReader.ReadInt32();
            
            binaryReader.BaseStream.Position = faceOffset + face.vertex * 44; //move to retarget stream

            //process each face 
                //read number of vertices and store 
                //add stored data into a Vec3 list
            for (int i = 0; i < face.n_vertices; i++)
            {
                //read from stream
                float x = binaryReader.ReadSingle(); 
                float y = binaryReader.ReadSingle();
                float z = binaryReader.ReadSingle();

                Vector3 n_vertices = new Vector3(x, y, z); //store read data from stream
                verticesList.Add(n_vertices); //add data from stream to list

                binaryReader.BaseStream.Position += 32; //reposition stream
            }
            
            
            //triangulate the polygon
            if (verticesList.Count < 3) return null;
        
            //store vertices as triangles wihthin a new int array
            for (int i = 0; i < verticesList.Count - 1; i++)
            {
                triangles.AddRange(new int[] {i + 1 , i, 0 });
            }
            
            for (int i = 0; i < verticesList.Count - 1; i++)
            {
                triangles.AddRange(new int[] {i + 1 , i, 0 });
            }
    
            Mesh mesh = new Mesh();
            mesh.SetVertices(verticesList);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}
