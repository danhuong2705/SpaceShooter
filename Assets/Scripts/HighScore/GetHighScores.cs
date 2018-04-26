using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GetHighScores : MonoBehaviour {
   public static List<HighScore> highScoreList = new List<HighScore>();
    public static GetHighScores current;
    void Awake()
    {
        current = this;
    }
    public  List<HighScore> GetHighScoreList()
    {
        LoadHighScoreList();
        return highScoreList;
    }
    public void LoadHighScoreList()
    {
        if (File.Exists(Application.persistentDataPath + "/a2.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/a2.gd", FileMode.Open);
            highScoreList = (List<HighScore>)bf.Deserialize(file);
            file.Close();
        }
        SortByScore();
        SaveHighScoreList();
    }
    public void SaveHighScoreList()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/a2.gd");
        bf.Serialize(file, highScoreList);
        file.Close();
    }
    public void AddHighScore(string userName,int score)
    {
        LoadHighScoreList();
        highScoreList.Add(new HighScore(userName, score));
        SaveHighScoreList();
    }
    public void SortByScore()
    {
        highScoreList.Sort(
            delegate (HighScore p1, HighScore p2)
            {
                return p1.Score.CompareTo(p2.Score);
            }
        );
    }
}
