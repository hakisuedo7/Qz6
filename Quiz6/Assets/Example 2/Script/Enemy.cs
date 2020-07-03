using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Enemy : Combat {
	//風力大小
	public float windForce = 2f;
	//爆炸傷害
	[SerializeField]
	private int damage = 50;
	public GameObject explodePrefab;
	
	bool quitting = false;
	
	void OnApplicationQuit () {
		quitting = true;
	}

	void Start () {
		if(!isServer) return;
		//5秒後自動刪除
		Destroy(this.gameObject, 5f);
		//每隔1秒被風施力
		InvokeRepeating("AddWindForce", 0f, 1f);
	}

	void AddWindForce () {
		//隨機選方向施力
		if(Random.Range(0f, 1f) > 0.5f)
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * windForce);
		else
			GetComponent<Rigidbody2D>().AddForce(-Vector2.right * windForce);
	}

	//觸碰到其他物體就刪除
	void OnCollisionEnter2D(Collision2D hit){
		if(!isServer) return;
		ScoreManager.singleton.RpcChangeScore("All", -score);
		Destroy (this.gameObject);
	}
	
	public override void OnNetworkDestroy(){
		if (quitting) return;
		//被銷毀時撥放爆炸特效
		GameObject effect = Instantiate(explodePrefab, this.transform.position, Quaternion.identity) as GameObject;
		Destroy(effect, 1f);
		//爆炸範圍內的角色受到傷害
		if(!isServer) return;
		Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, 1.5f, 1 << LayerMask.NameToLayer("Player"));
		GameObject previous_damged = null;
		foreach(Collider2D hit in hits){
			if(hit.gameObject != previous_damged){
				hit.GetComponent<Combat>().TakeDamage(damage);
				previous_damged = hit.gameObject;
			}
		}
	}
}
