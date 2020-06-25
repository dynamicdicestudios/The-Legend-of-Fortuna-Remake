using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    [SerializeField]
	float speed;
	
	[SerializeField]
	public int damage;
	
	[SerializeField]
	float timeToDestroy;
	
	float closest = 7f;
	float x;
	float y;
	
	public const string RIGHT = "right";
    public const string LEFT = "left";
	
	Rigidbody2D rb2d;
	
	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		
		rb2d.velocity = new Vector2(0, 20);
		
	}	
	
	private void FixedUpdate() {
		
		rb2d.velocity = new Vector2(x, y);
		
		Destroy(gameObject, timeToDestroy);
	}
	
	public void FindTarget(GameObject enemies, string playerDirection) {
		
		Debug.Log(enemies.transform.childCount);
		for (int i = 0; i <= enemies.transform.childCount; i++) {
			Transform enemy = enemies.transform.GetChild(i);
			float distToEnemy = Vector2.Distance(transform.position, enemy.position);
			
			if (distToEnemy <= closest) {
				if (enemy.position.y < transform.position.y) {
					Debug.Log("Below");
					Debug.Log(enemy.position.y);
					if (playerDirection == LEFT) {
						y = 0;
						x = -speed;
					} else {
						y = 0;
						x = speed;
					}
					if (playerDirection == LEFT) 
						transform.localScale = new Vector3(0.75f, 0.75f, 1);
					else
						transform.localScale = new Vector3(-0.75f, 0.75f, 1);
				} else {
					Debug.Log("Above");
					y = enemy.position.y - transform.position.y;
					x = enemy.position.x - transform.position.x;
					
					if (enemy.position.x < transform.position.x) 
						transform.localScale = new Vector3(0.75f, 0.75f, 1);
					else
						transform.localScale = new Vector3(-0.75f, 0.75f, 1);
				}
				
				
				
				break;
			}
		}
	}
}