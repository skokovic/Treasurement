using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SM = GameObject.FindObjectOfType<StateManager> ();
		material = GetComponent<MeshRenderer>().material;
		treasure = (int)Random.Range (500, 1500);
	}

	StateManager SM;
	public Ground[] Adjacent;
	public Material material;
	public int treasure;

	void OnMouseUp(){
		SM.setDestination (this);
	}

	public void Select(){
		material.color = Color.yellow;
	}

	public void Unselect(){
		material.color = Color.white;
	}

	// Update is called once per frame
	void Update () {
	
	}

}
