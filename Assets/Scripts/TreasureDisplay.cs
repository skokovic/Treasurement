using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreasureDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SM = GameObject.FindObjectOfType<StateManager> ();
		myText = GetComponent<Text> ();
	}

	StateManager SM;
	Text myText;


	// Update is called once per frame
	void Update () {
		if (SM.CurrentPlayerID < 0)
			return;
		
		for (int i = 0; i < SM.NumberOfPlayers; i++) {
			if (SM.Players [i].PlayerID == SM.CurrentPlayerID) {
				myText.text = "Treasure: " + SM.Players [i].treasure;
				break;
			}
		}
	}
}
