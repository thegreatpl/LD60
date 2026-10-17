using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Container;

public class PlayerInventory : MonoBehaviour
{

    public List<Item> PlayerItems = new List<Item>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Container GetLargestContainer(ItemType itemType)
    {
        var containers = PlayerItems.OfType<Container>();

        return containers.Where(x => x.ContainerType == itemType).OrderBy(x => x.CurrentAmount).FirstOrDefault();

    }

}
