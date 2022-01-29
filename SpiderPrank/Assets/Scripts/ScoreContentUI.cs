using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreContentUI : MonoBehaviour
{
    // [SerializeField] private GameObject scoreContentUIGameObject;
    private GameObject scoreContentUIGameObject;

    public ScoreContentData scoreContentData;

    public ScoreContentUI(GameObject scoreContentUIGameObject)
    {
        this.scoreContentUIGameObject = scoreContentUIGameObject;
        this.scoreContentData = new ScoreContentData(0, 0, 0, 0);
    }

    public void updatescoreContentUIGameObject()
    {
        foreach (Transform child in this.scoreContentUIGameObject.transform)
        {
            string dataLable = child.name;
            string dataValue = this.scoreContentData.getData(dataLable).ToString();
            child.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = dataValue;
        }
    }

    public void debugLogScoreContent()
    {
        foreach (Transform child in this.scoreContentUIGameObject.transform)
        {
            Debug.Log("child name: " + child.name + ", value:" + this.scoreContentData.getData(child.name).ToString());
        }
    }

    public void updatePoints(int diff)
    {

        this.scoreContentData.points += diff;
    }
    public void updateKills(int diff)
    {
        this.scoreContentData.kills += diff;
    }
    public void updateEggs(int diff)
    {
        this.scoreContentData.eggs += diff;
    }
    public void updateEnemies(int diff)
    {
        this.scoreContentData.enemies += diff;
    }
}
