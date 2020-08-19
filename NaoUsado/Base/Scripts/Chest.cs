using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemResource> dropList = new List<ItemResource>();
    public ItemResource chestItem;

    public void Start()
    {
        int itemIndex = Random.Range(0, dropList.Count);
        chestItem = dropList[itemIndex];
    }

    public Weapon OpenChest()
    {
        GameObject item = Instantiate(chestItem.itemPrefab);
        return item.GetComponent<Weapon>();
    }
}
