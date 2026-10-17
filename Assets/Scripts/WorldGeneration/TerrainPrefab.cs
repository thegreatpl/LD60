using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainPrefab : MonoBehaviour
{
    /// <summary>
    /// Object which will always be at 0 ground height to allow entry. 
    /// </summary>
    public GameObject EntryObject; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerable<Vector2Int> ReservedTiles()
    {
        //all tiles have a sprite renderer. 
        var tiles = GetComponentsInChildren<SpriteRenderer>().Select(x => x.gameObject).Where(x => x.layer == 3);

        foreach (var tile in tiles)
        {
            yield return new Vector2Int((int)tile.transform.localPosition.x *2, (int)tile.transform.localPosition.z *2);
        }
    }



    public void AdjustLocationToEntrance(Vector3 EntranceLocation)
    {
        //Location within the childs where we want to go. 
        var entranceLoc = EntryObject.transform.localPosition;

        // transform.localPosition = new Vector3(entranceLoc.x + EntranceLocation.x, EntranceLocation.y+ entranceLoc.y, entranceLoc.z + EntranceLocation.z);

        transform.localPosition = new Vector3(EntranceLocation.x - (entranceLoc.x*2), EntranceLocation.y - entranceLoc.y, EntranceLocation.z - (entranceLoc.z*2)); 

    }


}
