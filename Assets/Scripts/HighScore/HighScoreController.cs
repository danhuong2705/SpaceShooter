using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {
  
    [SerializeField]
    private Transform content;
    [SerializeField]
    private GameObject highScoreItem;

    public void PrintListHighScore(List<HighScore> listHighScore)
    {
        listHighScore = GetHighScores.current.GetHighScoreList();
        for (int i = 0; i < listHighScore.Count; i++)
        {
            GameObject newHighScoreItem = Instantiate(highScoreItem, Vector3.zero, Quaternion.identity, content);
            newHighScoreItem.transform.Find("RankText").GetComponent<Text>().text = (i + 1).ToString();
            newHighScoreItem.transform.Find("NameText").GetComponent<Text>().text = listHighScore[i].UserName;
            newHighScoreItem.transform.Find("ScoreText").GetComponent<Text>().text = listHighScore[i].Score.ToString();
        }
    }
}
