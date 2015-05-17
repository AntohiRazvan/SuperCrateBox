using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	public GameObject g;

	// Use this for initialization
	void Start () {
		Mesh mesh;
		mesh = g.GetComponent<MeshFilter>().mesh;
		Vector3[] newVertex = meshRescaleAndTranslate(mesh.vertices, Vector3.up, 2);
		mesh.vertices = newVertex;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Vector3[] meshRescaleAndTranslate(Vector3[] vertexList, Vector3 displacement, float scale)
	{
		float max = 0.0f;

		for (int i = 0; i < vertexList.Length; i++)
		{
			if (vertexList[i].magnitude > max)
				max = vertexList[i].magnitude;
		}

		Vector3[] returns = new Vector3[vertexList.Length];

		for (int i = 0; i < vertexList.Length; i++)
		{
			Vector3 vtx = vertexList[i].normalized * vertexList[i].magnitude / max;
			returns[i] = vtx * scale / 2 + displacement;
		}

		return returns;
	}
}
