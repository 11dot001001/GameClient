using GameCore.Tools;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class VirusGroup
    {
        public VirusGroup(Road road, int virusCount, float virusSpeed)
        {
            Virus virus = Object.Instantiate(GameManager.VirusPrefab, road.Points[0], Quaternion.identity).GetComponent<Virus>();
            virus.Initialize(new VirusGroapRoadManager(road.Points, 0.5F).GetCorrectedRoad().ToList(), virusSpeed);
        }
    }
}
