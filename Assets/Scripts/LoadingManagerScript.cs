using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingManagerScript : MonoBehaviour {

	IEnumerator Start() {
		Resources.UnloadUnusedAssets ();

		AsyncOperation async = SceneManager.LoadSceneAsync ("Play");
		yield return async;
	}
}
