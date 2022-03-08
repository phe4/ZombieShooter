using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDetail : MonoBehaviour
{
    public Item item;

    public Item.ItemType type;
    public int itemAmount;


    // Start is called before the first frame update
    void Start()
    {
        this.item = new Item { itemType = type, amount = itemAmount };
    }
}
