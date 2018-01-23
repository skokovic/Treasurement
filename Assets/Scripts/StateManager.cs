using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Players = GameObject.FindObjectsOfType<Player> ();
		Tiles = GameObject.FindObjectsOfType<Ground> ();


		StartCanvas = GameObject.FindGameObjectWithTag ("StartScreen");

		GameCanvas = GameObject.FindGameObjectWithTag ("Game");
		GameCanvas.SetActive (false);

		EndGameCanvas = GameObject.FindGameObjectWithTag ("Finish");
		EndGameCanvas.SetActive (false);
	}


	public Player[] Players;
	Ground[] Tiles;
	public GameObject GameCanvas;
	public GameObject StartCanvas;
	public GameObject EndGameCanvas;

	public int NumberOfPlayers;
	public int CurrentPlayerID;
	public int NumberOfTurns;

	public Ground currentGround;
	Ground tmpGround;

	public void startGame ()
	{
		CurrentPlayerID = 0;
		available ();
		StartCanvas.SetActive (false);
		GameCanvas.SetActive (true);
	}

	public void NextPlayer ()
	{
		if (tmpGround.Equals (currentGround)) {
			Text t = GameCanvas.GetComponentInChildren<Text> ();
			t.text = "You must chose a new destinaton.";
			return;
		}
		if (tmpGround == null) {
			return;
		}

		for (int i = 0; i < NumberOfPlayers; i++) {
			if ((Players [i].PlayerID == CurrentPlayerID) && (Players [i].isAnimating)) {
				return;
			}
		}

		setTargetPosition (tmpGround);
		CurrentPlayerID = (CurrentPlayerID + 1) % NumberOfPlayers;



		if (CurrentPlayerID % NumberOfPlayers == 0) {
			EndOfTurn ();
		}

		available ();

	}

	public void available ()
	{
		for (int i = 0; i < NumberOfPlayers; i++) {
			if (Players [i].PlayerID == CurrentPlayerID) {
				currentGround = Players [i].getCurrentGround ();
				tmpGround = currentGround;
				for (int j = 0; j < currentGround.Adjacent.Length; j++) {
					currentGround.Adjacent [j].available = true;
					currentGround.Adjacent [j].Unselect ();
				}
				break;
			}
		}
	}

	public void setDestination (Ground g)
	{
		Text t = GameCanvas.GetComponentInChildren<Text> ();
		t.text = "";

		if (CurrentPlayerID < 0)
			return;

		bool contains = false;
		for (int i = 0; i < currentGround.Adjacent.Length; i++) {
			if (currentGround.Adjacent [i].Equals (g)) {
				contains = true;
				break;
			}
		}
		if (!contains)
			return;
		if (tmpGround != null) {
			tmpGround.Unselect ();
		}
		tmpGround = g;
		tmpGround.Select ();
	}

	void setTargetPosition (Ground g)
	{

		for (int i = 0; i < NumberOfPlayers; i++) {
			if (Players [i].PlayerID == CurrentPlayerID) {

				for (int j = 0; j < currentGround.Adjacent.Length; j++) {
					currentGround.Adjacent [j].available = false;
					currentGround.Adjacent [j].Unselect ();
				}
				Players [i].SetTargetPosition (g.transform.position);
				Players [i].previousGround = Players [i].currentGround;
				Players [i].currentGround = g;
				g.Unselect ();
			}
		}
	}

	public void EndOfTurn ()
	{



		for (int i = 0; i < NumberOfPlayers; i++) {
			Players [i].isAnimating = true;
		}

		BattlePrep ();

		for (int i = 0; i < NumberOfPlayers; i++) {
			Players [i].treasure += Players [i].getCurrentGround ().treasure;
			Players [i].getCurrentGround ().treasure = 0;
			Players [i].getCurrentGround ().looted = true;
			Players [i].getCurrentGround ().Unselect ();
		}

		NumberOfTurns--;
		if (NumberOfTurns == 0) {
			EndGame ();
		}
		return;
	}

	void EndGame ()
	{
		EndGameCanvas.SetActive (true);
		GameCanvas.SetActive (false);

		Text t = EndGameCanvas.GetComponentInChildren<Text> ();

		int ID = -1;
		int max = 0;

		for (int i = 0; i < NumberOfPlayers; i++) {
			if (Players [i].treasure > max) {
				ID = i;
				max = Players [i].treasure;
			}
		}

		t.text = "Winner is player " + (ID + 1) + ".";
		switch (ID) {
		case 0:
			t.color = Color.blue;
			break;
		case 1:
			t.color = Color.red;
			break;
		case 2:
			t.color = Color.green;
			break;
		case 3:
			t.color = Color.yellow;
			break;
		default:
			break;

		}

	}

	public void Reset ()
	{
		StartCanvas.SetActive (true);
		GameCanvas.SetActive (false);
		EndGameCanvas.SetActive (false);

		NumberOfTurns = 5;
		CurrentPlayerID = -1;

		for (int i = 0; i < NumberOfPlayers; i++) {
			Players [i].treasure = 0;
			Players [i].getCurrentGround ().treasure = 0;
			Players [i].getCurrentGround ().looted = true;
			Players [i].getCurrentGround ().Unselect ();
			Players [i].SetTargetPosition (Players [i].startingGround.transform.position);
			Players [i].currentGround = Players [i].startingGround;
			Players [i].isAnimating = true;
		}
		for (int i = 0; i < Tiles.Length; i++) {
			Tiles [i].looted = false;
			Tiles [i].available = false;
			Tiles [i].treasure = (int)Random.Range (500, 1500);
			Tiles [i].Unselect ();
		}
	}

	void BattlePrep ()
	{
		
		ArrayList warriors = new ArrayList ();
		bool battle = false;

		for (int i = 0; i < NumberOfPlayers-1; i++) {
			for (int j = i+1; j < NumberOfPlayers; j++) {
				if (Players [i].currentGround == Players [j].currentGround) {
					if (!warriors.Contains (Players [i])) {
						warriors.Add (Players [i]);
					}
					warriors.Add (Players [j]);
					battle = true;
				}
			}
			if (battle) {
				Battle (warriors);
				warriors.Clear ();
				battle = false;
			}
		}
	}

	void Battle (ArrayList warriors)
	{
		int sum = 0;
		Player[] war = new Player[warriors.Count];

		for (int i = 0; i < warriors.Count; i++) {
			war [i] = (Player)warriors[i];
		}


		for (int i = 0; i < war.Length; i++) {
			war [i].min = sum;
			sum += war [i].getStrength();
			war [i].max = sum;
		}

		int result=Random.Range(0,sum);
		int lootAmount = Random.Range (250, 500);

		for (int i = 0; i < war.Length; i++) {
			if (!(war [i].min < result && war [i].max > result)) {
				war [i].currentGround = war [i].previousGround;
				war [i].treasure -= lootAmount;
			} else {
				war [i].treasure += lootAmount * (war.Length - 1);
			}
			war [i].SetTargetPosition (war [i].currentGround.transform.position);
			war [i].isAnimating = true;
		}

	}

	public void Quit()
	{
		Application.Quit();
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
