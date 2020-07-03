using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AutoGenerate : NetworkBehaviour {
	public GameObject enemyPrefab;
	public float createHeight = 5f;
	public float xMin = -10f;
	public float xMax = 10f;
	public float createRate = 3f;

	public override void OnStartServer () {
		//2秒後開始，之後每createRate秒產生
		InvokeRepeating("CreateEnemy", 2f, createRate);
	}

	void CreateEnemy(){
		//隨機X軸位置產生敵人
		GameObject enemy = (GameObject)Instantiate(enemyPrefab, new Vector3(Random.Range(xMin, xMax), createHeight, 0f), enemyPrefab.transform.rotation);
		NetworkServer.Spawn(enemy);
	}
}
