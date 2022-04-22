using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementAndFollowing
{

    public class ChaserSphere : MonoBehaviour
    {
        public Transform target;

        public float speed = 7f;
        public float stoppingDistance = 1.5f;

        void Update()
        {
            Vector3 displacementFromTarget = target.position - this.transform.position;
            Vector3 direction = displacementFromTarget.normalized;

            Vector3 velocity = direction * speed;
            Vector3 moveAmount = velocity * Time.deltaTime;

            float distanceFromTarget = displacementFromTarget.magnitude;

            if (distanceFromTarget > stoppingDistance)
            {
                this.transform.Translate(moveAmount);
            }

        
        }
    }

}
