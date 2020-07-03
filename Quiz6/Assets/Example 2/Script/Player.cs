using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : Combat {

	public Behaviour[] disabledComponents;
	[SyncVar]
	public Color myColor = Color.white;

	void Start () {
		SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer renderer in renderers) {
			renderer.color = myColor;
		}

		if(isLocalPlayer){
			FindObjectOfType<Parallax2D>().target = this.transform;
			ScoreManager.singleton.playerID = GetComponent<NetworkIdentity>().netId.ToString();
		}
		else{
			foreach(Behaviour component in disabledComponents)
				component.enabled = false;
		}
	}
	
	public override void EndOfLife(string playerID){
		health = maxHealth;
		RpcRespawn();
	}

	[ClientRpc]
	void RpcRespawn(){
		if(isLocalPlayer)
			transform.position = new Vector3(0, 1, 0);
	}

	[Command]
	public void CmdFlip(){
		RpcFlip();
	}

	[ClientRpc]
	void RpcFlip(){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
