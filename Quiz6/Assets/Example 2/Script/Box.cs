using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Box : Combat {
	public GameObject explodePrefab;

	bool quitting = false;
	
	void OnApplicationQuit () {
		quitting = true;
	}

	//被銷毀時撥放爆炸特效
	public override void OnNetworkDestroy(){
		if (quitting) return;
		GameObject effect = Instantiate(explodePrefab, this.transform.position, Quaternion.identity) as GameObject;
		Destroy(effect, 1f);
	}
}
