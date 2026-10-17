using UnityEngine;
using UnityEngine.UIElements;

public class TerrainGeneration : MonoBehaviour
{
    public GameObject Chunk; 

    public GameObject BaseTerrain; 

    public GameObject SlopePosX;
    public GameObject SlopePosZ;
    public GameObject SlopeZ;
    public GameObject SlopeX;


    public GameObject Cliff; 
    public GameObject CliffZ;

    public float Scale = 20f;


    public int ChunkWidth = 50; 
    public int ChunkLength = 50;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int xdx = 0; xdx < 3; xdx++)
        {
            for (int ydx = 0; ydx < 3; ydx++)
            {
                GenerateChunk(xdx, ydx);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateChunk(int chunkx, int chunkz)
    {
        var chunkobj = GameObject.Instantiate(Chunk);
        var chunkLox = chunkx * (ChunkWidth * 2);
        var chunkLoz = chunkz * (ChunkLength * 2);

        chunkobj.transform.position = new Vector3(chunkLox, 0, chunkLoz);

        for (int xdx = 0; xdx < 50; xdx++)
        {
            for (int ydx = 0; ydx < 50; ydx++)
            {
                var actX = xdx + (chunkx * ChunkWidth);
                var actZ = ydx + (chunkz * ChunkLength);

                var thisheight = GetHeight(actX,actZ);   
                GameObject type = BaseTerrain;

                //if (GetHeight(xdx - 1, ydx) == thisheight + 1)
                //    type = SlopeX;
                //if (GetHeight(xdx +1, ydx) == thisheight +1)
                //    type = SlopePosX;
                //if (GetHeight(xdx, ydx -1) == thisheight +1)
                //    type = SlopeZ;
                //if (GetHeight(xdx, ydx + 1) == thisheight +1)
                //    type = SlopePosZ;


                var tile = GameObject.Instantiate(type, chunkobj.transform);
                tile.transform.localPosition = new Vector3(xdx * 2, thisheight + type.transform.position.y, ydx * 2);

                GenerateCliffs(xdx, ydx, chunkx, chunkz, chunkobj);


            }
        }


    }


    public int GetHeight(int tilex, int tiley)
    {
        int width = 50;
        int height = 50; 

        float xCoord = (float)tilex / ChunkWidth * Scale;// + offsetX;
        float yCoord = (float)tiley / ChunkLength * Scale;// + offsetY;

        var result = Mathf.PerlinNoise(xCoord, yCoord);
        Debug.Log(result);
        //result between 0.0 and 1.0. 
        int val = (int)(result * 10); 
        return val;
    }


    public void GenerateCliffs(int tilex, int tiley, int chunX, int chunkZ, GameObject chunk)
    {
        var actX = tilex + (chunX * ChunkWidth);
        var actZ = tiley + (chunkZ * ChunkLength);

        var currentHeight = GetHeight(actX, actZ);

        var height = GetHeight(actX - 1, actZ);
        for (float idx = currentHeight + 0.5f;  idx < height; idx++)
        {
            var tile = GameObject.Instantiate(Cliff, chunk.transform);
            tile.transform.localPosition = new Vector3((tilex * 2)-1, idx, tiley * 2);
        }

        var height2 = GetHeight(actX +1, actZ);
        for (float idx = currentHeight + 0.5f; idx < height2; idx++)
        {
            var tile = GameObject.Instantiate(Cliff, chunk.transform);
            tile.transform.localPosition = new Vector3((tilex * 2)+1, idx, tiley * 2);
        }
        var height3 = GetHeight(actX, actZ - 1);
        for (float idx = currentHeight + 0.5f; idx < height3; idx++)
        {
            var tile = GameObject.Instantiate(CliffZ, chunk.transform);
            tile.transform.localPosition = new Vector3(tilex * 2, idx, (tiley * 2)-1);
        }
        var height4 = GetHeight(actX, actZ + 1);
        for (float idx = currentHeight + 0.5f; idx < height4; idx++)
        {
            var tile = GameObject.Instantiate(CliffZ, chunk.transform);
            tile.transform.localPosition = new Vector3(tilex * 2, idx, (tiley * 2)+1);
        }

    }



}
