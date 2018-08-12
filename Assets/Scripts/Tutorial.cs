using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

  public static Tutorial control;

  public Text helpText;
  public Edge edge;

  public bool movement = false;
  public bool space = false;
  public bool jump = false;
  public bool finish = false;

  public bool disabled = true;

  void Awake() {
    if( control == null ) {
      control = this;
    } else if( control != this ) {
      Object.Destroy(this.gameObject);
    }
  }

	void Start() {
    edge = Edge.control;
	}

	void Update () {
    if( !disabled && !finish && !movement && ( Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) )) {
      movement = true;
      UI.SetHelpText("You cannot jump without space (+).");
    }
	}

  public void Space() {
    if( disabled ) { return; }
    if( !space ) {
      space = true;
      UI.SetHelpText("Press SPACE to jump and consume space.");
    }
  }

  public void Jump() {
    if( disabled ) { return; }
    if( !jump ) {
      jump = true;
      UI.SetHelpText("Avoid the shrinking edge of the universe.");
      edge.StartEdge();
    }
  }

  public void Finish() {
    if( disabled ) { return; }
    if( jump && !finish ) {
      finish = true;
      helpText.text = "";
    }
  }

  public static void Enable() {
    control.disabled = false;
    UI.SetHelpText("Press A or D to move.");
  }

  public static void Disable() {
    control.disabled = true;
  }

  public static void Reset() {
    control.movement = false;
    control.space = false;
    control.jump = false;
    control.finish = false;
    UI.SetHelpText("Press A or D to move.");
  }
}
