using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
	float speed;
	
	[SerializeField]
	public int damage;
	
	[SerializeField]
	float timeToDestroy;
	
	public const string RIGHT = "right";
    public const string LEFT = "left";
	
	
	public void StartShoot(string playerDirection) {
		if (playerDirection == LEFT) {
			transform.localScale = new Vector3(-0.75f, 0.5f, 1);
			GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
		} else {
			transform.localScale = new Vector3(0.75f, 0.5f, 1);
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		}
		Destroy(gameObject, timeToDestroy);
	}
}
