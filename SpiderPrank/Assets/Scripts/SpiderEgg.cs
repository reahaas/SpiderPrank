using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderEgg : MonoBehaviour
{
    [SerializeField] float timeToSpawn;
    [SerializeField] private Sprite enemyImage;
    //[SerializeField] private Image sourceObject;

    private bool transformed = false;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (!transformed && Time.realtimeSinceStartup - startTime > timeToSpawn)
        {
            transformed = true;
            transform.gameObject.tag = "Enemy";
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
            spriteR.sprite = enemyImage;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {		
        if (transformed && col.gameObject.tag == "Weapon")
        {
            Destroy(this.gameObject);
        }
    }
}
