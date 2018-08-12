using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {

  public static Edge control;

  Rigidbody2D rb;
  EdgeCollider2D edgeCollider;

  public Vector3 startingPos;

  void Awake() {
    if( control == null ) {
      control = this;
    } else if( control != this ) {
      Object.Destroy(this.gameObject);
    }
  }

	void Start() {
    rb = GetComponent<Rigidbody2D>();
    edgeCollider = GetComponent<EdgeCollider2D>();
    startingPos = transform.position;
    DontDestroyOnLoad(this.gameObject);
	}

  void LateUpdate() {
    transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
  }

  void OnTriggerExit2D( Collider2D other ) {
    if( other.gameObject.name != "Player" ) {
      GameObject.Destroy(other.gameObject);
    }
  }

  public void StartEdge() {
    control.edgeCollider.enabled = true;
    if( !Tutorial.control.disabled ) {
      control.transform.position = new Vector3(-55f, control.transform.position.y, control.transform.position.z);
    }
    control.rb.velocity = new Vector2(74f, 0f);
  }

  public void Stop() {
    control.rb.velocity = Vector2.zero;
    control.edgeCollider.enabled = false;
  }

  public void Reset() {
    transform.position = startingPos;
    control.rb.velocity = Vector2.zero;
  }
}
