using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2Int ChunkPos;


    public int ChunkWidth = 50;
    public int ChunkLength = 50;


    public TerrainGeneration TerrainGeneration;

    System.Random LocalRandom;


    List<Vector3> ReservedPositions = new List<Vector3>(); 

    List<Vector2> ReservedTiles = new List<Vector2>();

    Dictionary<Vector2, int> ReservedHeights = new Dictionary<Vector2, int>();

    public string Feature; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChunkWidth = TerrainGeneration.ChunkWidth;
        ChunkLength = TerrainGeneration.ChunkLength;
        LocalRandom = new System.Random(ChunkPos.x + ChunkPos.y + (int)TerrainGeneration.XOffset + (int)TerrainGeneration.YOffset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public IEnumerator GenerateChunk()
    {
        int counter = 0;

        yield return GenerateFeatures(); 

        for (int xdx = 0; xdx < ChunkWidth; xdx++)
        {
            for (int ydx = 0; ydx < ChunkLength; ydx++)
            {

                if (ReservedTiles.Contains(new Vector2(xdx, ydx))) 
                { 
                    continue; 
                }

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

                //if (ReservedPositions.Contains(tile.transform.localPosition))
                //    Destroy(tile); 

                //TODO: fix the cliffs
                GenerateCliffs(xdx, ydx, ChunkPos.x, ChunkPos.y, gameObject);
                counter++;
                if (counter > TerrainGeneration.ChunkLoadSpeed)
                {
                    counter = 0;
                    yield return null;
                }

            }
        }

        yield return GenerateTrees(counter); 
        

    }


    public IEnumerator GenerateFeatures()
    {
        if (!string.IsNullOrWhiteSpace(Feature) && LocalRandom.NextDouble() < 0.6f)
            yield break;

        var featurePrefab = string.IsNullOrWhiteSpace(Feature) ? TerrainGeneration.GetRandomFeature(LocalRandom) : TerrainGeneration.GetFeature(Feature); 

        var vault = Instantiate(featurePrefab, transform).GetComponent<TerrainPrefab>();
        int vposx = LocalRandom.Next(10, ChunkWidth -10);
        int vposz = LocalRandom.Next(10, ChunkLength-10);


        vault.AdjustLocationToEntrance(new Vector3(vposx * 2, TerrainGeneration.GetHeight(vposx + (ChunkPos.x * ChunkWidth), vposz + (ChunkPos.y * ChunkLength)), vposz * 2));

        yield return null;

        var tiles = vault.GetComponentsInChildren<WalkableTile>();
        var cliffs = new List<Vector2>(); 
        foreach (var tile in tiles)
        {
            var pos = transform.InverseTransformPoint(tile.transform.position);
            ReservedPositions.Add(pos);
            ReservedTiles.Add(new Vector2(pos.x / 2, pos.z / 2));
            ReservedHeights.Add(new Vector2((pos.x/2) + (ChunkPos.x * ChunkWidth), (pos.z/2) + (ChunkPos.y * ChunkLength)), (int)tile.transform.position.y);
            if (tile.NeedsCliffs)
                cliffs.Add(new Vector2(pos.x / 2, pos.z / 2));
        }

        foreach (var tile in cliffs)
        {
            //need to fix this. Current generates cliffs where there are already walls. 
            GenerateCliffs((int)tile.x, (int)tile.y, ChunkPos.x, ChunkPos.y, gameObject);
        }    


    }

    public IEnumerator GenerateTrees(int counter)
    {
        for (int idx = 0; idx < LocalRandom.Next(0, 100); idx++)
        {
            var tree = Instantiate(TerrainGeneration.Tree, gameObject.transform);
            int x = LocalRandom.Next(0, ChunkWidth);
            int y = LocalRandom.Next(0, ChunkLength);

            if (ReservedTiles.Contains(new Vector2(x, y)))
                continue; 


            var actX = x + (ChunkPos.x * ChunkWidth);
            var actZ = y + (ChunkPos.y * ChunkLength);
            tree.transform.localPosition =
                new Vector3(x * 2, TerrainGeneration.GetHeight(actX, actZ) + TerrainGeneration.Tree.transform.position.y, y * 2);

            counter++;
            if (counter > TerrainGeneration.ChunkLoadSpeed)
            {
                counter = 0;
                yield return null;
            }
        }
    }



    public void GenerateCliffs(int tilex, int tiley, int chunX, int chunkZ, GameObject chunk)
    {
        var actX = tilex + (chunX * ChunkWidth);
        var actZ = tiley + (chunkZ * ChunkLength);

        var currentHeight = GetHeight(actX, actZ);

        var height = GetHeight(actX - 1, actZ);
        for (float idx = currentHeight + 0.5f; idx < height; idx++)
        {
            var tile = GameObject.Instantiate(TerrainGeneration.Cliff, chunk.transform);
            tile.transform.localPosition = new Vector3((tilex * 2) - 1, idx, tiley * 2);
        }

        var height2 = GetHeight(actX + 1, actZ);
        for (float idx = currentHeight + 0.5f; idx < height2; idx++)
        {
            var tile = GameObject.Instantiate(TerrainGeneration.Cliff, chunk.transform);
            tile.transform.localPosition = new Vector3((tilex * 2) + 1, idx, tiley * 2);
        }
        var height3 = GetHeight(actX, actZ - 1);
        for (float idx = currentHeight + 0.5f; idx < height3; idx++)
        {
            var tile = GameObject.Instantiate(TerrainGeneration.CliffZ, chunk.transform);
            tile.transform.localPosition = new Vector3(tilex * 2, idx, (tiley * 2) - 1);
        }
        var height4 = GetHeight(actX, actZ + 1);
        for (float idx = currentHeight + 0.5f; idx < height4; idx++)
        {
            var tile = GameObject.Instantiate(TerrainGeneration.CliffZ, chunk.transform);
            tile.transform.localPosition = new Vector3(tilex * 2, idx, (tiley * 2) + 1);
        }

    }


    public int GetHeight(int  actX, int actZ)
    {
        Vector2 pos = new Vector2(actX, actZ);
        if (ReservedHeights.ContainsKey(pos))
            return ReservedHeights[pos];


        return TerrainGeneration.GetHeight(actX, actZ);

    }


    //public Vector3 WorldPositionToLocal(Vector3 position)
    //{

    //}
}
