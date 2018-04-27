
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
	private Text user1 = null;
	private Text user2 = null;
	private Text user3 = null;
	private Text user4 = null;
	private Text user5 = null;
	private Text score1 = null;
	private Text score2 = null;
	private Text score3 = null;
	private Text score4 = null;
	private Text score5 = null;

	public ArrayList userList = new ArrayList();
	public ArrayList scoreList = new ArrayList();




	void Start ()
	{
		const string connectionString = "URI=file:Assets\\Plugins\\MumboJumbos.db";
		IDbConnection dbcon = new SqliteConnection (connectionString);
		dbcon.Open ();


		//create query for user name
		IDbCommand dbcmd = dbcon.CreateCommand();
		const string sql =
			"SELECT StuUserName, totalScore " +
			"FROM score " +
			"WHERE score.Difficulty like '%E%' " +
			"JOIN student " +
			"ON score.userID = student.StuID " +
			"ORDER BY totalScore " +
			"LIMIT 5";
		
		dbcmd.CommandText = sql;
		IDataReader reader = dbcmd.ExecuteReader();

		while (reader.Read())
		{
			string currentScore = reader.GetString(2);
			string currentUser = reader.GetString(7);


			//Debug.Log(currentWord);

			scoreList.Add(currentScore);
			userList.Add(currentUser);

		}

		for(int i = 0; i < scoreList.Length();i++) 
		{
				if(i == 0){
					user1.text = userList[i];
				score1.text = scoreList[i];
				}
			else if(i == 1){
				user2.text = userList[i];
				score2.text = scoreList[i];
				}
			else if(i == 2){
				user3.text = userList[i];
				score3.text = scoreList[i];
				}
			else if(i == 3){
				user4.text = userList[i];
				score4.text = scoreList[i];
				}
			else if(i == 4){
				user5.text = userList[i];
				score5.text = scoreList[i];
				}
		}		
}
}
