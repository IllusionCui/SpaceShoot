using UnityEngine;
using System.Collections;

[AddComponentMenu("SpaceShoot/Enemy")]
public class Enemy : MonoBehaviour {
	public float m_speed = 1;
	public float m_rotationTimeLimit = 3;
	public float m_rotationSpeed = 30;
	public float m_life = 10;

	protected Transform m_tranform;
	protected float m_rotationTimer = 0;

	// Use this for initialization
	void Start () {
		m_tranform = this.transform;
		m_rotationTimer = Random.value * m_rotationTimeLimit;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMove ();
	}

	void OnTriggerEnter(Collider other) {
//		Debug.Log("Enemy OnTriggerEnter");
		if (other.tag.CompareTo("Rocket") == 0) {
			Rocket rocket = other.GetComponent<Rocket>();
			if (rocket != null) {
				m_life -= rocket.m_power;
				if (m_life <= 0) {
					Destroy(this.gameObject);
				}
			}
		} else if (other.tag.CompareTo("Player") == 0) {
			m_life = 0;
			Destroy(this.gameObject);
		}
	}

	protected void UpdateMove() {
		m_rotationTimer += Time.deltaTime;
		if (m_rotationTimer > m_rotationTimeLimit) {
			m_rotationSpeed = -m_rotationSpeed;
			m_rotationTimer = 0;
		}
		m_tranform.Rotate (Vector3.up, m_rotationSpeed*Time.deltaTime, Space.World);
		m_tranform.Translate (new Vector3(0, 0, -m_speed*Time.deltaTime));
	}
}
