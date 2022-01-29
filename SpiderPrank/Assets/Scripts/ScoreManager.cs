using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreContentUIGameObject;
    private ScoreContentUI scoreContentUI;

    private void Awake()
    {
        this.scoreContentUI = new ScoreContentUI(scoreContentUIGameObject);
        this.scoreContentUI.debugLogScoreContent();
    }

    void Update()
    {
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

    void updateKillsOneKill()
    {
        this.scoreContentUI.updateKills(1);

        int earnedPointsByKill = 5000;
        this.scoreContentUI.updatePoints(earnedPointsByKill);
    }

    void updateEggsOneEgg()
    {
        this.scoreContentUI.updateEggs(1);
        int earnedPointsByNewEgg = 2000;
        this.scoreContentUI.updatePoints(earnedPointsByNewEgg);
    }

    void updateIncreaseEnemiesCount()
    {
        this.scoreContentUI.updateEnemies(1);
    }

    void updateDecreaseEnemiesCount()
    {
        this.scoreContentUI.updateEnemies(-1);
    }
}
