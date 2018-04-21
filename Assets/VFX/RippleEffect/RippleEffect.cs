using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffect : MonoBehaviour {
	class Ripple {
        Vector2 position = new Vector2 (0.5f, 0.5f);
        float time = -1f;
        float lifetime = 10f;

        public void Update () {
            if (time < -0.1f) {
                return;
            }
            if (time > lifetime) {
                time = -1f;
                return;
            }
            time += Time.deltaTime;
        }

        public void Reset (Vector2 pos) {
            position = pos;
            time = 0f;
        }

        public Vector4 MakeShaderParameter () {
            return new Vector4(position.x, position.y, time, 0);
        }
    }

    Material m_material;
	Ripple[] m_ripples;
    int m_index;
	RaycastHit m_hit = new RaycastHit();
	
	void Awake() {
		m_material = this.GetComponent<MeshRenderer>().sharedMaterial;
        m_ripples = new Ripple[] {
            new Ripple (),
            new Ripple (),
            new Ripple (),
        };
        UpdateShaderParameters();
	}

	void UpdateShaderParameters() {
        m_material.SetVector ("_Ripple1", m_ripples[0].MakeShaderParameter ());
        m_material.SetVector ("_Ripple2", m_ripples[1].MakeShaderParameter ());
        m_material.SetVector ("_Ripple3", m_ripples[2].MakeShaderParameter ());
    }

	void Update () {
		m_ripples[0].Update ();
		m_ripples[1].Update ();
		m_ripples[2].Update ();
        UpdateShaderParameters ();
	}

	public void OnClickDown (Vector3 pos) {
		var ray = Camera.main.ScreenPointToRay(pos);
		if (Physics.Raycast (ray, out m_hit)) {
			if (m_hit.collider.gameObject == this.gameObject) {
                Vector3 localPos = m_hit.point - transform.position;
				m_ripples[m_index].Reset (new Vector2(localPos.x, localPos.z));
                m_index = (m_index + 1) % m_ripples.Length;
			}
		}
	}
}
