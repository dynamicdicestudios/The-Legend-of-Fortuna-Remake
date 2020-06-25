using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
	GameObject projectile;
	
	[SerializeField]
	Transform projectileSpawnPos;
	
	[SerializeField]
	Transform groundCheckL;
	
	[SerializeField]
	Transform groundCheck;
	
	[SerializeField]
	Transform groundCheckR;
	
	[SerializeField] 
	public LayerMask platformLayerMask;
	
	[SerializeField]
	private float shootDelay = 1;
	
	[SerializeField]
	private int health;
	
	[SerializeField]
	GameObject attackHitBox;
	
	[SerializeField]
	private float moveSpeed;
	
	[SerializeField]
	private float jumpHeight;
	
	[SerializeField]
	GameObject enemies;
	
	[SerializeField]
	GameObject hearts;
	
	[SerializeField] 
	bool multipleAvatars;
	
	[SerializeField] 
	bool hasProjectiles;
	
	Animator animator;
	Rigidbody2D rb2d;
	SpriteRenderer spriteRenderer;
	BoxCollider2D boxCollider2d;

    public const string RIGHT = "right";
    public const string LEFT = "left";

    string buttonPressed = RIGHT;
	
	int avatarOn;
	
	bool isShooting;
	bool isGrounded = true;
	bool isAttacking;
	bool isSetup;
	bool isDead;

    
	// Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider2d = GetComponent<BoxCollider2D>();
		platformLayerMask = GetComponent<LayerMask>();
		projectile = GetComponent<GameObject>();
		projectileSpawnPos = GetComponent<Transform>();
		attackHitBox.SetActive(false);
		hearts = GetComponent<GameObject>();
		
		Setup();
		
		Debug.Log("start");
    }

    //Put physics based movement in here
    private void FixedUpdate()
    {	
		if (!isSetup)
			Setup();
		
		if (((Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
			(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
			(Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))) &&
			Input.GetKey("space"))
		{
			isGrounded = true;
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
		} else {
			isGrounded = false;
		}		
		if (this.gameObject.activeSelf && !isDead) {
			if (Input.GetKey("x") && hasProjectiles) {
				if (isShooting) return;
					
				animator.Play("XRight");
				rb2d.velocity = new Vector2(0, rb2d.velocity.y);
				
				
				GameObject a = Instantiate(projectile);
				a.GetComponent<ArrowScript>().StartShoot(buttonPressed);
				a.transform.position = projectileSpawnPos.transform.position;
			
				isShooting = true;
				Invoke("ResetShoot", shootDelay);
			}
			if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
			{
				rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
				animator.Play("MoveRight");
				transform.localScale = new Vector3(6, 6, 1);
				
				buttonPressed = RIGHT;
				
				if(Input.GetKey("z")) {
					animator.Play("ZRight");
					rb2d.velocity = new Vector2(0, rb2d.velocity.y);
					isAttacking = true;
					StartCoroutine(DoAttack());
				}
			}
			else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
			{
				rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
				animator.Play("MoveRight");
				transform.localScale = new Vector3(-6, 6, 1);
				
				buttonPressed = LEFT;
				
				if(Input.GetKey("z")) {
					animator.Play("ZRight");
					rb2d.velocity = new Vector2(0, rb2d.velocity.y);
					isAttacking = true;
					StartCoroutine(DoAttack());
				}
			}
			else
			{	
				if(Input.GetKey("z"))
				{
					animator.Play("ZRight");
					rb2d.velocity = new Vector2(0, rb2d.velocity.y);
					isAttacking = true;
					StartCoroutine(DoAttack());
				} else {
					animator.Play("IdleRight");
				}
				rb2d.velocity = new Vector2(0, rb2d.velocity.y);
			}
			if (multipleAvatars) {
				if (Input.GetKey("1"))
					FindObjectOfType<PlayerManager>().SwitchAvatar();
				//else if (Input.GetKey("2") && avatarOn == 1)
					//FindObjectOfType<PlayerManager>().SwitchAvatar();
			}
		}
    }
	
	IEnumerator DoAttack() {
		attackHitBox.SetActive(true);
		yield return new WaitForSeconds(0.005f);
		attackHitBox.SetActive(false);
		isAttacking = false;
	}
	
	IEnumerator Die() {
		yield return new WaitForSeconds(2f);
		this.gameObject.SetActive(false);
	}
	
	IEnumerator Win() {
		Debug.Log("You win!");
		yield return new WaitForSeconds(1f);
		FindObjectOfType<ObjectiveManager>().ReachPoint();
	}
	
	void ResetShoot() {
		animator.Play("IdleRight");
		isShooting = false;
	}
	
	void Setup() {
		
		Debug.Log("setup");
		attackHitBox.SetActive(false);
		if (multipleAvatars)
			avatarOn = FindObjectOfType<PlayerManager>().GetAvatarOn();
		
		if (health == 5) {
			hearts.transform.GetChild(5).transform.gameObject.SetActive(false);
			hearts.transform.GetChild(6).transform.gameObject.SetActive(false);
		}else if (health == 6)
			hearts.transform.GetChild(6).transform.gameObject.SetActive(false);
		isSetup = true;
	}
	
	void RemoveHeart() {
		try {
		hearts.transform.GetChild(health).transform.gameObject.SetActive(false);
		} catch (Exception e) {}		
	}
	
	public void Deactivate() {
		this.gameObject.SetActive(false);
	}
	
	public void Activate() {
		this.gameObject.SetActive(true);
	}
	
	public bool isActivated() {
		return this.gameObject.activeSelf;
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Orc") || collision.CompareTag("BlueOrc")) {
			if (isAttacking && 
			collision.transform.position.x > transform.position.x) {
				return;
			} else {
				health -= 1;
				RemoveHeart();
			}
			
			if (transform.position.x < collision.transform.position.x)
				rb2d.velocity = new Vector2(moveSpeed, 0);
			else
				rb2d.velocity = new Vector2(-moveSpeed, 0);
			
			if (health <= 0) {
				animator.Play("BaseDie");
				isDead = true;
				StartCoroutine(Die());
			}
		} else if (collision.CompareTag("GiantOrc")) {
			if (isAttacking && 
			collision.transform.position.x > transform.position.x) {
				health -= 1;
				RemoveHeart();
			} else {
				health -= 1;
				RemoveHeart();
				health -= 1;
				RemoveHeart();
			}
			
			if (transform.position.x < collision.transform.position.x)
				rb2d.velocity = new Vector2(moveSpeed, 0);
			else
				rb2d.velocity = new Vector2(-moveSpeed, 0);
			
			if (health <= 0) {
				animator.Play("BaseDie");
				isDead = true;
				StartCoroutine(Die());
			}
		} else if (collision.CompareTag("Flag")) {
			StartCoroutine(Win());	
		}
	}
}