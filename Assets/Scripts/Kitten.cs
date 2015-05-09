using UnityEngine;
using System.Collections;

public class Kitten : MonoBehaviour {
	public GameObject bloodSpatter;
	
	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Ground") {
			GameObject blood = (GameObject) Instantiate(
				bloodSpatter,
				transform.position,
				Quaternion.Euler(0, 270, 0));
			Destroy(blood, .8f);
			Destroy(gameObject);
		}
	}
}
