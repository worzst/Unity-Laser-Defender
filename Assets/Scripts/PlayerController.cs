using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float firingRate = 0.4f;

	public float health = 250f;

	public float movSpeed = 15.0f;
	public float padding = 1.0f;

	public AudioClip fireSound;
	public AudioClip deathSound;
	public AudioClip deathScream;

	float xmin;
	float xmax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
		//print("leftmost " + leftMost);
		//print("rightmost " + rightMost);
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			//transform.position += new Vector3(-movSpeed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * movSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			//transform.position += new Vector3(movSpeed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * movSpeed * Time.deltaTime;
		}

		// restrict player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector2(newX, transform.position.y);

		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	}

	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0, 0.7f, 0);
		GameObject laser = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
//		AudioSource audio = GetComponent<AudioSource>();
//		if (audio) {
//			audio.Play();
//		}
		AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.5f);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
			//Debug.Log("Hit by projectile");
		}
	}

	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position, 1.5f);
		AudioSource.PlayClipAtPoint(deathScream, transform.position, 1.5f);
		Invoke("Lost", 1.0f);
	}

	void Lost() {
		LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		manager.LoadLevel("Win");
		Destroy(gameObject);
	}
}
