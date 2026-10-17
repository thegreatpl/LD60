using UnityEditor;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [MenuItem("Level/CreateNew")]
    static void CreateLevel()
    {
        var tile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Terrain/dirtGround.prefab");


        for (int xdx = -50; xdx <= 50; xdx+=2)
        {
            for (int ydx = -50; ydx <= 50; ydx+= 2)
            {
                var t = PrefabUtility.InstantiatePrefab(tile) as GameObject; 
                t.transform.position = new Vector3(xdx, 0, ydx);
            }
        }


    }
}
