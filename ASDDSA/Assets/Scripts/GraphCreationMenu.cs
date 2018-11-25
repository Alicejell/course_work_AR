using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphCreationMenu : MonoBehaviour {

    public Transform target;
	public GameObject menu, addButton, textMesh, legend;

	public void CreateGraph() {
		GameObject[] inputs = GameObject.FindGameObjectsWithTag ("InputData");
		GameObject[] labely = GameObject.FindGameObjectsWithTag ("LegendLabel");
        GameObject prevGraph = GameObject.FindGameObjectWithTag("Graph");
        Destroy(prevGraph);
		List<float> vals = new List<float> ();
		foreach (GameObject field in inputs) {
            string text = field.GetComponent<InputField>().text;
            if (!string.IsNullOrEmpty(text))
                vals.Add(float.Parse(text));
		}

		List<string> lbls = new List<string> ();
		foreach (GameObject field in labely) {
			string text = field.GetComponent<InputField>().text;
			if (!string.IsNullOrEmpty(text))
				lbls.Add(text);
		}

		GameObject g = new GameObject ("Graph");
		g.AddComponent<GraphUI> ();
		g.GetComponent<GraphUI> ().data = vals.ToArray();
		g.GetComponent<GraphUI> ().textMesh = textMesh;
		g.GetComponent<GraphUI> ().legendPreset = legend;
		g.GetComponent<GraphUI> ().maxHeight = 5;
		g.GetComponent<GraphUI> ().threshold = 5;
		g.GetComponent<GraphUI> ().labels = lbls.ToArray();
		Instantiate (g, target);
		CancelGraph ();
    }

    public void CancelGraph() {
        menu.SetActive(false);
        addButton.SetActive(true);
    }

    public void OpenGraphMenu() {
        menu.SetActive(true);
        addButton.SetActive(false);
    }
}
