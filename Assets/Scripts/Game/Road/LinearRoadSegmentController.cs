using UnityEngine;

namespace Assets.Scripts
{
    public class LinearRoadSegmentController : RoadSegmentController
    {
        private readonly Vector2 _startPosition;
        private readonly Vector2 _endPosition;

        public LinearRoadSegmentController(Vector2 startPosition, Vector2 endPosition)
        {
            _startPosition = startPosition;
            _endPosition = endPosition;
            Distance = Vector2.Distance(_startPosition, _endPosition);
        }

        public override float Move(VirusMovement virus, float amplitude, float frequency, float phase, float _totalDistance)
        {
            virus.transform.position = Vector2.Lerp(_startPosition, _endPosition, Progress);
            virus.transform.right = _endPosition - _startPosition;
            virus.transform.position += virus.transform.up * amplitude * Mathf.Sin(_totalDistance * frequency + phase);
            virus.transform.Rotate(0F, 0F, Mathf.Atan(amplitude * Mathf.Cos(_totalDistance * frequency + phase)) * Mathf.Rad2Deg);
            return virus.FrameSpeed;
        }
        public override object Clone() => new LinearRoadSegmentController(_startPosition, _endPosition);
    }
}