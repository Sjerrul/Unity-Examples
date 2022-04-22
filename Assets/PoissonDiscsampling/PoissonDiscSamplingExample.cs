using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace PoissonDiscsampling
{
    public class PoissonDiscSamplingExample : MonoBehaviour
    {
        public float radius = 1f;
        public Vector2 regionSize = Vector2.one;
        public int numberOfSamples = 30;
        public float displayRadius = 1f;
        
        private List<Vector2> points;
        
        // Triggered when properties change in editor
        private void OnValidate()
        {
            points = PoissonDiscSampling.GeneratePoints(radius, regionSize, numberOfSamples);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(regionSize / 2, regionSize);
            if (points != null)
            {
                foreach (var point in points)
                {
                    Gizmos.DrawSphere(point, displayRadius);
                }
            }
        }
    }

}

