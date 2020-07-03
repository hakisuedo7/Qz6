using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankWeapon : NetworkBehaviour
{
    [SerializeField]
    public Transform bulletTrailPrefab;
    public Transform firePoint;
    public LayerMask whatToHit;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

	void Shoot()
	{
		//子彈發射角度
		float rotZ = firePoint.rotation.eulerAngles.z;
		/*
		//如果Player X scale為-1(向左) 角度需做修正
		if (transform.localScale.x == -1)
			rotZ = 180 - rotZ;*/
		CmdCreateBullet(rotZ);

		//發射點座標
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
		//滑鼠所在的世界座標
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

		//用發射點到滑鼠位置的向量做Raycast
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

		/*
		if (hit.collider != null)
		{
			switch (hit.collider.tag)
			{
				case "Player":
				case "Destroyable":
					CmdDamageEnemy(ScoreManager.singleton.playerID, hit.collider.GetComponent<NetworkIdentity>().netId);
					break;
				default:
					break;
			}
		}*/
	}

	[Command]
	void CmdDamageEnemy(string playerID, NetworkInstanceId objId)
	{
		Combat obj = NetworkServer.FindLocalObject(objId).GetComponent<Combat>();
//		if (obj != null) obj.TakeDamage(damage, playerID);
	}

	[Command]
	void CmdCreateBullet(float rotZ)
	{
		RpcCreateBullet(rotZ);
	}

	[ClientRpc]
	void RpcCreateBullet(float rotZ)
	{
		Instantiate(bulletTrailPrefab, firePoint.position, Quaternion.Euler(0f, 0f, rotZ));
	}
}
