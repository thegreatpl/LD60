using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{

    public List<PrefabObj> Prefabs; 

    public Dictionary<string , PrefabObj> PrefabsDictionary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrefabsDictionary = new Dictionary<string , PrefabObj>();
        foreach (var prefab in Prefabs)
        {
            PrefabsDictionary.Add(prefab.Name, prefab); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public GameObject GetPrefab(string name)
    {
        if (PrefabsDictionary.ContainsKey(name))
            return PrefabsDictionary[name].Prefab;
        return null;
    }


    public IEnumerator<GameObject> GetPrefabsOfType(string type)
    {
        foreach (var prefab in Prefabs)
        {
            if (prefab.Categories.Contains(type))
                yield return prefab.Prefab; 
        }
    }

}
