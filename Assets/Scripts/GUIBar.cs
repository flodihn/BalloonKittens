using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIBar : MonoBehaviour {
	public Text scoreText;

	void Update() {
		if(scoreText == null)
			return;
			
		scoreText.text = "Score: " + Game.score.ToString();
	}


}
