using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SwipeController {
	public static bool swipingEnabled = true;
	private static float timeSinceLastSwipe = 0.0f;
	private static GameObject player;
	
	public static void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
		
	public static void Update() {
		if(!swipingEnabled)
			return;
		
		IncrementTimeSinceLastSwipe();
		
		if(!IsSwipeInProgress())
			return;	
		
		Vector2 deltaMove = GetSwipeDeltaMovement();
		
		if(DetectHorizontalSwipe(deltaMove))
			HandleHorizontalSwipe(deltaMove);
		else if(DetectVerticalSwipe(deltaMove))
			HandleVerticalSwipe(deltaMove);
	}
	
	private static void IncrementTimeSinceLastSwipe() {
		timeSinceLastSwipe += Time.deltaTime;
	}
	
	private static bool IsSwipeInProgress() {
		if(timeSinceLastSwipe < 0.1f)
			return false;
		
		if(GetNumberOfTouches() != 1) 
			return false;
		if(Input.touches[0].phase != TouchPhase.Moved)
			return false;
		return true;
	}
	
	private static Vector2 GetSwipeDeltaMovement() {
		Vector2 deltaMove;
		deltaMove = Input.touches[0].deltaPosition / Input.touches[0].deltaTime;
		return deltaMove;
	}
	
	private static bool DetectHorizontalSwipe(Vector2 deltaMove) {
		if(Mathf.Abs(deltaMove.x) > 500.0f)
			return true;
		return false;
	}
	
	private static bool DetectVerticalSwipe(Vector2 deltaMove) {
		if(Mathf.Abs(deltaMove.y) > 500.0f)
			return true;
		return false;
	}
	
	public static void HandleHorizontalSwipe(Vector2 deltaMove) {
		/*
		if(deltaMove.x < 0)
			player.rigidbody.AddForce(new Vector3(0
		else
			CallSwipeRightInObservers();
		*/
		timeSinceLastSwipe = 0.0f;
	}
	
	public static void HandleVerticalSwipe(Vector2 deltaMove) {
		player.rigidbody.AddForce(new Vector3(0, deltaMove.y * 2, 0));
		timeSinceLastSwipe = 0.0f;
	}
	
	private static int GetNumberOfTouches() {
		return Input.touches.Length;
	}
}
