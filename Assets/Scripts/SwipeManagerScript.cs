using UnityEngine;
using System.Collections;

public class SwipeManagerScript : MonoBehaviour {

	private UIManagerScript ui;
	private readonly Vector2 mXAxis = new Vector2(1, 0);
	private readonly Vector2 mYAxis = new Vector2(0, 1);
	
	// The angle range for detecting swipe
	private const float mAngleRange = 40;
	
	// To recognize as swipe user should at lease swipe for this many pixels
	private const float mMinSwipeDist = 40.0f;
	
	// To recognize as a swipe the velocity of the swipe
	// should be at least mMinVelocity
	// Reduce or increase to control the swipe speed
	private const float mMinVelocity  = 800.0f;
	
	private Vector2 mStartPosition;
	private float mSwipeStartTime;
	
	// Use this for initialization
	void Start () {
		ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		// Mouse button down, possible chance for a swipe
		if (Input.GetMouseButtonDown(0)) {
			// Record start time and position
			mStartPosition = new Vector2(Input.mousePosition.x,
			                             Input.mousePosition.y);
			mSwipeStartTime = Time.time;
		}
		
		// Mouse button up, possible chance for a swipe
		if (Input.GetMouseButtonUp(0)) {
			float deltaTime = Time.time - mSwipeStartTime;
			
			Vector2 endPosition  = new Vector2(Input.mousePosition.x,
			                                   Input.mousePosition.y);
			Vector2 swipeVector = endPosition - mStartPosition;
			
			float velocity = swipeVector.magnitude/deltaTime;
			
			if (velocity > mMinVelocity &&
			    swipeVector.magnitude > mMinSwipeDist) {
				// if the swipe has enough velocity and enough distance
				
				swipeVector.Normalize();
				
				float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
				angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
				
				// Detect left and right swipe
				if (angleOfSwipe < mAngleRange) {
					OnSwipeRight();
				} else if ((180.0f - angleOfSwipe) < mAngleRange) {
					OnSwipeLeft();
				} else {
					// Detect top and bottom swipe
					angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
					angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
					if (angleOfSwipe < mAngleRange) {
						OnSwipeTop();
					} else if ((180.0f - angleOfSwipe) < mAngleRange) {
						OnSwipeBottom();
					} else {

					}
				}
			}
		}
	}
	
	private void OnSwipeLeft() {
		ui.LeftPressed ();
	}
	
	private void OnSwipeRight() {
		ui.RightPressed();
	}
	
	private void OnSwipeTop() {
		ui.InPlacePressed ();
	}
	
	private void OnSwipeBottom() {
		ui.MissPressed ();
	}
}