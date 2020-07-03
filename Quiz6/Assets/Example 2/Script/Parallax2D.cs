using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallax2D : MonoBehaviour {
	public Transform cam;
	public Transform target;
	public Transform[] backgrounds;
	public float smoothing = 1f;
	public float distance = 10f;
	public float upperX;
	public float lowerX;

	private Vector3 previousCamPos;
	
	void Start () {
		if(cam == null) cam = Camera.main.transform;
		previousCamPos = cam.position;
	}

	void Update () {
		if(target == null) return;
		Vector3 cameraPos = new Vector3(Mathf.Clamp(target.position.x, lowerX, upperX), cam.position.y, cam.position.z);
		cam.position = Vector3.Lerp(cam.position, cameraPos, 5f * Time.deltaTime);
	
		for(int i = 0; i < backgrounds.Length; i++){
			float parallax = (cam.position.x - previousCamPos.x) * (i+1) * distance;

			float backgroundTargetX = backgrounds[i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3(backgroundTargetX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		previousCamPos = cam.position;
	}
}
