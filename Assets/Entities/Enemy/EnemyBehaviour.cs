using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150f;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float firingRate = 3f;

	public AudioClip fireSound;
	public AudioClip deathSound;
	public AudioClip deathScream;

	public int scoreValue = 150;

	private ScoreKeeper scoreKeeper;

	private float timeLeft;

	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		timeLeft = Random.Range(0.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update() {
		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0) {
			Fire();
			timeLeft = Random.Range(0.8f, 4.0f);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
			// Debug.Log("Hit by projectile");
		}
	}

	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0, -0.7f, 0);
		GameObject laser = Instantiate(projectile, startPosition, Quaternion.AngleAxis(180, Vector3.left)) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position, 1.0f);
	}

	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position, 1.5f);
		AudioSource.PlayClipAtPoint(deathScream, transform.position, 1.5f);
		scoreKeeper.Score(scoreValue);
		Destroy(gameObject);
	}
}
