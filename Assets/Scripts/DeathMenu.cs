using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Text scoreText;
	public Text tokenText;

    public Image backgroundImage;
    private bool isShowed = false;
    private float transition = 0.0f;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isShowed)  {
            return;
        }
        transition += Time.deltaTime;
        backgroundImage.color = Color.Lerp(new Color(0,0,0,0),Color.black,transition);
	}

    public void ToggleEndMenu(float score) {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        isShowed = true;
    }

    public void Restart() {
		SceneManager.LoadScene("Game");
		//direct db connection to where the db is stored in app
		//and open connection
		const string connectionString = "URI=file:Assets\\Plugins\\MumboJumbos.db";
		IDbConnection dbcon = new SqliteConnection(connectionString);
		dbcon.Open();
		IDbCommand dbcmd = dbcon.CreateCommand();

		//create query for adding score
		//dbcmd = null;
		dbcon.CreateCommand();
		const string command =
		"INSERT INTO Score " +
		"(userID, totalScore, balance, grade) " +
		"VALUES (@two, @three, @four, @five)";

		//dbcmd.Parameters.Add(new SqliteParameter("@one", one)); 
		dbcmd.Parameters.Add(new SqliteParameter("@two", SubmitName.getStuID()));
		dbcmd.Parameters.Add(new SqliteParameter("@three", Int32.Parse(scoreText.text)));
		dbcmd.Parameters.Add(new SqliteParameter("@four", Int32.Parse(tokenText.text)));
		dbcmd.Parameters.Add(new SqliteParameter("@five", SubmitName.getStuGrade()));

		string sql = command;
		Debug.Log(sql);
		Debug.Log(SubmitName.getTeachID());
		Debug.Log(scoreText.text);
		Debug.Log (tokenText.text);
		Debug.Log(SubmitName.getStuGrade());

		dbcmd.CommandText = command;
		IDataReader reader = dbcmd.ExecuteReader();

	}

	public void ToMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
		//direct db connection to where the db is stored in app
		//and open connection
		const string connectionString = "URI=file:Assets\\Plugins\\MumboJumbos.db";
		IDbConnection dbcon = new SqliteConnection(connectionString);
		dbcon.Open();
		IDbCommand dbcmd = dbcon.CreateCommand();

		//create query for adding score
		dbcmd = null;
		dbcon.CreateCommand();
		const string command =
			"INSERT INTO Score " +
			"(userID, totalScore, balance, grade) " +
			"VALUES (@two, @three, @four, @five)";

		//dbcmd.Parameters.Add(new SqliteParameter("@one", one)); 
		dbcmd.Parameters.Add(new SqliteParameter("@two", SubmitName.getStuID()));
		dbcmd.Parameters.Add(new SqliteParameter("@three", Int32.Parse(scoreText.text)));
		dbcmd.Parameters.Add(new SqliteParameter("@four", Int32.Parse(scoreText.text)));
		dbcmd.Parameters.Add(new SqliteParameter("@five", SubmitName.getStuGrade()));

		string sql = command;
		Debug.Log(sql);
		Debug.Log(SubmitName.getTeachID());
		Debug.Log(scoreText.text);
		Debug.Log (tokenText.text);
		Debug.Log(SubmitName.getStuGrade());
		dbcmd.CommandText = command;
		IDataReader reader = dbcmd.ExecuteReader();

	} 
		
}
