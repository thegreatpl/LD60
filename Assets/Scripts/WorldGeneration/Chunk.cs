using System.Collections;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2Int ChunkPos;


    public int ChunkWidth = 50;
    public int ChunkLength = 50;


    public TerrainGeneration TerrainGeneration; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChunkWidth = TerrainGeneration.ChunkWidth;
        ChunkLength = TerrainGeneration.ChunkLength;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public IEnumerator GenerateChunk()
    {
        int counter = 0;

        for (int xdx = 0; xdx < 50; xdx++)
        {
            for (int ydx = 0; ydx < 50; ydx++)
            {
                var actX = xdx + (ChunkPos.x * ChunkWidth);
                var actZ = ydx + (ChunkPos.y * ChunkLength);

                var thisheight = TerrainGeneration.GetHeight(actX, actZ);
                GameObject type = TerrainGeneration.BaseTerrain;

                //if (GetHeight(xdx - 1, ydx) == thisheight + 1)
                //    type = SlopeX;
                //if (GetHeight(xdx +1, ydx) == thisheight +1)
                //    type = SlopePosX;
                //if (GetHeight(xdx, ydx -1) == thisheight +1)
                //    type = SlopeZ;
                //if (GetHeight(xdx, ydx + 1) == thisheight +1)
                //    type = SlopePosZ;


                var tile = GameObject.Instantiate(type, gameObject.transform);
                tile.transform.localPosition = new Vector3(xdx * 2, thisheight + type.transform.position.y, ydx * 2);

                TerrainGeneration.GenerateCliffs(xdx, ydx, ChunkPos.x, ChunkPos.y, gameObject);
                counter++;
                if (counter > 100)
                {
                    counter = 0;
                    yield return null;
                }

            }
        }
    }

}
