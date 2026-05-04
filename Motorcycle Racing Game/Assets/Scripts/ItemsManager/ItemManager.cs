using NUnit.Framework;
using SVS;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject goodItemGO, badItemGO;
    List<Item> items = new List<Item>();
    private int maxItems = 15;
    private float spawnHeight=0.3f;
    RoadHelper roadHelper;
    Vector3Int randomPos;
    

    Dictionary<Vector3Int, Item> itemPositions = new Dictionary<Vector3Int, Item>();
    void Start()
    {
       
        roadHelper = FindFirstObjectByType<RoadHelper>();
        PopulateItems();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnItemAt(Vector3 position)
    {
        bool isGoodItem = UnityEngine.Random.value > 0.5f;
        GameObject item = isGoodItem ? goodItemGO : badItemGO;
        float height=isGoodItem?0.5f:0.17f;

        GameObject newItem = Instantiate(item, new Vector3(position.x, position.y+height,position.z), Quaternion.identity);
        
        Item newItemScript = newItem.GetComponent<Item>();
        items.Add(newItemScript);
        newItemScript.Iam(this);
        Vector3Int gridPos = Vector3Int.RoundToInt(position/roadHelper.cellSize);
        itemPositions[gridPos] = newItemScript;


    }
    void PopulateItems()
    {
        while (items.Count < maxItems)
        {
            Vector3 randomPosition =GetRandomRoadPosition();
            SpawnItemAt(randomPosition);
        }
    }

    internal void IbeenCollected(Item item)
    {
        if (item is GoodItem)
        {
            (item as GoodItem).DoGoodThing();
        }
        else if (item is BadItem)
        {
            (item as BadItem).DoBadThing();
        }
        Vector3Int index = default;

        foreach (var key in itemPositions)
        {
            if (key.Value == item)
            {
                index = key.Key;
                break;
            }
        }

        itemPositions.Remove(index);

        Vector3 randomPosition = GetRandomRoadPosition();
        SpawnItemAt(randomPosition);
      items.Remove(item);
    }
    internal Vector3 GetRandomRoadPosition()
    {
        List<Vector3Int> roadPositions = roadHelper.GetRoadPositions();

        if (roadPositions == null || roadPositions.Count == 0)
        {
            return Vector3.zero;
        }

        do
        {
            randomPos = roadPositions[UnityEngine.Random.Range(0, roadPositions.Count)];
        }
        while (itemPositions.ContainsKey(randomPos));

        
        return GetOffsetRandomPosition(randomPos);
    }
    Vector3 GetOffsetRandomPosition(Vector3Int position)
    {
        float cellsize = roadHelper.cellSize;
        float offsetAmount = cellsize * spawnHeight;
        float offsetX = UnityEngine.Random.value > 0.5f ? offsetAmount : -offsetAmount;
        return new Vector3(position.x * cellsize + offsetX, position.y, position.z * cellsize);
    }
   
}
