using System;
using UnityEngine;

[Serializable]
public class HouseType
{
    [SerializeField] private GameObject[] prefabs;
    //public int sizeRequired;
    public int quantity;
    public int quantityAlreadyPlaced;

    public GameObject GetPrefab()
    {
        quantityAlreadyPlaced++;
        if (prefabs.Length > 1)
        {
            var random = UnityEngine.Random.Range(0, prefabs.Length);
            return prefabs[random];
        }
        else
        {
            return prefabs[0];
        }

    }
    public bool isBuildingAvailable()
    {
        return quantityAlreadyPlaced < quantity;
    }
    public void Reset()
    {
        quantityAlreadyPlaced = 0;
    }
}
