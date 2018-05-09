using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
	public static GameController instance;

	//public ShipController control;
    public GameObject enemyCraftPrefab;
	public GameObject enemyCraftPrefab2;
	public GameObject asteroidPrefab;
	public AudioSource baseUnderattack;
	private float speed;
	private float timeElapsed;

	public Text txtScore;
	public Text gameOverNotice;
	public Text txtBaseArmor;
	public int score;
	public int baseArmor;

	public bool gameOver;

	private void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		} 
		else if (instance != this)
		{
			Destroy (gameObject);
		}
		gameOver = false;
	}

	// Use this for initialization

	void Start ()
    {
		speed = Random.Range (20f, 50f) * -1;
        speed = -100;
		timeElapsed = 0;
		score = 0;
		txtScore.text = "Score: " + score;
		baseArmor = 100;
		txtBaseArmor.text = "Base Armor: " + baseArmor;
	}

	public void AddScore(int delta)
	{
		score += delta;
		txtScore.text = "Score: " + score;
	}

	public void MinusBaseArmor(int delta)
	{
		if (!gameOver)
		{
			baseArmor = baseArmor - delta;
			txtBaseArmor.text = "Base Armor: " + baseArmor;
			baseUnderattack.Play ();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		
		timeElapsed += Time.deltaTime;//需要搞懂
		if (timeElapsed % 5 < 0.05) 
		{
			Vector3 spawnOffset = new Vector3 (0f, Random.Range (-4.5f, 4.5f), 0f);
			var enemy2 = (GameObject)Instantiate (enemyCraftPrefab2, transform.position+spawnOffset, transform.rotation);
			enemy2.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (speed, 0f));
		}

        if (timeElapsed % 8 < 0.05)
        {
            Vector3 spawnOffset = new Vector3(0f, Random.Range(-4.5f, 4.5f), 0f);
            var enemy = (GameObject)Instantiate(enemyCraftPrefab, transform.position + spawnOffset, transform.rotation);
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-75f, 0f));
        }

		if (timeElapsed % 7 < 0.05)
		{
			Vector3 spawnOffset = new Vector3(0f, Random.Range(-4f, 4f), 0f);
			var asteroid = (GameObject)Instantiate(asteroidPrefab, transform.position + spawnOffset, transform.rotation);
			asteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(-135f, 0f));
		}

		if (timeElapsed % 9 < 0.1)
		{
			Vector3 spawnOffset = new Vector3(0f, Random.Range(-4f, 4f), 0f);
			var asteroid = (GameObject)Instantiate(asteroidPrefab, transform.position + spawnOffset, transform.rotation);
			asteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(-125f, 0f));
		}

		if (baseArmor <=0)
		{
			txtBaseArmor.text = "Base Armor: 0";
			gameOver = true;
		}

		if (gameOver)
		{
			gameOverNotice.text = "Game Over!" + "\n" + "Your peopole will remember you." + "\n" + 
				"Your Score: " + score + "\n" + "Press R to think another way"; //If you see score adding up after game overs, it is actually a feature.
		}

        if (gameOver && Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}



	}



	private void OnTriggerEnter (Collider other)
	{
        Debug.Log("Enemy collided with: " + other.tag);
        if (other.tag.Equals("boundary"))
        {
            Destroy(gameObject);
        }
	}


	public void PlayerDead()
	{
		gameOver = true;
	}
}
