using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

  public static Player control;

  public int speed = 10;
  public float maxSpeed = 50f;
  public float jumpSpeed = 100f;
  public float maxJumpSpeed = 150f;

  public int space = 0;
  public Edge edge;

  SpriteRenderer sr;
  Rigidbody2D rb;
  Animator anim;

  Vector3 startingPos;

  bool dead = false;
  bool dying = false;
  bool finishing = false;
  bool jump = false;
  public bool grounded = true;

  Tutorial tutorial;

  void Awake() {
    if( control == null ) {
      control = this;
    } else if( control != this ) {
      Object.Destroy(this.gameObject);
    }
  }

	void Start () {
    sr = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    edge = Edge.control;
    tutorial = Tutorial.control;
    startingPos = transform.position;
    DontDestroyOnLoad(this.gameObject);
	}

  void Update() {

    if( !dying && ( dead || (!dead && transform.position.y < -80) ) ) {
      if( transform.position.y < -80 ) {
        Game.PlayFallDeath();
      }
      dying = true;
      Game.RestartLevel();
    }

    if( Input.GetKeyDown(KeyCode.Space) && !jump && space > 0 ) {
      jump = true;
    }
  }

	void FixedUpdate() {
    float horizontal = Input.GetAxis("Horizontal");

    if( horizontal != 0 ) {
      anim.SetBool("running", true);
      rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

      if( horizontal > 0 ) {
        sr.flipX = false;
      } else if( horizontal < 0 ) {
        sr.flipX = true;
      }

    } else {
      anim.SetBool("running", false);
      rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    if( jump && space > 0 ) {
      if( tutorial.enabled ) {
        tutorial.Jump();
      }
      space -= 1;
      grounded = false;
      anim.SetBool("jump", true);
      rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
      Debug.Log("remove space");
      jump = false;
    } else if( space <= 0 ) {
      jump = false;
    }

    if( rb.velocity.x > maxSpeed ) {
      rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
    } else if( rb.velocity.x < -maxSpeed ) {
      rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
    }

    transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0f);
	}

  void OnCollisionEnter2D( Collision2D other ) {
    if( other.gameObject.tag == "Plus" ) {
      Game.PlaySpacePickup();
      GameObject.Destroy(other.gameObject);
      if( tutorial.enabled ) {
        tutorial.Space();
      }
      space += 1;
    }
  }

  void OnTriggerEnter2D( Collider2D other ) {
    if( other.gameObject.name == "Portal" && !finishing ) {
      Portal portal = other.gameObject.GetComponent<Portal>();
      portal.ScaleUp();
      finishing = true;
      Game.FinishLevel();
    }

    if( other.gameObject.name == "Edge" ) {
      dead = true;
      Game.PlayEdgeHit();
    }

    if( other.gameObject.tag == "Platform" && !grounded ) {
      grounded = true;
      anim.SetBool("jump", false);
    }
  }

  public static void StartFinishLevel() {
    control.sr.enabled = false;
    control.rb.constraints = RigidbodyConstraints2D.FreezeAll;
  }

  public static void EndFinishLevel() {
    ResetPosition();
    control.space = 0;
    control.dead = false;
    control.dying = false;
    control.finishing = false;
    control.sr.enabled = true;
    control.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
  }

  public static void ResetPosition() {
    control.transform.position = control.startingPos;
  }

  public static void YouWon() {
    control.anim.SetBool("running", false);
    control.anim.SetBool("jump", false);
    control.transform.position = control.transform.position + new Vector3(0f, 20f, 0);
    control.rb.constraints = RigidbodyConstraints2D.FreezeAll;
  }
}
