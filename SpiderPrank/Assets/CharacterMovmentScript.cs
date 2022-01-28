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

        Vector2 adjustedPosition = HandleOutOfBounds();

        rb.position = adjustedPosition;
    }

    private Vector2 HandleOutOfBounds()
    {
        Vector2 outOfBounds = Vector2.zero;

        if (rb.position.y > boundingBox.bounds.center.y + boundingBox.bounds.extents.y)
        {
            // Out of top
            outOfBounds.y = -1;
        }
        if (rb.position.y < boundingBox.bounds.center.y - boundingBox.bounds.extents.y)
        {
            // Out of bottom
            outOfBounds.y = 1;
        }
        if (rb.position.x > boundingBox.bounds.center.x + boundingBox.bounds.extents.x)
        {
            // Out of right
            outOfBounds.x = -1;
        }
        if (rb.position.x < boundingBox.bounds.center.x - boundingBox.bounds.extents.x)
        {
            // Out of left
            outOfBounds.x = 1;
        }

        print(outOfBounds);

        return new Vector2(
            rb.position.x + (outOfBounds.x * boundingBox.bounds.size.x),
            rb.position.y + (outOfBounds.y * boundingBox.bounds.size.y)
        );
    }
}
