using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faller : MonoBehaviour {

  Rigidbody2D rb;
  bool fallen = false;

	void Start () {
    rb = GetComponent<Rigidbody2D>();
    rb.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	void Update () {

	}

  void OnCollisionEnter2D( Collision2D other ) {
    if( other.gameObject.name == "Player" ) {
      StartCoroutine("FallCoroutine");
    }
  }

  IEnumerator FallCoroutine() {
    yield return new WaitForSeconds(0.4f);
    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
  }
}
