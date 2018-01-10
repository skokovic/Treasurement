using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoTurnsDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SM = GameObject.FindObjectOfType<StateManager> ();
		myText = GetComponent<Text> ();
	}

	StateManager SM;
	Text myText;

	
	// Update is called once per frame
	void Update () {
		myText.text = "Number of turns left: " + SM.NumberOfTurns;
	}
}
