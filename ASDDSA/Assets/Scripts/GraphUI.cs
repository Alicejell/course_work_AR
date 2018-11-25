using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GraphUI : MonoBehaviour {

	public GameObject textMesh, legendPreset; //Preset for Instantiating shit
	public float[] data;
    public int maxHeight, threshold, legendMargin = 2; //maxHeight - heights of the biggest element, Threshold - value to start scaling vals
    public string[] labels; //labels for legend

    float[] heights; //new heights of the elements if scaled
	float showTime = 2;
	float distance = 2;
	Transform parent;
	bool done;
    Color[] colors = { Color.red, Color.yellow, Color.green, Color.blue, Color.cyan };

	public void CreateGraph() {
		done = false;
        heights = new float[5];
        data.CopyTo(heights, 0);

		float lastCoor = 0;
		parent = new GameObject ("Graph").GetComponent<Transform> ();
        GameObject legend = Instantiate(legendPreset);
        legend.transform.parent = parent;

        GameObject[] labelMeshes = GameObject.FindGameObjectsWithTag("LegendLabel");
        scaleData();
        int lastLegendLabel = data.Length;
        for(int i = lastLegendLabel; i < labelMeshes.Length; i++) {
            labelMeshes[i].GetComponent<TMPro.TextMeshProUGUI>().alpha = 0;
        }

		for (int i = 0; i < data.Length; i++) {
			Transform element = new GameObject ("Element").GetComponent<Transform> ();
            //Create each block
			GameObject g = GameObject.CreatePrimitive (PrimitiveType.Cube);
			g.transform.localScale = new Vector3 (0, 0, 0);
			g.transform.position = new Vector3 (lastCoor, 0);//end is (lastCoor, data[i] / 2)
			g.transform.parent = element;
            g.GetComponent<Renderer>().material.color = colors[i];
            Debug.Log(i + " " + labelMeshes.Length);
            labelMeshes[i].GetComponent<TMPro.TextMeshProUGUI>().text = " ▄ " + labels[i];


            GameObject n = Instantiate (textMesh, element);
			n.transform.position = new Vector3 (-4f, 0f, 0.6f);
			n.transform.rotation = Quaternion.LookRotation (-transform.forward, transform.up);
			TMPro.TextMeshPro mesh = n.GetComponent<TMPro.TextMeshPro> ();
			mesh.text = data [i] + "";
			mesh.fontSize = 8;
			mesh.alignment = TMPro.TextAlignmentOptions.MidlineGeoAligned;

			StartCoroutine (ShowElement (g, mesh, heights [i], lastCoor));
			element.position = new Vector3 (lastCoor, heights [i] / 2);


            lastCoor += distance;
			element.parent = parent;
		}
        legend.transform.position += new Vector3(lastCoor - distance + legendMargin, 0.0f);

        parent.gameObject.tag = "Graph";
		done = true;
	}

    private void scaleData() {
        float max = data.Max();
        float min = data.Min();
        bool shouldNorm = false;
        for (int i = 0; i < data.Length; i++) {
            if(data[i] > threshold) {
                shouldNorm = true;
                break;
            }
        }
        if (shouldNorm) {
            for (int i = 0; i < data.Length; i++) {
                heights[i] = (data[i] / max * maxHeight);
            }
        }
    }

	IEnumerator ShowElement(GameObject g, TMPro.TextMeshPro t, float data, float coor) {
		float diff = 0;
		while (diff < showTime) {
			float sy = data * diff / showTime;
			float py = data / 2 * diff / showTime;
			g.transform.position = new Vector3 (coor, py);
			g.transform.localScale = new Vector3 (1, sy, 1);
			t.color = new Color (0, 0, 0, 255 * diff / showTime);
			diff += Time.deltaTime;
			yield return null;
		}
		g.transform.localScale = new Vector3 (1, data, 1);
		g.transform.position = new Vector3 (coor, data / 2);
	}

	void Start() {
		CreateGraph ();
	}
}
