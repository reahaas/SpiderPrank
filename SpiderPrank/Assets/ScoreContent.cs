using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreContent : MonoBehaviour
{
    public int points;
    public int kills;
    public int eggs;
    public int enemies;

    public ScoreContent(int points, int kills, int eggs, int enemies)
    {
        this.points = points;
        this.kills = kills;
        this.eggs = eggs;
        this.enemies= enemies;
    }
}
