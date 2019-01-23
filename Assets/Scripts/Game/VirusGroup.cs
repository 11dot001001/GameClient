using GameCore.Tools;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class VirusGroup
    {
        public VirusGroup(Road road, int virusCount, float virusSpeed)
        {
            Virus virus = Object.Instantiate(GameManager.VirusPrefab, road.Start.Transform.Position, Quaternion.identity).GetComponent<Virus>();
            virus.Initialize(new VirusGroapRoadManager(road, virusSpeed).GetCorrectedRoad().ToList(), virusSpeed);
        }
    }
}
