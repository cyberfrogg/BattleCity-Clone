using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonDetection : MonoBehaviour
{

    void Update()
    {
        //Collider2D detection = Physics2D.OverlapCircle(transform.position, 1f);
        Collider2D detection = Physics2D.OverlapCircle(transform.position,3f);

        if (detection != null)
        {
            Debug.Log(detection.name);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, 3f);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.transform.parent.CompareTag("dummy") && col.transform.parent.GetInstanceID() != gameObject.transform.parent.GetInstanceID())
        {
            Debug.Log("Hello");
        }
    }
}
