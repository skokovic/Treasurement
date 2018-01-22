using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		SM = GameObject.FindObjectOfType<StateManager> ();
		targetPosition = this.transform.position;
		buffs.Add (1);
	}

	StateManager SM;
	public int PlayerID;
	public Ground currentGround;
	public Ground previousGround;
	Ground selectedGround;
	Vector3 targetPosition;
	Vector3 velocity;

	int strength = 100;
	public int min = 0;
	public int max = 100;

	ArrayList buffs = new ArrayList ();

	public int getStrength ()
	{
		int s = strength;

		foreach (var i in buffs) {
			s *= (int)i;
		}

		return s;
	}


	public Ground startingGround;

	public int treasure;

	public bool isAnimating = false;

	float smoothTime = 0.5f;
	float smoothTimeVertical = 0.2f;
	float smoothDistance = 0.01f;
	float smoothHeight = 0.5f;


	float smoothAngle = 10;
	float targetAngle = 0;
	float currentAngle = 0;
	float deltaAngle = 0;


	public Ground getCurrentGround ()
	{
		return currentGround;
	}

	// Update is called once per frame
	//TODO: Rotacije
	void Update ()
	{
		if (!isAnimating) {
			return;
		}
//		if (Math.Abs ((this.transform.rotation.y - targetAngle)) < smoothAngle) {
			if (Vector3.Distance (new Vector3 (this.transform.position.x, targetPosition.y, this.transform.position.z), targetPosition) < smoothDistance) {
				if ((this.transform.position.y - smoothDistance) > targetPosition.y) {
					this.transform.position = Vector3.SmoothDamp (this.transform.position, new Vector3 (this.transform.position.x, targetPosition.y, this.transform.position.z), ref velocity, smoothTimeVertical);

				} else {
					this.isAnimating = false;
				}
			} else if (this.transform.position.y < (smoothHeight - smoothDistance)) {
				this.transform.position = Vector3.SmoothDamp (this.transform.position, new Vector3 (this.targetPosition.x, smoothHeight, this.transform.position.z), ref velocity, smoothTimeVertical);
			
			} else {
				this.transform.position = Vector3.SmoothDamp (this.transform.position, new Vector3 (targetPosition.x, targetPosition.y, targetPosition.z), ref velocity, smoothTime);
			}
//		} else {
//			this.transform.Rotate (0, deltaAngle, 0, Space.Self);
//		}

	}

	public void SetTargetPosition (Vector3 pos)
	{
		targetPosition = pos;
		targetPosition = targetPosition + Vector3.up;
		velocity = Vector3.zero;

//		currentAngle = currentGround.transform.rotation.y;
//
//		if (Math.Abs (pos.x - currentGround.transform.position.x) < 0.1) {
//			if ((pos.z - currentGround.transform.position.z) > 0) {
//				targetAngle = 0;
//			} else {
//				targetAngle = 180;
//			}
//		} else {
//			if ((pos.x - currentGround.transform.position.x) > 0) {
//				targetAngle = 90;
//			} else {
//				targetAngle = -90;
//			}
//		}
//
//		if (currentAngle > targetAngle) {
//			deltaAngle = -1;
//		} else {
//			deltaAngle = 1;
//		}



	}
		
}
