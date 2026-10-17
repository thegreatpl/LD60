using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features;

public class LevelGeneration : MonoBehaviour
{
    //[MenuItem("Level/CreateNew")]
    //static void CreateLevel()
    //{
    //    var tile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Terrain/dirtGround.prefab");


    //    for (int xdx = -50; xdx <= 50; xdx+=2)
    //    {
    //        for (int ydx = -50; ydx <= 50; ydx+= 2)
    //        {
    //            var t = PrefabUtility.InstantiatePrefab(tile) as GameObject; 
    //            t.transform.position = new Vector3(xdx, 0, ydx);
    //        }
    //    }


    //}

    [MenuItem("Level/CreateNewTerrainPrefab")]
    static void CreateNewTerrainPrefab()
    {
        var terrainPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TerrainPrefab.prefab");
        PrefabUtility.InstantiatePrefab(terrainPrefab);
    }

    //[MenuItem("Level/GenerateCliffs")]
    //static void GenerateCliffs()
    //{
    //    var terrainPrefab = SceneAsset.FindAnyObjectByType<TerrainPrefab>();
    //    var Cliff = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TerrainPrefab.prefab");

    //    var floors = GetOpenAirFloors(terrainPrefab.gameObject); 

    //    //foreach(var floor in floors)
    //    //{
    //    //    var currentHeight = floor.transform.position.y;

    //    //    var height = GetHeight(actX - 1, actZ);
    //    //    for (float idx = currentHeight + 0.5f; idx < height; idx++)
    //    //    {
    //    //        var tile = GameObject.Instantiate(Cliff, chunk.transform);
    //    //        tile.transform.localPosition = new Vector3((tilex * 2) - 1, idx, tiley * 2);
    //    //    }

    //    //    var height2 = GetHeight(actX + 1, actZ);
    //    //    for (float idx = currentHeight + 0.5f; idx < height2; idx++)
    //    //    {
    //    //        var tile = GameObject.Instantiate(Cliff, chunk.transform);
    //    //        tile.transform.localPosition = new Vector3((tilex * 2) + 1, idx, tiley * 2);
    //    //    }
    //    //    var height3 = GetHeight(actX, actZ - 1);
    //    //    for (float idx = currentHeight + 0.5f; idx < height3; idx++)
    //    //    {
    //    //        var tile = GameObject.Instantiate(CliffZ, chunk.transform);
    //    //        tile.transform.localPosition = new Vector3(tilex * 2, idx, (tiley * 2) - 1);
    //    //    }
    //    //    var height4 = GetHeight(actX, actZ + 1);
    //    //    for (float idx = currentHeight + 0.5f; idx < height4; idx++)
    //    //    {
    //    //        var tile = GameObject.Instantiate(CliffZ, chunk.transform);
    //    //        tile.transform.localPosition = new Vector3(tilex * 2, idx, (tiley * 2) + 1);
    //    //    }
    //    //}

    //}



    static IEnumerable<GameObject> GetOpenAirFloors(GameObject PrefabObj)
    {
        var childs = PrefabObj.GetComponentsInChildren<SpriteRenderer>().Select(x => x.gameObject);
        var floor = new List<GameObject>();
        foreach (var child in childs)
        {
            if (child.layer == 3)
                floor.Add(child);
        }

        foreach (var fl in floor)
        {
            if (!floor.Exists(x => x.transform.position.x == fl.transform.position.x && x.transform.position.z == fl.transform.position.z))
                yield return fl; 

            
        }


    }

}
