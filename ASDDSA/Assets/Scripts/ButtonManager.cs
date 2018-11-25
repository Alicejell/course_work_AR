using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

	public float[] data;
	public GameObject textMesh;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			GameObject g = new GameObject ("Graph");
			g.AddComponent<GraphUI> ();
			g.GetComponent<GraphUI> ().data = data;
			Instantiate (g, new Vector3 (0, 0, 0), Quaternion.identity);
		}
	}
}
