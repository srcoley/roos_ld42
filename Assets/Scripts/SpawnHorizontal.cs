using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnHorizontal : MonoBehaviour {

  public Transform prefab;
  public int amount;

  Transform newObj;

	void Start() {
    for( int i = 0; i < amount; i++ ) {
      int randY = Random.Range(0,2);
      Vector3 newPos = transform.position + new Vector3(17 * i+1, randY, 0f);
      newObj = Object.Instantiate(prefab, newPos, Quaternion.identity);
      newObj.parent = transform;
    }
	}
}
