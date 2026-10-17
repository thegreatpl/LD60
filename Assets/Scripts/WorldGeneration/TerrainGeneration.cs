using System.Collections;
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


    public float XOffset = 0; 
    public float YOffset = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ChunkManager()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator ChunkManager()
    {
        for (int xdx = -2; xdx < 2; xdx++)
        {
            for (int ydx = -2; ydx < 2; ydx++)
            {
                yield return GenerateChunk(xdx, ydx);
            }
        }

        yield return null; 
    }



    public IEnumerator GenerateChunk(int chunkx, int chunkz)
    {
        var chunkobj = GameObject.Instantiate(Chunk).GetComponent<Chunk>();
        var chunkLox = chunkx * (ChunkWidth * 2);
        var chunkLoz = chunkz * (ChunkLength * 2);

        chunkobj.transform.position = new Vector3(chunkLox, 0, chunkLoz);
        chunkobj.TerrainGeneration = this; 
        chunkobj.ChunkPos = new Vector2Int(chunkx, chunkz);
        yield return null;
        yield return StartCoroutine(chunkobj.GenerateChunk()); 

        yield return null;
    }


    public int GetHeight(int tilex, int tiley)
    {
        int width = 50;
        int height = 50; 

        float xCoord = (float)tilex / ChunkWidth * Scale + XOffset;
        float yCoord = (float)tiley / ChunkLength * Scale + YOffset;

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
