using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

  public static UI control;

  public Text helpText;
  public Text spaceText;
  public Image helpTextBackground;

  public Color helpTextBackgroundActive;
  public Color helpTextBackgroundInactive;

  public static void SetHelpText(string text) {
    control.helpText.text = text;
  }

  void Awake() {
    if( control == null ) {
      control = this;
    } else if( control != this ) {
      Object.Destroy(this.gameObject);
    }
  }

	void Start() {
    DontDestroyOnLoad(this.gameObject);
	}

	void Update() {
    spaceText.text = "Space: " + Player.control.space;

    if( helpText.text == "" ) {
      helpTextBackground.color = helpTextBackgroundInactive;
    } else {
      helpTextBackground.color = helpTextBackgroundActive;
    }
	}
}
