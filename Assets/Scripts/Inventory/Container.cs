using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Container :Item
{
    public enum ItemType { Water, Oxygen };

    public ItemType ContainerType { get; set; }

    public float MaxAmount { get; set; }


    public float CurrentAmount { get; set; }


}

