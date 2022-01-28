using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebScript : MonoBehaviour
{
    
    [SerializeField] private float _verticalBorder = 6.2f;
    [SerializeField] private float _horizontalBorder = 4.5f;
    
    [SerializeField] private float _speed = 1.8f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > -_horizontalBorder && transform.position.y < _horizontalBorder
            && transform.position.x > -_verticalBorder && transform.position.x < _verticalBorder )
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
