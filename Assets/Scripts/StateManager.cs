using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Players = GameObject.FindObjectsOfType<Player> ();
	}

	//TODO: create inital state and to start call next player (npr. CurrentPlayerID = -1)

	public Player[] Players;
	public int NumberOfPlayers = 4;
	public int CurrentPlayerID = 0;
	public int NumberOfTurns = 4;

	public Ground currentGround;
	Ground tmpGround;

	public void NextPlayer(){
		if (tmpGround == null) {
			return;
		}
		setTargetPosition (tmpGround);
		CurrentPlayerID = (CurrentPlayerID + 1) % NumberOfPlayers;

		for (int i = 0; i < NumberOfPlayers; i++) {
			if (Players [i].PlayerID == CurrentPlayerID) {
				currentGround = Players [i].getCurrentGround ();
				//TODO: color adjacent tiles green
				break;
			}
		}

		if (CurrentPlayerID % NumberOfPlayers == 0) {
			EndOfTurn();
		}
			

	}

	public void setDestination(Ground g){
		//TODO: Fix limitations
/*		bool contains = false;
		for (int i = 0; i < currentGround.Adjacent.Length; i++) {
			if (currentGround.Adjacent [i].Equals (g)) {
				contains = true;
				break;
			}
		}
		if (!contains)
			return;
*/		if(tmpGround!=null)
			tmpGround.Unselect ();
		tmpGround = g;
		tmpGround.Select ();
	}

	void setTargetPosition(Ground g){
		for (int i = 0; i < NumberOfPlayers; i++) {
			if (Players [i].PlayerID == CurrentPlayerID) {
				Players [i].SetTargetPosition (g.transform.position);
				Players [i].currentGround = g;
				g.Unselect ();
			}
		}
	}

	public void EndOfTurn(){

		for (int i = 0; i < NumberOfPlayers; i++) {
			Players [i].isAnimating = true;
		}

		//TODO: Battle?

		//TODO: Treasure transfer;
		for (int i = 0; i < NumberOfPlayers; i++) {
			Players [i].treasure += Players [i].getCurrentGround ().treasure;
			Players [i].getCurrentGround ().treasure = 0;
		}

		NumberOfTurns--;
		if (NumberOfTurns == 0) {
			//TODO: END!!!
		}
		return;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
