using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	float delay=0f;
	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, this.GetComponentInChildren <Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
