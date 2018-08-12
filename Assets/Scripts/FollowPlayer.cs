using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

  public static FollowPlayer control;

  Transform target;

  void Awake() {
    if( control == null ) {
      control = this;
    } else if( control != this ) {
      Object.Destroy(this.gameObject);
    }
  }

	void Start() {
    target = Player.control.transform;
    transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    DontDestroyOnLoad(this.gameObject);
    StartCoroutine("Follow");
	}

  void Update() {
    if( Input.GetKeyDown(KeyCode.Escape) ) {
      Application.Quit();
    }
  }

  IEnumerator Follow() {
    while( true ) {
      transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
      yield return new WaitForSeconds(0.01f);
    }
  }
}
