using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SM = GameObject.FindObjectOfType<StateManager> ();
		targetPosition = this.transform.position;
	}

	StateManager SM;
	public int PlayerID;
	public Ground currentGround;
	Ground selectedGround;
	Vector3 targetPosition;
	Vector3 velocity;

	public int treasure;

	public bool isAnimating = false;

	float smoothTime= 0.5f;
	float smoothTimeVertical = 0.2f;
	float smoothDistance = 0.01f;
	float smoothHeight = 0.5f;

	public Ground getCurrentGround(){
		return currentGround;
	}

	// Update is called once per frame
	void Update () {
		if (!isAnimating) {
			return;
		}

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
	}

	public void SetTargetPosition(Vector3 pos){
		targetPosition = pos;
		targetPosition = targetPosition + Vector3.up;
		velocity = Vector3.zero;
	}
		
}
