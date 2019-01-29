using Assets.Scripts;
using GameCore.Tools;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private VirusMovementManager _virusMovementManager;

    private void FixedUpdate() => _virusMovementManager.Move();

    public void Initialize(IEnumerable<RoadSegmentController> roadSegments, float routeDistance, float speedFactor, float phase)
        => _virusMovementManager = new VirusMovementManager(roadSegments, routeDistance, GetComponent<VirusMovement>(), speedFactor, phase);
}
