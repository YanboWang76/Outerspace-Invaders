using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour {

	private GameController Controller;
	// Use this for initialization
	void Start () 
	{
		Controller = (GameController)GameObject.Find ("TheGame").GetComponent("GameController");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//gameObject.transform.Rotate = 
		gameObject.transform.Rotate (new Vector3 (0f,0f, 2f));
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		//Debug.Log("Enemy collided with: " + other.tag);
		if (other.tag.Equals("boundary"))
		{
			Destroy(gameObject);
			Controller.MinusBaseArmor (5);
		}
	}
}
