using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitcher : MonoBehaviour
{
    [SerializeField] private Image _button;

    /*
     * type 0 = attack
     * type 1 = egg
     * type 2 = turn left
     * type 3 = turn right
     */
    [SerializeField] private Sprite[] _buttonTypes;

    private float _lastSwitch = 0f;
    public int lastImage;
    
    // Start is called before the first frame update
    void Start()
    {
        lastImage = Random.Range(0, 4);
        _button.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.realtimeSinceStartup - _lastSwitch > 1.5f) // every 1.5 seconds change the image and   
            // functionality of the button
        {
            _lastSwitch = Time.realtimeSinceStartup; // reset timer
            changeButton();

        }
    }

    public void changeButton()
    {
            int nextImage = Random.Range(0, 4); // roll random number 0 - 3
            if (nextImage != lastImage) 
                lastImage = nextImage;
            else
            {
                if (lastImage != 4)
                {
                    lastImage++;
                }
                else
                {
                    lastImage--;
                }
            }
            lastImage = lastImage % 4;
            //lastImage ++;
            _button.sprite = _buttonTypes[lastImage];
    }
    public int getActionNumber()
    {
        _lastSwitch = Time.realtimeSinceStartup; // reset timer
        return lastImage;
    }

}
