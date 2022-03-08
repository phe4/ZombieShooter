using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Ammo,
        Mask,
        Medkit,
        Syringe
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ammo: return ItemAssets.Instance.ammoSprite;
            case ItemType.Mask: return ItemAssets.Instance.maskSprite;
            case ItemType.Medkit: return ItemAssets.Instance.medkitSprite;
            case ItemType.Syringe: return ItemAssets.Instance.syringeSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ammo:
            case ItemType.Mask:
            case ItemType.Medkit:
            case ItemType.Syringe:
                return true;
        }
    }
}
