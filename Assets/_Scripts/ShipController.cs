using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour 
{
	private float timepass = 0;
	public float speed = 5f;
	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	private Rigidbody2D rb;
	public Text txtAmmo;

	private int ammoCount;

	public int initAmmo = 200;
	public AudioSource laserSound;
	public GameObject Explosion;
    public AudioSource playerExplosion;

	private GameController Controller;

	//Rigidbody2D是一个默认的刚体类么？是否当我们每次使用一个component的时候都要查阅文档看其有什么函数？

	// Use this for initialization
	void Start () 
	{
		ammoCount = initAmmo;
		txtAmmo.text = "Ammo: " + ammoCount;
		rb = gameObject.GetComponent <Rigidbody2D>();
		Controller = (GameController)GameObject.Find ("TheGame").GetComponent("GameController");
		//gameObject又是个什么东西？Get.Component是它的成员么？这个尖括号后跟一个圆括号是它的语法么？
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//if(Input.GetKeyDown(KeyCode.LeftArrow))
		//	Debug.Log("Left arrow key pressed");
		if (!Controller.gameOver) 
		{
			float moveHM = Input.GetAxis("Mouse X");
			float moveVM = Input.GetAxis("Mouse Y");
			float moveHK = Input.GetAxis ("Horizontal");
			float moveVK = Input.GetAxis ("Vertical");
			//这里Input可以智能识别横向和纵向的箭头by receiving这两个特殊的字符就可以了吗？这也是函数已经预设好了的是么？
			//要是我想用别的按键来操纵呢？

			Vector2 motionM = new Vector2 (moveHM, moveVM);
			Vector2 motionK = new Vector2 (moveHK, moveVK);
			//Vector2又是一个什么类？为什么叫这名字……

			rb.AddForce ((motionM+motionK) * speed);

			float currentTime = Time.time;

			if (((Input.GetKeyDown (KeyCode.Space))||(Input.GetMouseButtonDown(0))) && (currentTime - timepass) > 0.3f)
			{
				timepass = Time.time;
				Fire ();
			}

			//float zero = 0f;
				//if(Input.GetMouseButton(0))
				//print("Mouse Down!");
				//Input.GetAxis("Mouse X");//鼠标横向增量（横向移动） 
				//Input.GetAxis("Mouse Y");

			if (!Input.GetKey (KeyCode.RightArrow) || !Input.GetKey (KeyCode.LeftArrow) || !Input.GetKey (KeyCode.UpArrow) || !Input.GetKey (KeyCode.DownArrow)) {
				rb.velocity = Vector3.zero;
			}
		}
	}
	//Ilia


	public void Fire()
	{
		if (ammoCount > 0) 
		{
			ammoCount--;
			txtAmmo.text = "Ammo:" + ammoCount;
			laserSound.Play ();//different variable name
            var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
			//GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

			Vector2 bulletMotion = new Vector2 (10f, 0f);
			bullet.GetComponent<Rigidbody2D> ().AddForce (bulletMotion * 30);
			Destroy (bullet, 4f);//Long enough to let it fly through the scence without being destroyed
			//gameObject.GetComponent<AudioSource> ().Play ();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	//这OnTriggerEnter2D难道又是一个库函数么……Collider2D貌似是碰撞体类？跟上面的Rigidbody2D一样？
	{
		if (other.tag == "enemy" || other.tag == "asteroid") 
		{
			Instantiate (Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			Instantiate (Explosion, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation);
			Destroy (other.gameObject);
			Destroy (gameObject);
            playerExplosion.Play();
            //enemyExplosion.Play();//但我似乎应该把这个行为放到enemyship类里去……
			GameController.instance.PlayerDead ();//他妈这些函数是怎么出来的……
		}
		//因为other是一个Collider类，这等于说是Collider2D里面有一个tag的成员，这里输出他的默认值咯？
	}

	/*void OnCollisionEnter2D()
	{
		Debug.Log ("Something collide");
	}

	void OnTriggerEnter2d()
	{
		Debug.Log ("Something");
	}*/



}


