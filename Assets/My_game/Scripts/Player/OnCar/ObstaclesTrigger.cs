using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            Destroy(collision.gameObject);
        }
    }
}
