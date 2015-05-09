using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject head;
	public GameObject body;
	public GameObject bloodSpatter;
	
	public GameObject cloudGenerator;
	public GameObject balloonGenerator;
	public GameObject blockGenerator;

	public GameObject menu;
	public AudioClip dieAudioClip;
	public AudioClip pickupAudioClip;

	public float speed;
	public float maxY;
	public float minY;

	public bool isDead = false;
	
	private bool hasCollidedWithGround = false;
	private Vector3 startPos;
	private TrailRenderer trailRenderer;
	private float origSpeed;
	
	void Start() {
		origSpeed = speed;
		startPos = transform.position;
		SwipeController.Start();
		trailRenderer = GetComponentInChildren<TrailRenderer>();
	}
	
	public void StartGame() {
		trailRenderer.enabled = true;
	}

	void Update () {
		if(isDead)
			return;
		if(Game.inMenu)
			return;
			
		SwipeController.Update();
		MoveUpdate();
	}

	void DieUpdate() {
		if(transform.position.y > -10)
			transform.Translate(-Time.deltaTime * 2.5f, -Time.deltaTime * 5.0f, 0);
	}

	void MoveUpdate() {
		transform.position += new Vector3(0, 0, -Time.deltaTime * speed);		
		if(transform.position.y > maxY)
			transform.position = new Vector3(
				transform.position.x, maxY, transform.position.z);
		if(transform.position.y < minY)
			transform.position = new Vector3(
				transform.position.x, minY, transform.position.z);
	}

	public void Die() {
		cloudGenerator.SetActive(false);
		balloonGenerator.SetActive(false);
		blockGenerator.SetActive(false);
		
		speed = 0;
		audio.clip = dieAudioClip;
		audio.Play();
		transform.localScale = new Vector3(1, -1, 1);
		isDead = true;
		trailRenderer.enabled = false;
		rigidbody.useGravity = true;
	}

	public void Reset() {
		cloudGenerator.SetActive(true);
		balloonGenerator.SetActive(true);
		blockGenerator.SetActive(true);
		
		isDead = false;
		transform.localScale = new Vector3(1, 1, 1);
		transform.position = startPos;
		trailRenderer.enabled = true;
		speed = origSpeed;
		hasCollidedWithGround = false;
		rigidbody.useGravity = false;
		
		ResetBodyPart(head);
		ResetBodyPart(body);
	}

	public void PlayPickupSound() {
		audio.clip = pickupAudioClip;
		audio.Play();
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Block") {
			col.gameObject.collider.enabled = false;
			Die();
		} else if(col.gameObject.tag == "Balloon") {
			col.gameObject.GetComponent<Balloon>().Explode();
		} else if(col.gameObject.tag == "Kitten") {
			Destroy(col.gameObject);
			Game.score += 1;
			PlayPickupSound();
			speed += 0.1f;
		} else if(col.gameObject.tag == "Ground") {
			if(!hasCollidedWithGround) {
				hasCollidedWithGround = true;
				ReleaseBodyPart(head);
				ReleaseBodyPart(body);
				
				StartCoroutine(WaitAndShowMenu());
			}
			GameObject blood = (GameObject) Instantiate(
				bloodSpatter,
				transform.position,
				Quaternion.Euler(0, 270, 0));
			Destroy(blood, .8f);
		}
	}
	
	void ReleaseBodyPart(GameObject bodyPart) {
		bodyPart.GetComponent<Animator>().enabled = false;
		bodyPart.transform.SetParent(null, true);
		
		bodyPart.AddComponent("Rigidbody");
		bodyPart.rigidbody.constraints = RigidbodyConstraints.FreezePositionX |
			RigidbodyConstraints.FreezeRotationZ | 
				RigidbodyConstraints.FreezeRotationY;
		bodyPart.collider.enabled = true;
		bodyPart.rigidbody.AddTorque(new Vector3(0, 0, 100.0f));
		bodyPart.rigidbody.AddForce(new Vector3(0, 100.0f, 0));
	}
	
	void ResetBodyPart(GameObject bodyPart) {
		bodyPart.transform.SetParent(transform, false);
		bodyPart.transform.localPosition = Vector3.zero;
		bodyPart.transform.localRotation = Quaternion.identity;
		bodyPart.collider.enabled = false;
		Destroy(bodyPart.rigidbody);
		bodyPart.GetComponent<Animator>().enabled = true;
		bodyPart.transform.localScale = new Vector3(1, 1, 1);
	}
	
	IEnumerator WaitAndShowMenu() {
		yield return new WaitForSeconds(5.0f);
		menu.SetActive(true);
		Game.inMenu = true;
		Menu menuScript = menu.GetComponent<Menu>();
		menuScript.finalScore.text = "Final Score " + Game.score.ToString();
		menuScript.startText.text = "Tap to play again";
		menuScript.restartMode = true;
	}
}
