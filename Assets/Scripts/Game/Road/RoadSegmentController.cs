using System;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class RoadSegmentController
    {
        public float Distance;
        public float CoveredDistance;
        public float Progress => CoveredDistance / Distance;

        public abstract float Move(VirusMovement virus, float amplitude, float frequency, float phase, float _totalDistance);
        public abstract object Clone();

        public static RoadSegmentController GetCopy(RoadSegmentController roadSegmentController) =>(RoadSegmentController)roadSegmentController.Clone();
    }
}