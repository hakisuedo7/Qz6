using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour {
	[SerializeField, SyncVar]
	protected int health = 100;
	protected int maxHealth;
	[SerializeField]
	protected int score = 10;
	public Texture frame;
	
	void Awake(){
		maxHealth = health;
	}

	//取得分數
	public int GetScore(){
		return score;
	}

	//當生命值歸零時
	public virtual void EndOfLife(string playerID){
		ScoreManager.singleton.RpcChangeScore(playerID, score);
		Destroy (gameObject);
	}
	
	//受到傷害
	public void TakeDamage(int value, string playerID = "All"){
		health -= value;
		if(health <= 0){
			EndOfLife(playerID);
		}
	}

	//畫出生命條
	void OnGUI(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		
		// draw health bar background
		GUI.color = Color.grey;
		GUI.DrawTexture (new Rect(pos.x-26, Screen.height - pos.y + 40, 52, 7), frame);
		
		// draw health bar amount
		GUI.color = Color.green;
		GUI.DrawTexture (new Rect(pos.x-25, Screen.height - pos.y + 41, 50f * (float)health/maxHealth, 5), frame);	
	}
}
