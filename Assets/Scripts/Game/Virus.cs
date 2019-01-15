using GameCore.Tools;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private float _speed;
    private Road _road;
    private float _progress;
    private int _pointIndex;

    private void FixedUpdate()
    {
        _progress += _speed * Time.fixedDeltaTime;
        float currentProgress = 0f;
        Vector2 lastPosition = _road.Points[0];
        Vector2 currentPosition = Vector2.zero;
        float difference = 0f;
        for (_pointIndex = 1; _pointIndex < _road.Points.Count; _pointIndex++)
        {
            currentPosition = _road.Points[_pointIndex];
            difference = (currentPosition - lastPosition).magnitude;
            if (_progress < currentProgress + difference)
                break;
            if (_pointIndex == _road.Points.Count - 1)
                Destroy(gameObject);
            lastPosition = currentPosition;
            currentProgress += difference;
        }
        transform.position = Vector2.Lerp(lastPosition, currentPosition, (_progress - currentProgress) / difference);
    }

    public void Initialize(Road road, float speed)
    {
        _road = road;
        _speed = speed;
    }
}
