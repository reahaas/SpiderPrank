using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreContentData : MonoBehaviour
{
    public int points;
    public int kills;
    public int eggs;
    public int enemies;

    public ScoreContentData(int points, int kills, int eggs, int enemies)
    {
        this.points = points;
        this.kills = kills;
        this.eggs = eggs;
        this.enemies = enemies;
    }

    public int getData(string dataLable)
    {
        if (dataLable == "Points")
            return this.points;
        else if (dataLable == "Kills")
            return this.kills;
        else if (dataLable == "Eggs")
            return this.eggs;
        else if (dataLable == "Enemies")
            return this.enemies;
        else
            //Debug.Log("Unsupported data requested: [" + dataLable + "]");
            return -1;
    }

    public void reset()
    {
        this.points = 0;
        this.kills = 0;
        this.eggs = 0;
        this.enemies = 0;
    }
}
