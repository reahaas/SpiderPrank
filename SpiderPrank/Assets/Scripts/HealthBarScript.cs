using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    private int hp = 3;
    [SerializeField] private Sprite[] lives;
    [SerializeField] private Image livesDisplay;

    // Start is called before the first frame update
    void Start()
    {
        livesDisplay.enabled = true;
    }

    // Update is called once per frame
    public void updateLives(int health)
    {
        livesDisplay.sprite = lives[health];
    }

    public void resetHP()
    {
        updateLives(this.hp);
    }
}
