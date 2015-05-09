using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
	public Text finalScore;
	public Text startText;
	public bool restartMode = false;
	public Player player;
	
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.menu = gameObject;
	}

	void OnGUI () {
		if (Input.GetMouseButtonDown(0)) {
			audio.Play();
			StartCoroutine(WaitAndStartGame());
		}
	}

	IEnumerator WaitAndStartGame() {
		yield return new WaitForSeconds(0.5f);
		if(restartMode) {
			ResetGame();
		}
		Game.inMenu = false;
		gameObject.SetActive(false);
		player.StartGame();
	}

	void ResetGame() {
		Game.score = 0;
		Game.speed = 1;
		Game.inMenu = false;
		player.Reset();
		player.StartGame();
		
		GameObject[] items1 = GameObject.FindGameObjectsWithTag("Block");
		foreach(GameObject item in items1) {
			Destroy(item);
		}
		
		GameObject[] items2 = GameObject.FindGameObjectsWithTag("Balloon");
		foreach(GameObject item in items2) {
			Destroy(item);
		}
		
		GameObject[] items3 = GameObject.FindGameObjectsWithTag("Kitten");
		foreach(GameObject item in items3) {
			Destroy(item);
		}
	}
}
