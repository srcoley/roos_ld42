using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

  public static Game control;

  public AudioClip intro;
  public AudioClip loop;
  public AudioClip spacePickup;
  public AudioClip wormhole;
  public AudioClip edgeHit;
  public AudioClip fallDeath;
  public AudioSource introSource;
  public AudioSource loopSource;
  public AudioSource spacePickupSource;
  public AudioSource wormholeSource;
  public AudioSource edgeHitSource;
  public AudioSource fallDeathSource;

  int currentLevel = 1;
  bool audioLooping = false;

  void Awake() {
    if( control == null ) {
      control = this;
    } else if( control != this ) {
      Object.Destroy(this.gameObject);
    }
  }

  void Start() {
    DontDestroyOnLoad(this.gameObject);
    // SceneManager.LoadScene("Scene01");
  }

	void Update() {
    if( !audioLooping && introSource.time > 27.248f ) {
      loopSource.Play();
      audioLooping = true;
    }
	}

  public static void RegisterLevel( int levelIndex ) {
    control.currentLevel = levelIndex;
  }

  public static void RestartLevel() {
    control.StartCoroutine("RestartLevelCoroutine");
  }

  public static void FinishLevel() {
    control.wormholeSource.PlayOneShot(control.wormhole);
    control.currentLevel += 1;
    control.StartCoroutine("FinishLevelCoroutine");
  }

  public static void PlayFallDeath() {
    control.fallDeathSource.PlayOneShot(control.fallDeath);
  }

  public static void PlayEdgeHit() {
    control.edgeHitSource.PlayOneShot(control.edgeHit);
  }

  public static void PlaySpacePickup() {
    control.spacePickupSource.PlayOneShot(control.spacePickup);
  }

  IEnumerator RestartLevelCoroutine() {
    Debug.Log("restart level");
    Edge.control.Stop();
    AsyncOperation loadingLevel = SceneManager.LoadSceneAsync(GetScenePrefix() + currentLevel);

    while( true ) {
      yield return new WaitForSeconds(0.01f);

      if( loadingLevel.isDone ) {

        Edge.control.Reset();

        if( currentLevel == 1 ) {
          Tutorial.Reset();
        }

        Player.EndFinishLevel();
        break;
      }
    }
  }

  IEnumerator FinishLevelCoroutine() {
    Debug.Log("finish level coro");
    Edge.control.Stop();
    Player.StartFinishLevel();

    yield return new WaitForSeconds(0.8f);

    AsyncOperation loadingLevel = SceneManager.LoadSceneAsync(GetScenePrefix() + currentLevel);

    while( true ) {
      yield return new WaitForSeconds(0.01f);

      if( loadingLevel.isDone ) {
        Level.control.Loaded();
        // Player.EndFinishLevel();
        // Edge.control.Reset();
        break;
      }
    }
  }

  string GetScenePrefix() {
    if( currentLevel < 10 ) {
      return "Scene0";
    } else {
      return "Scene";
    }
  }
}
