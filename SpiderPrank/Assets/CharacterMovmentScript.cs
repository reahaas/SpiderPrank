using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovmentScript : MonoBehaviour
{
    [SerializeField] private float speed = 300f;

    private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boundingBox;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 velocity = new Vector2(0, 1);

        velocity *= speed;
        velocity *= Time.deltaTime;

        rb.velocity = velocity;
    }

    private void HandleOutOfBounds()
    {
        if (rb.position.y > boundingBox.bounds.center.y + boundingBox.bounds.extents.y)
        {
            // Out of top
            rb.position = new Vector2(rb.position.x, boundingBox.bounds.center.y - boundingBox.bounds.extents.y);
        }
        if (rb.position.y < boundingBox.bounds.center.y - boundingBox.bounds.extents.y)
        {
            // Out of bottom
            rb.position = new Vector2(rb.position.x, boundingBox.bounds.center.y + boundingBox.bounds.extents.y);
        }
        if (rb.position.x > boundingBox.bounds.center.x + boundingBox.bounds.extents.x)
        {
            // Out of right
            rb.position = new Vector2(boundingBox.bounds.center.x - boundingBox.bounds.extents.x, rb.position.y);
        }
        if (rb.position.x < boundingBox.bounds.center.x - boundingBox.bounds.extents.x)
        {
            // Out of left
            rb.position = new Vector2(boundingBox.bounds.center.x + boundingBox.bounds.extents.x, rb.position.y);
        }
    }
}
