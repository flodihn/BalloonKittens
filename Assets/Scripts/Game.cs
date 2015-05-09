using UnityEngine;
using System.Collections;

public static class Game {
	public static int score = 0;
	public static float speed = 1.0f;
	public static bool inMenu = true;
	

	public static void GameOver() {
		speed = 0;
	}
}
