using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcScript : MonoBehaviour
{	
	[SerializeField]
	float agroRange;
	
	[SerializeField]
	float moveSpeed;
	
	[SerializeField]
	private int health;
	
	[SerializeField]
	public int damage;
	
	bool isFacingLeft;
	
	Rigidbody2D rb2d;
	Animator animator;
	SpriteRenderer spriteRenderer;
	Transform player;
	
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerController>().transform;
		float distToPlayer = Vector2.Distance(transform.position, player.position);
		
		if (distToPlayer < agroRange) {
			ChasePlayer();
		}else {
			StopChasingPlayer();
		}
    }
	
	public int GetDamage() {
		return damage;
	}
	
	private void ChasePlayer() { 
		 if (transform.position.x < player.position.x) {
			rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
			animator.Play("OrcRight");
			transform.localScale = new Vector3(6, 6, 1);
			isFacingLeft = false;
		 } else {
			rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
			animator.Play("OrcRight");
			transform.localScale = new Vector3(-6, 6, 1);
			isFacingLeft = true;
		 }
	} 
	
	private void StopChasingPlayer() { 
		rb2d.velocity = new Vector2(0, rb2d.velocity.y);
		if (isFacingLeft)
			animator.Play("OrcIdleRight");
		else
			animator.Play("OrcIdleRight");
	}
	
	IEnumerator Die() {
		yield return new WaitForSeconds(0.2f);
		Destroy(gameObject);
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Projectile")) {
			Destroy(collision.gameObject);
			health -= collision.gameObject.GetComponent<ArrowScript>().damage;
			if (transform.position.x < collision.transform.position.x)
				rb2d.velocity = new Vector2(moveSpeed, 0);
			else
				rb2d.velocity = new Vector2(-moveSpeed, 0);
			
			if (health <= 0) {
				StartCoroutine(Die());
			}
		} else if (collision.CompareTag("Small Blade")) {
			health -= 3;
			if (transform.position.x < collision.transform.position.x)
				rb2d.velocity = new Vector2(moveSpeed, 0);
			else
				rb2d.velocity = new Vector2(-moveSpeed, 0);
			
			if (health <= 0) {
				StartCoroutine(Die());
			}
		} else if (collision.CompareTag("Weapon")) {
			health -= 5;
			if (transform.position.x < collision.transform.position.x)
				rb2d.velocity = new Vector2(moveSpeed, 0);
			else
				rb2d.velocity = new Vector2(-moveSpeed, 0);
			
			if (health <= 0) {
				StartCoroutine(Die());
			}
		}
	}
}