using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {
	[SerializeField]
	private int moveSpeed = 150;

	void Start(){
		//1秒後刪除
		Destroy (gameObject, 1);
	}

	void Update () {
		//往右移動
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
	}
}
