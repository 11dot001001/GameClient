using Devdeb.Maths.Geometry2D;
using UnityEngine;

namespace Assets.Scripts
{
    public class CircleRoadSegmentController : RoadSegmentController
    {
        private readonly Circle _circle;
        private readonly Vector2 _startDirection;
        private readonly Vector2 _endDirection;
        private readonly Vector2 _startPosition;
        private readonly Vector2 _endPosition;
        private readonly bool _clockWise;

        public CircleRoadSegmentController(Circle circle, Vector2 startDirection, Vector2 endDirection, bool clockWise)
        {
            _circle = circle;
            _startDirection = startDirection.normalized;
            _endDirection = endDirection.normalized;
            _startPosition = _circle.Position + _startDirection * _circle.Radius;
            _endPosition = _circle.Position + _endDirection * _circle.Radius;
            _clockWise = clockWise;
            Distance = Circle.ArcLength(circle, startDirection, endDirection, clockWise);
        }

        public Circle Circle => _circle;
        public Vector2 StartDirection => _startDirection;
        public Vector2 EndDirection => _endDirection;
        public bool ClockWise => _clockWise;

        public override float Move(VirusMovement virus, float amplitude, float frequency, float phase, float _totalDistance)
        {
            Vector2 newTarget = Circle.Lerp(_startDirection, _endDirection, _clockWise, Progress).normalized;
            virus.transform.position = _circle.Position + newTarget * _circle.Radius;
            virus.transform.up = _clockWise ? newTarget : -newTarget;
            virus.transform.position += virus.transform.up * amplitude * Mathf.Sin(_totalDistance * frequency + phase);
            virus.transform.Rotate(0F, 0F, Mathf.Atan(amplitude * Mathf.Cos(_totalDistance * frequency + phase)) * Mathf.Rad2Deg);
            return virus.FrameSpeed;
        }
        public override object Clone() => new CircleRoadSegmentController(_circle, _startDirection, _endDirection, _clockWise);
    }
}