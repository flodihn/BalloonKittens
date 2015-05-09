using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public AudioClip pickupSound;

	public enum ItemType {
		Correct,
		Incorrect
	};
	public ItemType itemType;

	void OnCollisionEnter2D(Collision2D other) {
		if(Game.inMenu)
			return;

		if(itemType == ItemType.Correct) {
			Game.speed += 0.1f;
			Game.score += 1;
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.SendMessage("PlayPickupSound");
		} else {
			Game.GameOver();
		}
		Destroy(gameObject);
	}

	void Update() {
		if(Game.inMenu)
			return;

		transform.Translate(Game.speed * 2 * Time.deltaTime, 0, 0);
		if(transform.position.x >= 10.0f)
			Destroy(gameObject);
	}
}
