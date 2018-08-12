using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

  [Range(0f, 1f)]
  public float scaleSlider;
  public float scaleSpeed;

  public Vector3 startScale;
  public Vector3 endScale;

  public void ScaleUp() {
    StartCoroutine("ScaleUpCoroutine");
  }

  IEnumerator ScaleUpCoroutine() {
    while( scaleSlider < 1f ) {
      transform.localScale = Vector3.Lerp(startScale, endScale, scaleSlider);
      scaleSlider += 0.05f;
      yield return new WaitForSeconds(scaleSpeed);
    }
  }
}
