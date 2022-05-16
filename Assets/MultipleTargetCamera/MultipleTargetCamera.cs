using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementAndFollowing
{
    [RequireComponent(typeof(Camera))]
    public class MultipleTargetCamera : MonoBehaviour
    {
        public Transform[] gameobjects;

        public float smoothTime = 0.5f;

        public float zoomLimiter = 200f;
        public float minZoom = 100f;
        public  float maxZoom = 40f; 
        public Vector3 offset;

        private Vector3 velocity;

        private Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        void LateUpdate()
        {
            Move();
            Zoom();
        }

        private void Zoom()
        {
            float distance = GetGreatestDistance();
            float newZoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomLimiter);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        }

        private void Move()
        {
            Vector3 centerpoint = GetCenterPoint();
            Vector3 newPosition = centerpoint + offset;


            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        }

        private float GetGreatestDistance()
        {
            Bounds bounds = CreateBounds();
            return bounds.size.x;
        }
        private Vector3 GetCenterPoint()
        {
            Bounds bounds = CreateBounds();
            return bounds.center;
        }

        private Bounds CreateBounds()
        {
            if (gameobjects.Length == 0)
            {
                return new Bounds();
            }

            var bounds = new Bounds(gameobjects[0].position, Vector3.zero);
            for (int i = 1; i < gameobjects.Length; i++)
            {
                bounds.Encapsulate(gameobjects[i].position);
            }

            return bounds;
        }
    }
}