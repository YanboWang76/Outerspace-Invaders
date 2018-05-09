using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {


    public AudioSource enemyExplosion;
	// Update is called once per frame
	void Update () 
	{
		gameObject.GetComponent<Transform> ().Rotate(new Vector2 (90f, 0f));
		//小写的game是这个体本身（子弹）
	}
    
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "enemy")
		{
			Debug.Log ("Bullet hit " + other.tag);
			Destroy (other.gameObject);
			Destroy (gameObject);//这里可以直接写gameObject,因为默认是这个脚本里的gameObject
            enemyExplosion.Play();
			//但是加了一个if，不然会摧毁飞船本身
		}
	}
}
