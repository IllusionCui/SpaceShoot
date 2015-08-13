using UnityEngine;
using System.Collections;

[AddComponentMenu("SpaceShoot/Rocket")]
public class Rocket : MonoBehaviour {
	public float m_speed = 10;
	public float m_liveTime = 1;
	public float m_power = 1;

	protected Transform m_transform;

	// Use this for initialization
	void Start () {
		m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		m_liveTime -= Time.deltaTime;
		if (m_liveTime < 0) {
			Debug.Log("Rocket destory by time");
			Destroy(this.gameObject);
		}

		m_transform.Translate (new Vector3(0, 0, -m_speed*Time.deltaTime));
	}

	void OnTriggerEnter(Collider other) {
//		Debug.Log("Rocket OnTriggerEnter");
		if (other.tag.CompareTo("Player") != 0) {
			Debug.Log("Rocket destory by collide");
			Destroy(this.gameObject);
		}
	}
}
