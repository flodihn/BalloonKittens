using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {
	public AudioClip[] kittenSounds;
	public GameObject kitten;
	Animator animator;
	
	void Start() {
		animator = GetComponent<Animator>();
		Destroy(gameObject, 10.0f);
	}

	void OnMouseDown()
    {
    	Explode();
    }
    
    public void Explode() {
    	collider.enabled = false;
    	kitten.transform.parent = null;
    	kitten.rigidbody.useGravity = true;
    	kitten.collider.enabled = true;
		kitten.audio.clip = kittenSounds[Random.Range(0, kittenSounds.Length)];
    	kitten.audio.Play();
        animator.SetBool("Explode", true);
        Destroy(gameObject, 0.75f);
	}
}