using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    [SerializeField] float bulletLifespan;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float rotSpeed;
    [SerializeField] float moveForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float magnitude;
    private Animator an;
    private Rigidbody2D rb;
    void Start()
    {
        an = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Rotate the ship
        transform.Rotate(new Vector3(0f, 0f, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime));


        // Add forward thrust.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            an.SetBool("hasThrus", true);
            Vector2 force = transform.right * moveForce * Time.deltaTime;
            rb.AddForce(force);
            // Engine sound
            if (Game.Instance != null)
                Game.Instance.SOMA.PlayLoopedSound("Engines");
        }

        // Spawn a bullet.
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Teleport();
        }
        // Wrap player to other side of screen.
        if (Mathf.Abs(transform.position.x) >9f) // a-axis
        {
            transform.position = new Vector2(-(transform.position.x), transform.position.y);    

        }
        if (Mathf.Abs(transform.position.y) >7f) // y-axis
        {
            transform.position = new Vector2(transform.position.x, -(transform.position.y));

        }
    }

    void FixedUpdate()
    {
        // Clamp the magnitude of the ship.
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        // Print the magnitude of the ship.
        magnitude = rb.velocity.magnitude;
    }

    private void SpawnBullet()
    {
        // Play the Fire sound.
        if (Game.Instance != null)
            Game.Instance.SOMA.PlaySound("Fire");

        // Instantiate a bullet at the spawn point.
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, transform.rotation);
        Destroy(bullet, bulletLifespan);

        // Set the bullet's velocity based on the ship's rotation.
        Vector2 force = transform.right * bulletForce;
        bullet.GetComponent<Rigidbody2D>().AddForce(force);

    }

    private void Teleport()
    {
        // Add code to check for a safe position, i.e. a gap in asteroids. For LE3.
        //
        //
        //
        transform.position = new Vector2(Random.Range(-7f,7f), Random.Range(5f,-5f));
        if (Game.Instance != null) 
            Game.Instance.SOMA.PlaySound("Teleport");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            if (Game.Instance != null)
                Game.Instance.SOMA.PlaySound("Explode");
        }
    }
}
