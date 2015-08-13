using UnityEngine;
using System.Collections;

[AddComponentMenu("SpaceShoot/Player")]
public class Player : MonoBehaviour {
	public float m_speed = 1;
	public Transform m_rocket;
	public float m_rocketRateLimit = 1;
	public float m_life = 3;
	public AudioClip m_shootAudioClip;
	public Transform m_explosionFX;

	protected AudioSource m_audio;
	protected Transform m_transform;
	protected float m_rocketRate = 0;


	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		m_rocketRate = m_rocketRateLimit;
		m_audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		// move
		float moveV = 0;
		float moveH = 0;
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			moveV -= m_speed*Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			moveV += m_speed*Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			moveH += m_speed*Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			moveH -= m_speed*Time.deltaTime;
		}
		m_transform.Translate (new Vector3(moveH, 0 , moveV));
		// shoot
		if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
			if (m_rocketRate < m_rocketRateLimit) {
				m_rocketRate += Time.deltaTime;
			} else {
				m_audio.PlayOneShot(m_shootAudioClip);
				Instantiate(m_rocket, m_transform.position, m_transform.rotation);
				m_rocketRate = 0;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
//		Debug.Log("Player OnTriggerEnter");
		if (other.tag.CompareTo("Rocket") != 0) {
			m_life--;
			if (m_life <= 0) {
				Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}
		}
	}
}
