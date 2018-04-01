using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour {

	void Awake () {
		Generate();
	}

	void OnValidate () {
		Generate();
	}

	public int xSize = 1;
	public int ySize = 1;
	public Color verticeColor = Color.white;

	Mesh m_mesh;

	public void Generate () {
		if (xSize <= 0 || ySize == 0) {
			throw new System.InvalidOperationException ("xSize and ySize must be positive int");
		}

		this.GetComponent<MeshFilter>().mesh = m_mesh = new Mesh();
		m_mesh.name = "Procedural Grid";
		var meshCollider = this.GetComponent<MeshCollider> ();
		if (meshCollider != null) {
			meshCollider.sharedMesh = m_mesh;
		}

		int verticeCount = (xSize + 1) * (ySize + 1);
		Vector3[] vertices = new Vector3[verticeCount];
		Vector2[] uv =  new Vector2[verticeCount];
		Color[] colors =  new Color[verticeCount];
		int[] triangles = new int[xSize * ySize * 6];

		for (int vIdx = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, vIdx++) {
				vertices[vIdx] = new Vector3((float)x / xSize, 0, (float)y / ySize);
				uv[vIdx] = new Vector2 ((float)x / xSize, (float)y / ySize);
				colors[vIdx]  = verticeColor;
			}
		}

		for (int vIdx = 0, tIdx = 0, y = 0; y < ySize; y++, vIdx++) {
			for (int x = 0; x < xSize; x++, vIdx++, tIdx += 6) {
				triangles[tIdx] = vIdx;
				triangles[tIdx + 1] = triangles[tIdx + 4] = vIdx + xSize + 1;
				triangles[tIdx + 2] = triangles[tIdx + 3] = vIdx + 1;
				triangles[tIdx + 5] = vIdx + xSize + 2;
			}
		} 

		m_mesh.vertices = vertices;
		m_mesh.uv = uv;
		m_mesh.triangles = triangles;
		m_mesh.colors = colors;
		m_mesh.RecalculateNormals();
	}
}
