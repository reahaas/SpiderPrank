using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private ButtonSwitcher _buttonSwitcher;
    [SerializeField] private PlayerMovementScript player;

    private float lastShot;

    void Start()
    {
        lastShot = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if (player == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 0)
            {
                player = players[0].GetComponent<PlayerMovementScript>();
            }
        }
    }
    public void actionButtonClick()
    {
        if (Time.realtimeSinceStartup - lastShot > 0.5f)
        {
            lastShot = Time.realtimeSinceStartup;
            
            switch (_buttonSwitcher.getActionNumber())
            {
                case 0: // attack
                    player.fire();
                    break;
                case 1: // lay egg
                    player.layAnEgg();
                    break;
                case 2: // turn left
                    player.turnLeft();
                    break;
                case 3: // turn right
                    player.turnRight();
                    break;
            }
            //_buttonSwitcher.changeButton();

        }
    }
}
