using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

  public static Level control;

  public int levelIndex;

  public bool triggerEdgeAfter = false;
  public float edgeDelay = 0f;

  public bool tutorialEnabled = false;
  public bool lastLevel = false;

  void Awake() {
    if( control == null ) {
      control = this;
    } else if( control != this ) {
      Object.Destroy(this.gameObject);
    }
  }

	void Start() {
    Game.RegisterLevel(levelIndex);

    if( tutorialEnabled ) {
      Tutorial.Enable();
    } else {
      Tutorial.Disable();
    }

    if( triggerEdgeAfter ) {
      StartCoroutine("DelayEdge");
    }
	}

  public void Loaded() {
    Player.EndFinishLevel();
    Edge.control.Reset();

    if( lastLevel ) {
      Player.YouWon();
    }
  }

  IEnumerator DelayEdge() {
    yield return new WaitForSeconds(edgeDelay);
    Edge.control.StartEdge();
  }
}
