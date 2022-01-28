using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitcher : MonoBehaviour
{
    [SerializeField] private Image _button;
     private GameObject _player;

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
        _player = GameObject.Find("Player");
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
    }

    public void actionButtonClick()
    {
        switch (lastImage)
        {
            case 0: // attack
                break;
            case 1: // lay egg
                break;
            case 2: // turn left
                //_player;
                break;
            case 3: // turn right
                break;

        }
    }
}
