using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LerpingAndAnimationCurves
{
    public class LerpingAndAnimationCurves : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private Vector3 targetrotation;
        [SerializeField] private Vector3 targetscale;

        [SerializeField] private float speed;

        private float currentLerp;
        private float targetLerp;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                targetLerp = targetLerp == 0 ? 1 : 0;
            }

            currentLerp = Mathf.Lerp(currentLerp, targetLerp, speed * Time.deltaTime);

            transform.position = Vector3.Lerp(Vector3.zero, targetPosition, curve.Evaluate(currentLerp));
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(targetrotation), curve.Evaluate(currentLerp));
            transform.localScale = Vector3.Lerp(Vector3.one, targetscale, curve.Evaluate(currentLerp));
        }
    }
}
