using NUnit.Framework;
using SVS;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject goodItemGO, badItemGO;
    List<Item> items = new List<Item>();
    private int maxItems = 5;
    RoadHelper roadHelper;
    List<Vector3Int> availablePositions;
    Dictionary<Item, Vector3Int> itemActions = new Dictionary<Item, Vector3Int>();
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
        GameObject item = UnityEngine.Random.value > 0.5f ? goodItemGO : badItemGO;
        GameObject newItem = Instantiate(item, position, Quaternion.identity);
        Item newItemScript = newItem.GetComponent<Item>();
        items.Add(newItemScript);
        newItemScript.Iam(this);


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
        Vector3Int randomPos = roadPositions[UnityEngine.Random.Range(0, roadPositions.Count)];
        float cellSize = roadHelper.cellSize;
        return new Vector3(randomPos.x * cellSize, randomPos.y, randomPos.z * cellSize);
    }
}
