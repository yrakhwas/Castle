using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    static public GameObject POI;

    [Header("Set in Inspector")]
    public float easing = 0.05f;

    [Header("Set Dynamically")]
    public float camZ;


    public Vector2 minXY = Vector2.zero;
    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        Vector3 destination;
        if (POI == null) 
        { 
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;
            if(POI.tag == "Projectile")
            {
                if(POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }

        }
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);


        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}
