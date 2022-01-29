using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private GameObject scoringBoardCanvas;
    [SerializeField] private GameObject actionButtonCanvas;
    [SerializeField] private GameObject pauseScreenCanvas;
    [SerializeField] private Image startScreen;
    [SerializeField] private Image deathScreen;
    private float deathScreenShowTime;

    [SerializeField] private GameObject player;
    private bool died = false;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBarCanvas.GetComponent<Canvas>().enabled = false;
        scoringBoardCanvas.GetComponent<Canvas>().enabled = false;
        actionButtonCanvas.GetComponent<Canvas>().enabled = false;
        pauseScreenCanvas.GetComponent<Canvas>().enabled = false;
        deathScreen.enabled = false;
        startScreen.enabled = true;
    }

    void startGame()
    {
        startScreen.enabled = false;
        healthBarCanvas.GetComponent<Canvas>().enabled = true;
        scoringBoardCanvas.GetComponent<Canvas>().enabled = true;
        actionButtonCanvas.GetComponent<Canvas>().enabled = true;
        Instantiate(player,new Vector3(0,0,0),Quaternion.identity);
        ScoreManager.instance.resetScore();
    }
    void pauseGame()
    {
        
    }



    public void gameOver()
    {
        actionButtonCanvas.GetComponent<Canvas>().enabled = false;
        deathScreen.enabled = true;
        deathScreenShowTime = Time.realtimeSinceStartup;
        ScoreManager.instance.stopGame();
        cleanScreen();
    }

    private void cleanScreen()
    {
        List<string> tagsToRemove = new List<string>() {"RemoveOnReset", "Enemy"};
        foreach (string tag in tagsToRemove)
        {
            GameObject[] objectsToClean = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objectsToClean)
            {
                Destroy(obj.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startScreen.enabled)
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                startGame();
            }
        }
        else if (deathScreen.enabled)
        {
            if (Time.realtimeSinceStartup - deathScreenShowTime > 2f)
            {
                deathScreen.enabled = false;
                startScreen.enabled = true;
            }
        }
    }
}
