using System;
using UnityEngine;

[Serializable]
public class HouseType
{
    [SerializeField] private GameObject[] prefabs;
    //[SerializeField] private GameObject endPrefab;
    public int sizeRequired;
    public int quantity;
    public int quantityAlreadyPlaced;

    public GameObject GetPrefab()
    {
        

          if(prefabs.Length == 1)
        {
            return prefabs[0];
            
        }
        else if (prefabs.Length == 0 || prefabs == null)
        {
            Debug.LogError("No prefabs assigned to HouseType.");
            return null;
        }
        var random = UnityEngine.Random.Range(0, prefabs.Length);
        return prefabs[random];

    }
  public bool TryPlaceHouses()
    {
        if(!isBuildingAvailable())
        {
            Debug.LogError("No more houses of this type can be placed.");
            return false;
            
        }
        quantityAlreadyPlaced++;
        return true;
    }
    public bool isBuildingAvailable()
    {
        return quantityAlreadyPlaced < quantity;
    }
    public void Reset()
    {
        quantityAlreadyPlaced = 0;
    }
  /*  public GameObject GetEndPrefab()
    {
        if (endPrefab != null)
        {
            return endPrefab;
        }
        return GetPrefab();
    }*/
}
