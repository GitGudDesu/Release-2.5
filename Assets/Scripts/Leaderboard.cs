
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    //create array lists for the DB
    public Text user1 = null;
    public Text user2 = null;
    public Text user3 = null;
    public Text user4 = null;
    public Text user5 = null;
    public Text score1 = null;
    public Text score2 = null;
    public Text score3 = null;
    public Text score4 = null;
    public Text score5 = null;
    public Text AvgScore = null;

	private ArrayList userList = new ArrayList();
    private ArrayList scoreList = new ArrayList();
    private int StuID = SubmitName.getStuID();



    void Start ()
	{
		const string connectionString = "URI=file:Assets\\Plugins\\MumboJumbos.db";
		IDbConnection dbcon = new SqliteConnection (connectionString);
		dbcon.Open ();


		//create query for user name
		IDbCommand dbcmd = dbcon.CreateCommand();
        const string sql =
            "SELECT StuUserName, totalScore " +
            "FROM score sc " +
            "JOIN student s " +
            "ON s.StuID = sc.userID " +
            "ORDER BY totalScore desc " +
			"LIMIT 5";
		
		dbcmd.CommandText = sql;
		IDataReader reader = dbcmd.ExecuteReader();

		while (reader.Read())
		{
			string currentScore = reader.GetString(1);
			string currentUser = reader.GetString(0);


			//Debug.Log(currentWord);

			scoreList.Add(currentScore);
			userList.Add(currentUser);

		}

        for (int i = 0; i <= 5; i++) 
		{

			if(i == 0){
                user1.text = userList[i].ToString() ;
			    score1.text = scoreList[i].ToString();
			}
			else if(i == 1){
				user2.text = userList[i].ToString();
				score2.text = scoreList[i].ToString();
            }
			else if(i == 2){
				user3.text = userList[i].ToString();
                score3.text = scoreList[i].ToString();
            }
			else if(i == 3){
				user4.text = userList[i].ToString();
                score4.text = scoreList[i].ToString();
            }
			else if(i == 4){
				user5.text = userList[i].ToString();
                score5.text = scoreList[i].ToString();
            }
		}
        if(StuID > 0)
        {
            const string connectionStringOne = "URI=file:Assets\\Plugins\\MumboJumbos.db";
            IDbConnection dbconOne = new SqliteConnection(connectionStringOne);
            dbconOne.Open();


            //create query for user name
            IDbCommand dbcmdOne = dbcon.CreateCommand();
            const string sqlOne =
                "SELECT avg(totalScore) " +
                "FROM score " +
                "WHERE userID = @StuID " +
                "group by userID ";

            dbcmdOne.Parameters.Add(new SqliteParameter("@StuID", StuID));

            dbcmdOne.CommandText = sqlOne;
            IDataReader readerOne = dbcmdOne.ExecuteReader();

            while (readerOne.Read())
            {
                AvgScore.text = "AVG SCORE: " + readerOne.GetString(0);
            }
        }
    }
}
