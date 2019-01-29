using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class VirusMovementManager
    {
        private readonly VirusMovement _virusMovement;
        private readonly float _routeDistance;
        private Queue<RoadSegmentController> _roadSegments;
        private RoadSegmentController _currentRoadSegment;
        private readonly float _speed;
        private float _currentDistance;
        private float Progress => _currentDistance / _routeDistance;
        public EventHandler Arrived;
        public float Phase;
        public float Amplitude;
        public float Frequency;

        public VirusMovementManager(IEnumerable<RoadSegmentController> roadSegments, float routeDistance, VirusMovement virusMovement, float groupSpeed, float phase)
        {
            _routeDistance = routeDistance;
            _virusMovement = virusMovement;
            _speed = groupSpeed;
            _virusMovement.Initialize(UnityEngine.Random.Range(0, Mathf.PI / 2));
            _roadSegments = new Queue<RoadSegmentController>(roadSegments);
            _currentRoadSegment = _roadSegments.Dequeue();
            Phase = phase;
            Amplitude = UnityEngine.Random.Range(0.1F, 0.3F);
            Frequency = UnityEngine.Random.Range(0.3F, 1.5F);
            ChangeSpeed();
        }
        public void Move()
        {
            ChangeSpeed();
            if (_currentRoadSegment.Progress >= 1F)
                if (_roadSegments.Count != 0)
                {
                    RoadSegmentController roadSegment = _roadSegments.Dequeue();
                    roadSegment.CoveredDistance = _currentRoadSegment.CoveredDistance - _currentRoadSegment.Distance;
                    _currentRoadSegment = roadSegment;
                }
                else
                {
                    Arrived?.Invoke(this, EventArgs.Empty);
                    return;
                }
            float moveInterval = _currentRoadSegment.Move(_virusMovement, Amplitude, Frequency, Phase, _currentDistance);
            _currentRoadSegment.CoveredDistance += moveInterval;
            _currentDistance += moveInterval;
        }
        public void ChangeSpeed() => _virusMovement.Speed = _speed;
    }
}