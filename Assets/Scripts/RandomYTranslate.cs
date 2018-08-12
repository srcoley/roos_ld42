using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomYTranslate : MonoBehaviour {

  Transform[] transforms;

	void Start () {
    transforms = GetComponentsInChildren<Transform>();

    for( int i = 0; i < transforms.Length; i++ ) {
      transforms[i].position = transforms[i].position + new Vector3(0f, Random.Range(0, 3), 0f);
    }
	}

	void Update() {

	}
}
