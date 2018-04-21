using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Renderer))]
public class SurfaceWave : MonoBehaviour {

	Renderer m_targetRenderer;  
	CommandBuffer m_commandBuffer;
	RenderTexture[] m_renderTextures = new RenderTexture[3];
	RaycastHit m_hit = new RaycastHit();

	public Material surfaceMaterial;
	public Material equationMaterial;
	public int textureWidth = 256;
	public int textureHeight = 256;

	int m_textureIdx = 0;
	RenderTexture m_currentFrame {
		get {
			return m_renderTextures[m_textureIdx];
		}
	}
	RenderTexture m_prevFrame {
		get {
			return m_renderTextures[(m_textureIdx + 1) % 3];
		}
	}
	RenderTexture m_prevPrevFrame {
		get {
			return m_renderTextures[(m_textureIdx + 2) % 3];
		}
	}

	void OnEnable () {
		m_targetRenderer = this.GetComponentInChildren<Renderer>();  
		m_commandBuffer = new CommandBuffer();
		for (int i = 0 ; i < m_renderTextures.Length; i++) {
			m_renderTextures[i] = RenderTexture.GetTemporary(textureWidth, textureHeight);
		}

		// TODO
		// commandBuffer.Blit(renderTexture, renderTexture, equationMaterial);  

		Camera.main.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_commandBuffer);  
	}

	void Update () {
		m_textureIdx = (m_textureIdx + 1) % m_renderTextures.Length;
	}

	void OnDisable () {
		Camera.main.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, m_commandBuffer);  
        m_commandBuffer.Clear();
		for (int i = 0 ; i < m_renderTextures.Length; i++) {
			RenderTexture.ReleaseTemporary (m_renderTextures[i]);
		}
	}

	public void OnClickDown (Vector3 pos) {
		var ray = Camera.main.ScreenPointToRay(pos);
		if (Physics.Raycast (ray, out m_hit)) {
			if (m_hit.collider.gameObject == this.gameObject) {
				// TODO
				Debug.Log (m_hit.point);
			}
		}
	}
}
