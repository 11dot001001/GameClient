using GameCore.Tools;
using System.Linq;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private float _speed;
    private Road _road;
    private int _roadOffset;
    private WayPosition _target;

    private void Move()
    {
        if ((Vector2)transform.position == _road.Points[_roadOffset].position)
            _roadOffset++;
        transform.position = Vector2.MoveTowards(transform.position, _road.Points[_roadOffset].position, _speed * Time.deltaTime);
    }

    public void Initialize(Road road, float speed)
    {
        _road = road;
        _speed = speed;
        _target = road.Points[road.Points.Count - 1];
    }

    bool _flag;
    private void Update()
    {
        if (((Vector2)transform.position-_target.position).magnitude <0.2f)
        {
            Destroy(gameObject);
            _flag = true;
        }
        else if(!_flag)
            Move();
    }
}
