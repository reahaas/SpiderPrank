using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
 [SerializeField] private float _speed = 3f;
    [SerializeField] private float _verticalBorder = 4.5f;
    [SerializeField] private float _horizontalBorder = 4.5f;
    [SerializeField] private int hp = 3;
    [SerializeField] private GameObject camera;
    private bool _invincible = false;
    private bool _protected = false;

    // Sounds:
    [SerializeField] private AudioSource playerTurnSound;
    [SerializeField] private AudioSource playerTakeHitSound;
    [SerializeField] private AudioSource gameOverSound;

    [SerializeField] private GameObject spiderWebPrefab;
    [SerializeField] private BoxCollider2D boundingBox;
    
    [SerializeField] private GameObject spiderEggPrefab;
  
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        move();
        Vector3 adjustedPosition = HandleOutOfBounds();
        transform.position = adjustedPosition;
    }

    private Vector3 HandleOutOfBounds()
    {
	    Vector3 outOfBounds = Vector3.zero;

	    if (transform.position.y > boundingBox.bounds.center.y + boundingBox.bounds.extents.y)
	    {
		    // Out of top
		    outOfBounds.y = -1;
	    }
	    if (transform.position.y < boundingBox.bounds.center.y - boundingBox.bounds.extents.y)
	    {
		    // Out of bottom
		    outOfBounds.y = 1;
	    }
	    if (transform.position.x > boundingBox.bounds.center.x + boundingBox.bounds.extents.x)
	    {
		    // Out of right
		    outOfBounds.x = -1;
	    }
	    if (transform.position.x < boundingBox.bounds.center.x - boundingBox.bounds.extents.x)
	    {
		    // Out of left
		    outOfBounds.x = 1;
	    }


	    return new Vector2(
		    transform.position.x + (outOfBounds.x * boundingBox.bounds.size.x),
		    transform.position.y + (outOfBounds.y * boundingBox.bounds.size.y)
	    );
    }

    void move()
    {
	    Vector3 dest = transform.position + Vector3.up * Time.deltaTime * _speed;
	    if (dest.y > _verticalBorder)
	    {
		    dest.y = -1 * _verticalBorder;
		    transform.position = dest;
	    }

	    if (dest.y < -1 * _verticalBorder)
	    {
		    dest.y = _verticalBorder;
		    transform.position = dest;
	    }

	    if (dest.x > _horizontalBorder)
	    {
		    dest.x = -1 * _horizontalBorder;
		    transform.position = dest;
	    }

	    if (dest.x < -1 * _horizontalBorder)
	    {
		    dest.x = _horizontalBorder;
		    transform.position = dest;
	    }
	
	    transform.Translate(Vector3.up * Time.deltaTime * _speed );

	    /*
	     * Vector3 dest = transform.position + Vector3.up * Time.deltaTime * _speed;
		if (transform.eulerAngles.z == 0) // if going up
		{
			if (dest.y < _verticalBorder)
				transform.Translate(Vector3.up * Time.deltaTime * _speed );
			else
				transform.position = new Vector3(dest.x, -_verticalBorder, dest.z);
		}
		else if (transform.eulerAngles.z == 180) // if going down
        {
			if (dest.y > -1 * _verticalBorder)
				transform.Translate(Vector3.up * Time.deltaTime * _speed );
			else
				transform.position = new Vector3(dest.x, _verticalBorder, dest.z);
		}
	
		else if (transform.eulerAngles.z == 270) // if going right
		{
            if (dest.x < _horizontalBorder)
                transform.Translate(Vector3.up * Time.deltaTime * _speed);
            else
                transform.position = new Vector3(-1 * _horizontalBorder, dest.y, dest.z);  // dest.x, -_verticalBorder, dest.z);
		}
		else if(transform.eulerAngles.z == 90)// if going down
        {
			if (dest.x > -1 * _horizontalBorder)
				transform.Translate(Vector3.up * Time.deltaTime * _speed );
			else
				transform.position = new Vector3(_horizontalBorder, dest.y, dest.z);
		}
	     */
		
    }
    
    public void fire()
    {   
	    Instantiate(spiderWebPrefab, transform.position, transform.rotation);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {		
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyWeapon")
        {
            if (_invincible) // if hit while shielded or w/e
            {
                _invincible = false;
              //  shieldGameObject.SetActive(false);
            }
            else
            {
				if (!_protected)
			        StartCoroutine(takeHit());    	
            }
        }
    }
    
    private IEnumerator takeHit()
    {
		_protected = true;
        hp--;
        //HealthBar bar = healthBar.GetComponent<HealthBar>();
        //bar.gotHit();
        if (hp < 0)
        {
            // explode
            gameOverSound.Play();
            Destroy(this.gameObject);
        }
		else{
			float blinkingTotalDuration = 1f;
			float endTime = Time.realtimeSinceStartup + blinkingTotalDuration; 
			float timeLeft = blinkingTotalDuration;
			float intervals = blinkingTotalDuration/4;

            playerTakeHitSound.Play();

            while (timeLeft > 0)
			{
				timeLeft = endTime - Time.realtimeSinceStartup;
				if (this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
             		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;  //make changes
        		 } else {
            		 this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   //make changes
         		}
				yield return new WaitForSeconds(intervals);
			}
			if (this.gameObject.GetComponent<SpriteRenderer> ().enabled == false) 
			{
             	this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;  //make changes
        	} 
			_protected = false;

		}
    }

    public void turnRight()
    {
        int right_angle = -5;
        this.turn(right_angle);
    }
 
    public void turnLeft()
    {
        int left_angle = 5;
        this.turn(left_angle);
    }

    private void turn(int direction)
    {
        float newAngle = (transform.eulerAngles.z + direction) % 360;
        transform.eulerAngles = new Vector3(0, 0, newAngle);
        camera.transform.eulerAngles = new Vector3(0, 0, newAngle);
        this.playerTurnSound.Play();
    }

    public void layAnEgg()
    {
	    Instantiate(spiderEggPrefab, transform.position, transform.rotation);
    }

}