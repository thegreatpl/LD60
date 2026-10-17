using KaimiraGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Aseprite;
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

    public GameObject Tree;


    public int LoadDistance = 2; 


    Dictionary<Vector2Int, Chunk> LoadedChunks = new Dictionary<Vector2Int, Chunk>();


    public int ChunkLoadSpeed = 50;



    public GameObject VaultPrefab; 


    public List<TerrainPrefabDef> TerrainPrefabs = new List<TerrainPrefabDef>();

    public WeightedList<TerrainPrefabDef> WeightedTerrainPrefabs = new WeightedList<TerrainPrefabDef>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(var terrain in TerrainPrefabs)
        {
            WeightedTerrainPrefabs.Add(terrain, terrain.Weight); 
        }

       // StartCoroutine(ChunkManager()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator ChunkManager()
    {
        yield return null;

        //Do an initial load here? 
        var playerchunk2 = GetChunkFromLocation(GameManager.Instance.Player.transform.position);
        //yield return StartCoroutine(GenerateChunk(playerchunk2.x, playerchunk2.y));
        for (int xdx = playerchunk2.x - 1; xdx <= playerchunk2.x + 1; xdx++)
        {
            for (int ydx = playerchunk2.y - 1; ydx <= playerchunk2.y + 1; ydx++)
            {
                if (!LoadedChunks.ContainsKey(new Vector2Int(xdx, ydx)))
                    yield return StartCoroutine(GenerateChunk(xdx, ydx));
            }
        }
        yield return null;

        while (true) //TODO: figure out how to stop this on game end. 
        {
            if (GameManager.Instance?.Player == null)
            {
                yield return null;
                continue; //Break here maybe?
            }
            //this needs tweaking. 
            var playerchunk = GetChunkFromLocation(GameManager.Instance.Player.transform.position); 
            for (int xdx = playerchunk.x -LoadDistance; xdx <= playerchunk.x + LoadDistance; xdx++)
            {
                for (int ydx = playerchunk.y -LoadDistance; ydx  <= playerchunk.y + LoadDistance; ydx++)
                {
                    if (!LoadedChunks.ContainsKey(new Vector2Int(xdx, ydx)))
                        yield return StartCoroutine(GenerateChunk(xdx, ydx)); 
                }
            }

            //Cull chunks too far from the player here. 

            yield return null;
        }

    }



    public IEnumerator GenerateChunk(int chunkx, int chunkz, string feature = "")
    {
        var chunkobj = GameObject.Instantiate(Chunk).GetComponent<Chunk>();
        var chunkLox = chunkx * (ChunkWidth * 2);
        var chunkLoz = chunkz * (ChunkLength * 2);

        chunkobj.transform.position = new Vector3(chunkLox, 0, chunkLoz);
        chunkobj.TerrainGeneration = this; 
        chunkobj.ChunkPos = new Vector2Int(chunkx, chunkz);
        chunkobj.Feature = feature; 
        LoadedChunks.Add(chunkobj.ChunkPos, chunkobj);
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


    public Vector2Int GetChunkFromLocation(Vector3 location)
    {
        var chunkx = location.x / (ChunkWidth * 2);
        var chunkz = location.z / (ChunkLength * 2); 

        return new Vector2Int((int)chunkx, (int)chunkz);
    }


    public GameObject GetRandomFeature(System.Random chunkRandom)
    {
        return WeightedTerrainPrefabs.Next(chunkRandom).Prefab;
    }

    public GameObject GetFeature(string  name)
    {
        return TerrainPrefabs.FirstOrDefault(x => x.Name == name)?.Prefab;
    }

}
