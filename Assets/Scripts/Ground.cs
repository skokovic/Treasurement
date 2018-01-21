using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		SM = GameObject.FindObjectOfType<StateManager> ();
		material = GetComponent<MeshRenderer> ().material;
		treasure = (int)Random.Range (500, 1500);
	}

	StateManager SM;
	public Ground[] Adjacent;
	public Material material;
	public int treasure;
	public bool available = false;
	public bool looted = false;

	void OnMouseUp ()
	{
		if (SM.CurrentPlayerID < 0) {
			return;
		}
		SM.setDestination (this);
	}

	public void Select ()
	{
		material.color = Color.yellow;
	}

	public void Unselect ()
	{
		if (available && looted) {
			material.color = Color.blue;
		} else if (available) {
			material.color = Color.green;
		} else if (looted) {
			material.color = Color.grey;
		} else {
			material.color = Color.white;
		}
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

}
