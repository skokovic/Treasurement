using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrentPlayerDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SM = GameObject.FindObjectOfType<StateManager> ();
		myText = GetComponent<Text> ();
	}

	StateManager SM;
	Text myText;

	string[] numbers = { "Blue", "Red", "Green", "Yellow" };
	
	// Update is called once per frame
	void Update () {
		myText.text = "Current player: " + numbers [SM.CurrentPlayerID];
		switch (SM.CurrentPlayerID) {
		case 0:
			myText.color = Color.blue;
			break;
		case 1:
			myText.color = Color.red;
			break;
		case 2:
			myText.color = Color.green;
			break;
		case 3:
			myText.color = Color.yellow;
			break;
		default:
			break;
		
		}

	}
}
