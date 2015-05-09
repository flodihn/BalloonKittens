using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	GameObject player;
	Vector3 origPos;
	
	void Start () {
		origPos = transform.position;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
		transform.position = new Vector3(origPos.x, origPos.y, player.transform.position.z);
	}
}
