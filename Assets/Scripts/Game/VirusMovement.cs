using GameCore.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMovement : MonoBehaviour
{
    private const float _speedCorrector = Mathf.PI * 0.5F / 0.701983915256665F;
    private const float _maxSpeed = 0.7176653F * _speedCorrector;
    private const float _maxScaleChangeFactor = 0.4F;
    private float _speedFactor;
    private float _x;
    private float _speed;
    private Vector2 _lastRoadPoint, _targetRoadPoint;
    private Road _road;
    private int _lastRoadPointIndex;

    private void FixedUpdate()
    {
        UpdateScale();
        UpdateSpeed();
        UpdatePosition();
    }
    private void UpdateSpeed()
    {
        int count = (int)(_x / (Mathf.PI * 0.5F));
        _x -= count * Mathf.PI * 0.5F;
        _x += Time.fixedDeltaTime;
        _speed = Mathf.Sin(_x) * (Mathf.Cos(_x) / (_x + 0.1F)) * _speedCorrector * _speedFactor;
    }
    private void UpdatePosition()
    {
        float nextPositionLength = ((Vector2)transform.position - _lastRoadPoint).magnitude + _speed * Time.fixedDeltaTime;
        float roadSegmentLength = (_targetRoadPoint - _lastRoadPoint).magnitude;
        float nextPositionFactor = nextPositionLength / roadSegmentLength;

        if (nextPositionFactor < 1f)
        {
            transform.position = Vector2.Lerp(_lastRoadPoint, _targetRoadPoint, nextPositionFactor);
            return;
        }
        _lastRoadPointIndex++;
        if (_lastRoadPointIndex == _road.Count - 1)
        {
            Destroy(gameObject);
            return;
        }
        _lastRoadPoint = _road[_lastRoadPointIndex];
        _targetRoadPoint = _road[_lastRoadPointIndex + 1];
        UpdateRotation();
    }
    private void UpdateRotation() => transform.right = _targetRoadPoint - _lastRoadPoint;
    private void UpdateScale() => transform.localScale = new Vector3(1F + _maxScaleChangeFactor * _speed / (_maxSpeed * _speedFactor), 1F - _maxScaleChangeFactor * _speed / (_maxSpeed * _speedFactor));

    public void Initialize(Road road, float speedFactor)
    {
        _road = road;
        _speedFactor = speedFactor;
        _lastRoadPoint = _road[_lastRoadPointIndex];
        _targetRoadPoint = _road[_lastRoadPointIndex + 1];
        UpdateRotation();
    }
}
