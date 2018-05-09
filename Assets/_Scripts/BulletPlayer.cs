using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPlayer : MonoBehaviour {


	//private AudioSource enemyExplosion;
	// Update is called once per frame

	public GameObject Explosion;
	//public GameController Controller;

	private GameController Controller;

	void Start()
	{
		Controller = (GameController)GameObject.Find ("TheGame").GetComponent("GameController");
		//Controller.
		//	enemyExplosion = (AudioClip) Resources.Load("Sounds/cube_release");
	}


	void Update () 
	{
		gameObject.GetComponent<Transform> ().Rotate(new Vector2 (90f, 0f));
		//小写的game是这个体本身（子弹）
	}
    
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "enemy" || other.tag == "asteroid")
		{
			if (other.tag == "enemy") 
			{
				Controller.AddScore (100);
			} 
			else if (other.tag == "asteroid")
			{
    			Controller.AddScore (200);
			}
			Instantiate (Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			//Controller.AddScore();
			Destroy (other.gameObject);
			Destroy (gameObject);//这里可以直接写gameObject,因为默认是这个脚本里的gameObject
            //enemyExplosion.Play();
			//但是加了一个if，不然会摧毁飞船本身
		}
	}

}
