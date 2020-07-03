using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour {
	[SerializeField]
	private int damage = 50;
	public Transform bulletTrailPrefab;
	public Transform firePoint;
	public LayerMask whatToHit;
	
	// Update is called once per frame
	void Update () {
		//按下Fire鍵 發射子彈
		if (Input.GetButtonDown ("Fire1")) {
			Shoot();
		}
	}
	
	void Shoot () {
		//子彈發射角度
		float rotZ = firePoint.rotation.eulerAngles.z;
		//如果Player X scale為-1(向左) 角度需做修正
		if(transform.localScale.x == -1)
			rotZ = 180-rotZ;
		CmdCreateBullet(rotZ);

		//發射點座標
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		//滑鼠所在的世界座標
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

		//用發射點到滑鼠位置的向量做Raycast
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);
		//如果命中物體且是敵人則刪除他
		if (hit.collider != null) {
			switch(hit.collider.tag){
			case "Enemy" :
			case "Destroyable" :
				CmdDamageEnemy(ScoreManager.singleton.playerID,  hit.collider.GetComponent<NetworkIdentity>().netId);
				break;
			default :
				break;
			}
		}
	}

	[Command]
	void CmdDamageEnemy(string playerID, NetworkInstanceId objId){
		Combat obj = NetworkServer.FindLocalObject (objId).GetComponent<Combat>();
		if(obj != null) obj.TakeDamage(damage, playerID);
	}

	[Command]
	void CmdCreateBullet(float rotZ){
		RpcCreateBullet(rotZ);
	}

	[ClientRpc]
	void RpcCreateBullet(float rotZ){
		//產生子彈軌跡
		Instantiate (bulletTrailPrefab, firePoint.position, Quaternion.Euler (0f, 0f, rotZ));
	}
}
