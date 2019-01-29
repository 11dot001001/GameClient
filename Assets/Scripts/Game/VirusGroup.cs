using Devdeb.Maths.Geometry2D;
using GameCore.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class VirusGroup
    {
        private const float _convergenceFactor = 0.8F;
        private readonly float _groupSpeed;
        private readonly float _routeDistance;
        private readonly Vector2 _startPosition;
        private readonly Vector2 _firstTargetPosition;
        private readonly List<RoadSegmentController> _roadSegments;
        private GameObject VirusPrefab => GameManager.VirusPrefab;
        private Vector2 StartDirection => (_firstTargetPosition - _startPosition).normalized;

        public VirusGroup(Road road, int virusCount, float groupSpeed)
        {
            _groupSpeed = groupSpeed;
            _startPosition = road.Start.Transform.Position;
            _firstTargetPosition = road.BacteriumProximities[0].StartPosition;
            _roadSegments = CreateSegments(_startPosition, road.End.Transform.Position, road.BacteriumProximities).ToList();
            _routeDistance = _roadSegments.Sum(x => x.Distance);

            float maxRespawnDeviation = GetMaximumDeviationVirusRespawn(road.Start.Transform.Circle, 0.5F, StartDirection); // ещё вычесть радиус вирусины 
            for (int i = 0; i < 10; i++)
                InitializeVirus(Random.Range(-maxRespawnDeviation, maxRespawnDeviation));
        }

        private float GetMaximumDeviationVirusRespawn(Circle circle, float deviation, Vector2 targetDirection)
        {
            if (circle.Radius <= deviation)
                throw new System.Exception("The deviation must be more than circle radius.");
            Vector2 rotated90TargetDirection = new Vector2(-targetDirection.y, targetDirection.x) * deviation;
            Geometry2D.Line2CircleIntersect(circle.Position + rotated90TargetDirection, circle.Position + rotated90TargetDirection + targetDirection, circle, out Vector2 intersectedPoint1, out Vector2 intersectedPoint2);
            return Vector2.Distance(intersectedPoint1, intersectedPoint2) / 2;
        }

        private void InitializeVirus(float respawnDeviation)
        {
            List<RoadSegmentController> roadSegments = new List<RoadSegmentController>();
            for (int i = 0; i < _roadSegments.Count(); i++)
                roadSegments.Add(RoadSegmentController.GetCopy(_roadSegments[i]));
            Vector2 startPosition = _startPosition + StartDirection * respawnDeviation;
            roadSegments[0] = new LinearRoadSegmentController(startPosition, _firstTargetPosition);
            float virusSpeed = _groupSpeed * (_routeDistance - respawnDeviation * _convergenceFactor) / _routeDistance;

            Virus virus = Object.Instantiate(VirusPrefab, startPosition, Quaternion.identity).GetComponent<Virus>();
            virus.Initialize(roadSegments, _routeDistance - respawnDeviation, virusSpeed, Random.Range(0, Mathf.PI / 2));
        }

        private IEnumerable<RoadSegmentController> CreateSegments(Vector2 startPosition, Vector2 endPosition, List<BacteriumProximity> bacteriumProximities)
        {
            int bacteriumIndex = 0;
            List<RoadSegmentController> roadSegments = new List<RoadSegmentController>
            {
                new LinearRoadSegmentController(startPosition, bacteriumProximities[bacteriumIndex].StartPosition)
            };
            for (; bacteriumIndex < bacteriumProximities.Count - 1; bacteriumIndex++)
            {
                roadSegments.Add(new CircleRoadSegmentController(bacteriumProximities[bacteriumIndex].BindedBacterium.Transform.Circle, bacteriumProximities[bacteriumIndex].StartDirection, bacteriumProximities[bacteriumIndex].EndDirection, bacteriumProximities[bacteriumIndex].ClockWise));
                roadSegments.Add(new LinearRoadSegmentController(bacteriumProximities[bacteriumIndex].EndPosition, bacteriumProximities[bacteriumIndex + 1].StartPosition));
            }
            roadSegments.Add(new CircleRoadSegmentController(bacteriumProximities[bacteriumIndex].BindedBacterium.Transform.Circle, bacteriumProximities[bacteriumIndex].StartDirection, bacteriumProximities[bacteriumIndex].EndDirection, bacteriumProximities[bacteriumIndex].ClockWise));
            roadSegments.Add(new LinearRoadSegmentController(bacteriumProximities[bacteriumIndex].EndPosition, endPosition));
            return roadSegments;
        }
    }
}
