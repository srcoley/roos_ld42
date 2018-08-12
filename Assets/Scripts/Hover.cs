using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

  public float waitTime = 0.5f;

  float startingY;
  Vector3 offset;

	void Start() {
    startingY = transform.position.y;

    StartCoroutine("HoverCoroutine");
	}

  IEnumerator HoverCoroutine() {
    while(true) {
      yield return new WaitForSeconds(waitTime);

      if( transform.position.y > startingY ) {
        offset = new Vector3(0f, -1f, 0f);
      } else if( transform.position.y == startingY ) {
        offset = new Vector3(0f, 1f, 0f);
      }
      transform.position = transform.position + offset;
    }
  }
}
