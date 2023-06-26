using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBoundaryScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShot")
        {
            ShotScript playerShot = collision.GetComponent<ShotScript>();
            playerShot.DestroyGameObject();
        }
        
    }
}
