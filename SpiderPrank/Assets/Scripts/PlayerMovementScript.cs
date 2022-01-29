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

	private UIManagerScript uiManager;
  
    // Start is called before the first frame update
    void Start()
    {
		GameObject [] managers = GameObject.FindGameObjectsWithTag("UiManager");
        if (managers.Length > 0)
        {
			uiManager = managers[0].GetComponent<UIManagerScript>();
		}		
    }

    // Update is called once per frame
    void Update()
    {
        move();
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
        if (hp < 0)
        {
            // explode
            gameOverSound.Play();
      			uiManager.gameOver();
            Destroy(this.gameObject);
        }
		else{
			GameObject [] healthBars = GameObject.FindGameObjectsWithTag("HealthBar");
     	 	if (healthBars.Length > 0)
     	   {
				healthBars[0].GetComponent<HealthBarScript>().updateLives(hp);
			}		
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