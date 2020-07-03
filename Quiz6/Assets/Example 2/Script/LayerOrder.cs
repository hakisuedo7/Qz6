using UnityEngine;
using System.Collections;

public class LayerOrder : MonoBehaviour {
	public string layerName = "Default";
	public int  order = 0;
	
	void Start () {
		GetComponent<Renderer>().sortingLayerName = layerName;
		GetComponent<Renderer>().sortingOrder = order;
	}

}
