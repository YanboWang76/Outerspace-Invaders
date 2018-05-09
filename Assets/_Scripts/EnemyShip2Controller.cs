using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShip2Controller: MonoBehaviour {

	public Transform enemyBulletSpawn;
	public GameObject enemyBulletPrefab;

	private GameController Controller;

	private float timeElapsed;
	// Use this for initialization
	void Start () 
	{
		Controller = (GameController)GameObject.Find ("TheGame").GetComponent("GameController");
	}

	public void EnemyFire()
	{
		var bullet = (GameObject)Instantiate(enemyBulletPrefab, enemyBulletSpawn.position, enemyBulletSpawn.rotation);
		//GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
	
		Vector2 bulletMotion = new Vector2 (-10f, 0f);
		bullet.GetComponent<Rigidbody2D> ().AddForce (bulletMotion * 40);
		Destroy (bullet, 4f);//Long enough to let it fly through the scence without being destroyed
		//gameObject.GetComponent<AudioSource> ().Play ();
	}
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed % 5 < 0.05) 
		{
			EnemyFire ();
		}
	}
	private void OnTriggerEnter2D (Collider2D other)
	{
		//Debug.Log("Enemy collided with: " + other.tag);
		if (other.tag.Equals("boundary"))
		{
			Destroy(gameObject);
			Controller.MinusBaseArmor (10);
		}
	}
}
