using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
       
    void Update()
    {
        // Wrap bullet to other side of screen.
        if (transform.position.x < -8.5f)
        {
            transform.position = new Vector2(8.5f, transform.position.y);
        }
        else if (transform.position.x > 8.5f)
        {
            transform.position = new Vector2(-8.5f, transform.position.y);
        }
        if (transform.position.y < -6.5f)
        {
            transform.position = new Vector2(transform.position.x, 6.5f);
        }
        else if (transform.position.y > 6.5f)
        {
            transform.position = new Vector2(transform.position.x, -6.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            // When an asteroid gets destroyed, make smaller chunks two times. For LE3.
            //
            //
            //
            int index = AsteroidManager.Instance.GetAsteroids().IndexOf(collision.gameObject);
            if (index != -1)
                AsteroidManager.Instance.DeleteAsteroid(index); // Just two lines for readability.
        }
    }
}
