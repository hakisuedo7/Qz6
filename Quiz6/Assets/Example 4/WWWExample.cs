using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WWWExample : MonoBehaviour {
	public RawImage image;
	public InputField url;

	public void GetImageWrapper(){
		StartCoroutine(GetImage());
	}

	public IEnumerator GetImage() {
		WWW www = new WWW(url.text);
		yield return www;
		if(!string.IsNullOrEmpty(www.error)) {
			Debug.Log( "Error downloading: " + www.error );
		} else {
			Texture tex = www.texture;
			image.texture = tex;
		}
	}
}
