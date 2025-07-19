using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public Color tint;
    public Vector2 direction;
    public float rotation;
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    [SerializeField] float tintAmount;

    [SerializeField] AsteroidData data;
   
    // TODO: Create a private but Serialized reference to AsteroidData called data.
    
    private GameObject child;
    private int currentPhase;
    void Start()
    {
        currentPhase = 0;
        direction = new Vector2(Random.Range(data.minDirections[currentPhase], data.maxDirections[currentPhase]) * RandomSign(),
        Random.Range(data.minDirections[currentPhase], data.maxDirections[currentPhase]) * RandomSign());
        rotation = Random.Range(minRotation, maxRotation) * RandomSign();
        tint = new Color(1f - Random.Range(0f, tintAmount), 1f - Random.Range(0f, tintAmount), 1f - Random.Range(0f, tintAmount));
        child = transform.GetChild(0).gameObject;
        child.transform.localScale = new Vector3(data.scales[currentPhase], data.scales[currentPhase], 1f);
        child.GetComponent<SpriteRenderer>().color = tint;
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
        child.transform.Rotate(0f, 0f, rotation * Time.deltaTime);
        // Wrap asteroid to other side of screen.
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

    private int RandomSign()
    {
        return 1 - 2 * Random.Range(0, 2); // returns 1 or -1
    }
}
