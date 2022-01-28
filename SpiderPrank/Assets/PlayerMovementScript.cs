using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
 [SerializeField] private float _speed = 3f;
    [SerializeField] private float _verticalBorder = 4.5f;
    [SerializeField] private float _horizontalBorder = 4.5f;
    [SerializeField] private int hp = 3;
    private bool _invincible = false;
    private bool _protected = false;

    [SerializeField] private float _defFireRate = 2f;

    [SerializeField] private float _fireRate;
    private float _nextFire = 0.0f;

    private bool leftRight = false;
    [SerializeField] private GameObject regularWaterdropPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if (Input.GetAxis("Fire1") > 0)
            fire();
    }

    void move()
    {
		Vector3 dest = transform.position + Vector3.up * Time.deltaTime * _speed;
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
    }
    
    private void fire()
    {   
        Vector3 spawnAt = transform.position + Vector3.up;
        Instantiate(regularWaterdropPrefab, spawnAt, Quaternion.identity);
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
            Destroy(this.gameObject);
        }
		else{
			float blinkingTotalDuration = 1f;
			float endTime = Time.realtimeSinceStartup + blinkingTotalDuration; 
			float timeLeft = blinkingTotalDuration;
			float intervals = blinkingTotalDuration/4;
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


 
}