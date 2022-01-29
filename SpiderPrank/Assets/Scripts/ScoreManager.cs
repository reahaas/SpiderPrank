using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreContentUIGameObject;    


    private ScoreContentUI scoreContentUI;

    public static ScoreManager instance;
    private bool isGameRunning = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Canvas canvas = this.gameObject.GetComponent<Canvas>();
        canvas.enabled = true;
        this.scoreContentUI = new ScoreContentUI(scoreContentUIGameObject);
        this.scoreContentUI.debugLogScoreContent();
    }

    void Update()
    {
        if (isGameRunning == true)
           this.updatePointsBySurvive();
        this.scoreContentUI.updatescoreContentUIGameObject();
        // this.debugLogScoreContent();
    }

    void updatePointsBySurvive()
    {
        int liveEnemies = this.scoreContentUI.scoreContentData.enemies;
        int earnedPoints = (liveEnemies + 1) * 1;
        this.scoreContentUI.updatePoints(earnedPoints);
    }

    public void updateKillsOneKill()
    {        
        this.scoreContentUI.updateKills(1);

        int earnedPointsByKill = 5000;
        this.scoreContentUI.updatePoints(earnedPointsByKill);
        updateDecreaseEnemiesCount();
    }

    public void updateEggsOneEgg()
    {
        this.scoreContentUI.updateEggs(1);
        int earnedPointsByNewEgg = 2000;
        this.scoreContentUI.updatePoints(earnedPointsByNewEgg);
    }

    public void updateIncreaseEnemiesCount()
    {
        this.scoreContentUI.updateEnemies(1);
    }

    private void updateDecreaseEnemiesCount()
    {
        this.scoreContentUI.updateEnemies(-1);
    }

    public void resetScore()
    {
        this.scoreContentUI.scoreContentData.reset();
        this.isGameRunning = true;
    }

    public void stopGame()
    {
        this.isGameRunning = false;
    }
}
