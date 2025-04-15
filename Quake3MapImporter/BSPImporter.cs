using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Quake3MapImporter
{
    public class Importer : MonoBehaviour
    {
        void Start()
        {
            //read data from file into a byte array
                //store meshes from file into a list
            byte[] bspData = File.ReadAllBytes("Assets/tig_den.bsp");
            List<Mesh> meshes = FaceExtractor.ExtractFaces(bspData);
            
            foreach (Mesh mesh in meshes)
            {
                //create obj and transorm accordingly
                GameObject obj = new GameObject("MapPart");
                obj.transform.parent = transform;
                
                
                //set up mesh properties
                var meshColour = Color.gray;
                var material = new Material(Shader.Find("Standard"));
                
                var meshRenderer = obj.AddComponent<MeshRenderer>();
                meshRenderer.material = material;
                //meshRenderer.material.color = meshColour;
                
                MeshFilter filter = obj.AddComponent<MeshFilter>();
                filter.mesh = mesh;

                obj.transform.Rotate(-90, 0, 0);
            }
        }
    }
}