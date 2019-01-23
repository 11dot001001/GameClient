using Devdeb.Maths.Geometry2D;
using GameCore.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class VirusGroapRoadManager
    {
        private readonly Road _road;
        private readonly float _maximumDeviationAmplitude;

        public VirusGroapRoadManager(Road road, float maximumDeviationAmplitude)
        {
            _road = road;
            _maximumDeviationAmplitude = maximumDeviationAmplitude;
        }

        public IEnumerable<Vector2> GetCorrectedRoad()
        {
            List<Vector2> newRoad = new List<Vector2>
            {
                _road.Start.Transform.Position
            };
            for (int bacteriumProximity = 0; bacteriumProximity < _road.BacteriumProximities.Count; bacteriumProximity++)
            {
                Geometry2D.Rotate(_road.BacteriumProximities[bacteriumProximity].BindedBacterium.Transform.Circle, _road.BacteriumProximities[bacteriumProximity].StartDirection, _road.BacteriumProximities[bacteriumProximity].EndDirection, 5F, _road.BacteriumProximities[bacteriumProximity].IsClockRotate, out List<Vector2> directions);
                newRoad.AddRange(directions.Select(direction => _road.BacteriumProximities[bacteriumProximity].BindedBacterium.Transform.Position + direction));
            }
            newRoad.Add(_road.End.Transform.Position);

            return newRoad;
            //List<Vector2> newRoad = new List<Vector2>
            //{
            //    _road[0].Position
            //};

            //for (int roadPoint = 0; roadPoint < _road.Count - 1; roadPoint++)
            //{
            //    Vector2 lastPosition = _road[roadPoint];
            //    Vector2 targetPosition = _road[roadPoint + 1];

            //    float segmentDistance = (targetPosition - lastPosition).magnitude;
            //    Vector2 targetDirection = (targetPosition - lastPosition).normalized;
            //    int intervalCount = (int)(segmentDistance * _maximumDeviationAmplitude);
            //    float intervalDistance = intervalCount != 0 ? segmentDistance / intervalCount : segmentDistance;
            //    Vector2 intervalDirection = targetDirection;

            //    if (intervalCount == 0)
            //        newRoad.Add(_road[roadPoint]);
            //    for (int intervalIteration = 1; intervalIteration <= intervalCount; intervalIteration++)
            //    {
            //        if (intervalIteration == intervalCount)
            //            if (roadPoint + 1 == _road.Count - 1)
            //            {
            //                newRoad.Add(targetPosition);
            //                return newRoad;
            //            }
            //            else
            //                intervalDirection = (_road[roadPoint + 2] - targetPosition).normalized;

            //        int randomRotate = Random.Range(0, 2);
            //        randomRotate = randomRotate == 0 ? -1 : randomRotate;
            //        float randomAmplitude = Random.Range(-_maximumDeviationAmplitude, _maximumDeviationAmplitude);

            //        newRoad.Add(lastPosition + targetDirection * intervalDistance * intervalIteration + new Vector2(intervalDirection.y * randomRotate, intervalDirection.x * -randomRotate) * randomAmplitude);
            //    }
            //}
            //return newRoad;
        }
    }
}
