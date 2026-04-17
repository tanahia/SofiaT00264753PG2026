
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SVS
{
	public class StructureHelper : MonoBehaviour
	{
		public HouseType[] houseTypes;
        public float cellSize;
        public Dictionary<Vector3Int, GameObject> structuresDictionary = new Dictionary<Vector3Int, GameObject>();

        public void PlaceStructuresAroundRoad(List<Vector3Int> roadPositions)
        {
            foreach (var houseType in houseTypes)
            {
                houseType.Reset();
            }
            Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPositions);
            List<Vector3Int> blockedPositions = new List<Vector3Int>();
           var randomizedFreeSpots = freeEstateSpots.OrderBy(x => UnityEngine.Random.value).ToList();
            foreach (var freeSpot in randomizedFreeSpots)
            {
                if (blockedPositions.Contains(freeSpot.Key) || structuresDictionary.ContainsKey(freeSpot.Key))
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

                    if (houseTypes[i].sizeRequired > 1)
                    {


                        // Vector3Int direction;
                        Vector3Int[] possibleDirections;
                        if (freeSpot.Value == Direction.Down || freeSpot.Value == Direction.Up)
                            possibleDirections = new[] { Vector3Int.forward, Vector3Int.back };
                        else
                            possibleDirections = new[] { Vector3Int.right, Vector3Int.left };
                        foreach (var direction in possibleDirections)
                        {
                            List<Vector3Int> temppositionsToBlock = new List<Vector3Int>();
                            if (VerifyHousesFits(houseTypes[i].sizeRequired, roadPositions, freeSpot.Key, direction, ref temppositionsToBlock))
                            {

                                if (houseTypes[i].quantity == -1 || houseTypes[i].TryPlaceHouses())
                                {
                                    blockedPositions.AddRange(temppositionsToBlock);
                                    if (houseTypes[i].GetPrefab() == null) continue;

                                    var house = SpawnPrefab(houseTypes[i].GetPrefab(), freeSpot.Key, rotation);
                                    structuresDictionary.Add(freeSpot.Key, house);
                                    foreach (var position in temppositionsToBlock)
                                    {
                                        structuresDictionary.Add(position, house);
                                    }
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        if (houseTypes[i].quantity == -1 || houseTypes[i].TryPlaceHouses())
                        {
                            if (houseTypes[i].GetPrefab() == null) continue;
                            var house = SpawnPrefab(houseTypes[i].GetPrefab(), freeSpot.Key, rotation);
                            structuresDictionary.Add(freeSpot.Key, house);
                            break;
                        }

                    }
                }    
              }
            }
        

        private bool VerifyHousesFits(int sizeRequired,
    List<Vector3Int> roadPositions,
    Vector3Int startPosition,
    Vector3Int direction,
    ref List<Vector3Int> positionsToBlock)
        {
            List<Vector3Int> tempPositions = new List<Vector3Int>();
            for (int i = 1; i < sizeRequired; i++)
            {
                var nextPos = startPosition + direction * i;

                if (roadPositions.Contains(nextPos))
                    return false;
                if (structuresDictionary.ContainsKey(nextPos))
                    return false;

                tempPositions.Add(nextPos);
            }
            positionsToBlock.AddRange(tempPositions);
            return true;
        }

        private GameObject SpawnPrefab(GameObject prefab, Vector3Int position, Quaternion rotation)
        {
            //  var newStructure = Instantiate(prefab, position, rotation,transform);
            // return newStructure;
            Vector3 worldPosition = new Vector3(
         position.x * cellSize,
         position.y,
         position.z * cellSize
     );

            return Instantiate(prefab, worldPosition, rotation, transform);
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

