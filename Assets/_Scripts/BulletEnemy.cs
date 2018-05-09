using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {

	public GameObject Explosion;
	private GameController Controller;

	void Start()
	{
		Controller = (GameController)GameObject.Find ("TheGame").GetComponent("GameController");
		//Controller.
		//	enemyExplosion = (AudioClip) Resources.Load("Sounds/cube_release");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "spaceship")
		{
			Instantiate (Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			//Controller.AddScore();
			Destroy (other.gameObject);
			Destroy (gameObject);//这里可以直接写gameObject,因为默认是这个脚本里的gameObject
			Controller.gameOver = true;
			//enemyExplosion.Play();
			//但是加了一个if，不然会摧毁飞船本身
		}
	}


}
