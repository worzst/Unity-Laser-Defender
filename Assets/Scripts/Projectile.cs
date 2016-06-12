using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float damage = 100f;
	public AudioClip hitSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetDamage() {
		return damage;
	}

	public void Hit() {
		AudioSource.PlayClipAtPoint(hitSound, transform.position, 1.0f);
		Destroy(gameObject);
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}
}
