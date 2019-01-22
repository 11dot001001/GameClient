using GameCore.Tools;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private VirusMovement _virusMovement;

    private void Awake() => _virusMovement = GetComponent<VirusMovement>();
    public void Initialize(List<Vector2> road, float speed) => _virusMovement.Initialize(road, speed);
}
