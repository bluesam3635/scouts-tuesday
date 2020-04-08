using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityStandardAssets._2D;
using System.Drawing;

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  maps/[NAME].txt format:
//      e(1,2)name
//      OR
//      t(1,2):name
//      one per line
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     public struct Vector2Int
     {
         public int x;
         public int y;
     
         public Vector2Int(int x, int y)
         {
             this.x = x;
             this.y = y;
         }
     }
    public struct Vector3Int
     {
         public int x;
         public int y;
         public int z;
     
         public Vector3Int(int x, int y, int z)
         {
             this.x = x;
             this.y = y;
             this.z = z;
         }
     }


    public class MapLoader : MonoBehaviour {
        [SerializeField] GameObject cam;
        void Start()
        {
            string detailFile = "Assets/Resources/maps/map.txt";
            BuildTable(detailFile);
            LoadMap("map"); // TODO: allow different maps to be loaded
        }
        public void EndLevel()
        {
            Debug.Log("Level Ended!");
        }
        
        Dictionary<Vector3Int,string> types;
        private void BuildTable(string file)
        {
            StreamReader reader = new StreamReader(file);
            types = new Dictionary<Vector3Int,string>();
            string line;
            int l = 0;
            while ((line = reader.ReadLine()) != null)
            {
                l += 1;
                int reading = 0;
                int r = 0;
                int g = 0;
                int b = 0;
                string name = "";
                for (int i = 0; i < line.Length; ++i)
                {
                    if (""+line[i] == "(")
                    {
                        reading = 1;
                    }
                    else if (reading == 1)
                    {
                        if (""+line[i] == ",")
                        {
                            reading = 2;
                        }
                        else
                        {
                            r = 10*r + int.Parse(""+line[i]);
                        }
                    }
                    else if (reading == 2)
                    {
                        if (""+line[i] == ",")
                        {
                            reading = 3;
                        }
                        else 
                        {
                            g = 10*g + int.Parse(""+line[i]);
                        }
                    }
                    else if (reading == 3)
                    {
                        if (""+line[i] == ")")
                        {
                            reading = 4;
                        }
                        else 
                        {
                            b = 10*b + int.Parse(""+line[i]);
                        }
                    }
                    else if (reading == 4)
                    {
                        if (""+line[i] == "\n" || ""+line[i] == "\r")
                        { 
                        }
                        else 
                        {
                            name += line[i];
                        }
                    }
                }
                types.Add(new Vector3Int(r,g,b),name);
                
            }
        }
        public void LoadMap(string name)
        {
            var dir = Directory.GetCurrentDirectory();
            dir += "/Assets/Resources/maps/";
            var mapname = dir + name;
            Image im = Image.FromFile(mapname + ".png");
            Bitmap bm = new Bitmap(im);
            int width = im.Width;
            int height = im.Height; 
            for(int row = 0; row < height; ++row)
            {
                //load row
                for (int col = 0; col < width; ++col)
                {
                    //load pixel
                    Vector3Int vec = new Vector3Int(0,0,0);
                    vec.x = Mathf.RoundToInt(bm.GetPixel(col,row).R);
                    vec.y = Mathf.RoundToInt(bm.GetPixel(col,row).G);
                    vec.z = Mathf.RoundToInt(bm.GetPixel(col,row).B);
                    if (types.ContainsKey(vec))
                    {
                        //there's something in that space
                        SpawnTile(height - row,col,vec);
                    }
                }
            }
        }
        void SpawnTile(int row, int col, Vector3Int vec)
        {
            GameObject tile; 
            string name = types[vec];
            tile = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/"+name));
            tile.transform.position += new Vector3(col,row,0);
            tile.transform.parent = transform;
            if (name == "Character")
            {
                cam.GetComponent<Camera2DFollow>().target = tile.transform;
                cam.transform.position = tile.transform.position;
            }
        }

    }
