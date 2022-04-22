using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster3D : MonoBehaviour
{
    public LayerMask Mask;
    public float MaxDistance = 100f;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        // Note: Rays collide with the Collider object, NOT the mesh
        // Mask: the layer mask determines which layers interact with the ray that's been cast
        // QueryTriggerInteraction: determines if the ray interacts with a trigger (Ignore will ray through triggers, collide will hit, 
        //                          and useglobal uses settings in projectsettings>physics)
        if (Physics.Raycast(ray, out hitInfo, this.MaxDistance, this.Mask, QueryTriggerInteraction.Ignore))
        {
            print (hitInfo.collider.gameObject.name);
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.direction.normalized * this.MaxDistance, Color.green);
        }

    }
}
