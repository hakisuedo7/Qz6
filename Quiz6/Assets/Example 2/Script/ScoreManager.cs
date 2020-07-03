using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour {
	public Text scoreText;
	private int score;
	public static ScoreManager singleton;
	public string playerID;

	void Awake () {
		score = 0;
		singleton = this;
	}

	public void ChangeScore(int value){
		score += value;
		scoreText.text = score.ToString();
	}

	[ClientRpc]
	public void RpcChangeScore(string playerID, int value){
		if(playerID == "All" || playerID == this.playerID)
			ChangeScore(value);
	}
}
