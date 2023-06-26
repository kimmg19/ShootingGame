using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundaryScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid") {
            AsteroidScript asteroidScript= collision.GetComponent<AsteroidScript>();
            asteroidScript.DestroyGameObject();
        }
        else if(collision.tag == "Enemy")
        {
            EnemyScript enemyScript = collision.GetComponent<EnemyScript>();
            enemyScript.DestroyGameObject();
        }
        else if (collision.tag == "EnemyShot")
        {
            EnemyShotScript enemyShotScript = collision.GetComponent<EnemyShotScript>();
            enemyShotScript.DestroyGameObject();
        }
        else if (collision.tag == "Item")
        {
            CoinScript coinScript = collision.GetComponent<CoinScript>();
            coinScript.DestroyGameObject();
        }
    }
}
