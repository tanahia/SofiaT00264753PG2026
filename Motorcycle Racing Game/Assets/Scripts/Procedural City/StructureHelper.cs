
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{
	public class StructureHelper : MonoBehaviour
	{
		public HouseType[] houseTypes;
		public Dictionary<Vector3Int, GameObject> structuresDictionary = new Dictionary<Vector3Int, GameObject>();

		public void PlaceStructuresAroundRoad(List<Vector3Int> roadPositions)
		{
			Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPositions);
            List<Vector3Int> blockedPositions = new List<Vector3Int>();

            foreach (var freeSpot in freeEstateSpots)
            {
                if(blockedPositions.Contains(freeSpot.Key))
                {
                    continue;
                }
                var rotation = Quaternion.identity;
                switch (freeSpot.Value)
                {
                    case Direction.Up:
                        rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case Direction.Down:
                        rotation = Quaternion.Euler(0, -90, 0);
                        break;
                    case Direction.Right:
                        rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    default:
                        break;
                }
                for (int i = 0; i < houseTypes.Length; i++)
                {
                    if (houseTypes[i].quantity==-1)
                    {
                        var house = SpawnPrefab(houseTypes[i].GetPrefab(), freeSpot.Key, rotation);
                        structuresDictionary.Add(freeSpot.Key, house);
                        break;
                    }
                    if (houseTypes[i].isBuildingAvailable())
                    {
                        if (houseTypes[i].sizeRequired > 1)
                        {
                        var halfSize =Mathf.FloorToInt(houseTypes[i].sizeRequired / 2.0f);
                            List<Vector3Int> temppositionsToBlock = new List<Vector3Int>();
                            if (VerifyHousesFits(halfSize, freeEstateSpots, freeSpot,blockedPositions, ref temppositionsToBlock))
                            { 
                            blockedPositions.AddRange(temppositionsToBlock);
                                var house = SpawnPrefab(houseTypes[i].GetPrefab(), freeSpot.Key, rotation);
                                structuresDictionary.Add(freeSpot.Key, house);
                                foreach (var position in temppositionsToBlock)
                                {
                                    structuresDictionary.Add(position, house);
                                }
                                break;
                            }
                        }
                        else
                        {
                            var house = SpawnPrefab(houseTypes[i].GetPrefab(), freeSpot.Key, rotation);
                            structuresDictionary.Add(freeSpot.Key, house);
                            
                        }
                        break;
                    }
                }
                
            }
        }

        private bool VerifyHousesFits(int halfSize, Dictionary<Vector3Int, Direction> freeEstateSpots, KeyValuePair<Vector3Int, Direction> freeSpot, List<Vector3Int> blockedPositions, ref List<Vector3Int> temppositionsToBlock)
        {
            Vector3Int direction = Vector3Int.zero;
            if(freeSpot.Value == Direction.Down || freeSpot.Value == Direction.Up)
            {
                direction = Vector3Int.right;
            }
            else
            {
                direction = new Vector3Int(0, 0, 1);
            }
            for (int i = 1; i <= halfSize; i++)
            {
               var position1=freeSpot.Key + direction * i;
               var position2 = freeSpot.Key - direction * i;
                if (!freeEstateSpots.ContainsKey(position1)||!freeEstateSpots.ContainsKey(position2)||blockedPositions.Contains(position1) || blockedPositions.Contains(position2))
                {
                    return false; 
                }
                temppositionsToBlock.Add(position1);
                temppositionsToBlock.Add(position2);
            }
            return true;
        }

        private GameObject SpawnPrefab(GameObject prefab, Vector3Int position, Quaternion rotation)
        {
           var newStructure = Instantiate(prefab, position, rotation,transform);
            return newStructure;
        }

        private Dictionary<Vector3Int, Direction> FindFreeSpacesAroundRoad(List<Vector3Int> roadPositions)
        {
            Dictionary<Vector3Int, Direction> freeSpaces = new Dictionary<Vector3Int, Direction>();
            foreach (var position in roadPositions)
            {
                var neighbourDirections = PlacementHelper.FindNeighbour(position, roadPositions);
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    if (neighbourDirections.Contains(direction) == false)
                    {
                        var newPosition = position + PlacementHelper.GetOffsetFromDirection(direction);
                        if (freeSpaces.ContainsKey(newPosition))
                        {
                            continue;
                        }
                        freeSpaces.Add(newPosition, PlacementHelper.GetReverseDirection(direction)); 
                    }
                }
            }
            return freeSpaces;
        }                            

	}
}

