using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static float score = 0.0f;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 2;
    private int scoreToNextLevel = 100;
    private bool isDeath = false;
    public Text scoreText;
    public DeathMenu deathMenu;



    public object FindData(string tableName, string selectHeader, string whereHeader, object whereValue)
    {
        using (var dbConnection = new SqliteConnection("URI=file:" + Application.dataPath + "/Plugins/MumboJumbos.db"))
        {
            dbConnection.Open();

            using (var dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = string.Format("SELECT {0} " +
                                                       "FROM {1} " +
                                                       "WHERE {2} = @whereValue", selectHeader, tableName, whereHeader);

                SqliteParameter whereParam = new SqliteParameter("@whereValue", whereValue);
                dbCommand.Parameters.Add(whereParam);

                using (var dbReader = dbCommand.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (dbReader.GetValue(0) != null)
                            return dbReader.GetValue(0);
                    }
                }
            }
        }
        return null;
    }

    public static void UpdateData(string tableName, string whereHeader, object whereValue, string setHeader, object setValue)
    {
        using (var dbConnection = new SqliteConnection("URI=file:" + Application.dataPath + "/Plugins/MumboJumbos.db"))
        {
            dbConnection.Open();

            using (var dbCommand = dbConnection.CreateCommand())
            {

                dbCommand.CommandText = string.Format("UPDATE {0} " + "SET {1} = @setValue " + "WHERE {2} = @whereValue",
                    tableName, setHeader, whereHeader);

                SqliteParameter setParam = new SqliteParameter("@setValue", setValue);
                SqliteParameter whereParam = new SqliteParameter("@whereValue", whereValue);

                dbCommand.Parameters.Add(setParam);
                dbCommand.Parameters.Add(whereParam);

                Debug.Log(dbCommand.CommandText);

                dbCommand.ExecuteNonQuery();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(isDeath){
            return;
        }
        if(score >= scoreToNextLevel) {
           // LevelUp();
        }
        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp() {
        if(difficultyLevel == maxDifficultyLevel) {
            return;
        }
        scoreToNextLevel *= 2;
        difficultyLevel++;
        GetComponent<CharacterControl>().SetSpeed(difficultyLevel);

        Debug.Log(difficultyLevel);
    }

    public void OnDeath() {
        isDeath = true;
        PlayerPrefs.SetFloat("Highscore", score);
        deathMenu.ToggleEndMenu (score);
        var hscore = FindData("student", "Score", "StuID", SubmitName.getStuID());
        int hscores = Convert.ToInt32(hscore);
        if (hscores < score) {
            UpdateData("student", "StuID", SubmitName.getStuID(), "Score", score);
        }
        Score.score = 0;
    }
}
