using UnityEngine;
using System.Collections;

public class CloudGenerator : MonoBehaviour {
	public GameObject[] items;
	public float spawnFrequency = 6;
	private float timeSinceSpawn = 0;
	
	
	void Update () {
		if(Game.inMenu)
			return;
		
		timeSinceSpawn += Time.deltaTime;
		if(timeSinceSpawn > spawnFrequency) { 
			SpawnItem();
			timeSinceSpawn = 0.0f;
		}
	}
	
	void SpawnItem() {
		GameObject itemPrefab = items[Random.Range(0, items.Length)];
		GameObject itemInstance = (GameObject) Instantiate(
			itemPrefab,
			transform.position,
			Quaternion.Euler(0, -90, 0));
		itemInstance.transform.Translate(0, Random.Range(-3.0f, 3.0f), 0);
	}
}
