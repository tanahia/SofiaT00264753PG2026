using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject goodItemGO, badItemGO;
    List<Item> items= new List<Item>();
    private int maxItems = 5;
    void Start()
    {
        PopulateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnItemAt(Vector3 position)
    {
        GameObject item = UnityEngine.Random.value > 0.5f ? goodItemGO : badItemGO;
        GameObject newItem = Instantiate(item, position, Quaternion.identity);
        Item newItemScript= newItem.GetComponent<Item>();
        items.Add(newItemScript);
        newItemScript.Iam(this);


    }
    void PopulateItems()
    {
while(items.Count<maxItems)
        {
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
            SpawnItemAt(randomPosition);
        }
    }

    internal void IbeenCollected(Item item)
    {
        if(item is GoodItem)
        {
            (item as GoodItem).DoGoodThing();
        }
        else if(item is BadItem)
        {
            (item as BadItem).DoBadThing();
        }
        SpawnItemAt(new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10)));
        items.Remove(item);
    }
}
