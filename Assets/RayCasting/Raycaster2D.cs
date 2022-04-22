using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster2D : MonoBehaviour
{
    public LayerMask Mask;
    public float MaxDistance = 100f;

    void Start()
    {       
        // NOTE:in the 2D system, rays will not by default ignore colliders they start in, disable this manually 
        Physics2D.queriesStartInColliders = false;
    }
    // Update is called once per frame
    void Update()
    {


        RaycastHit2D hitInfo =  Physics2D.Raycast(transform.position, transform.right);
        if (hitInfo.collider != null)
        {
            print (hitInfo.collider.gameObject.name);
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * this.MaxDistance, Color.green);
        }

    }
}
