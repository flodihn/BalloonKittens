using UnityEngine;
using System.Collections;

public class ScrollSprite : MonoBehaviour {
	public float speed = 1.0f;
	private Player player;
	
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void Update() {
		renderer.material.SetTextureOffset("_MainTex", new Vector2(Time.time * speed * player.speed, 0));
	}
}
