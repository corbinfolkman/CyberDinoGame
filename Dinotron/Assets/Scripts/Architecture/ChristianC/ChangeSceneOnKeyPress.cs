using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneOnKeyPress : MonoBehaviour {

    public KeyCode key;
    public KeyCode altKey;
    public string scene;
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(key) || Input.GetKeyDown(altKey)) {
            SceneManager.LoadScene(scene);
        }
	}
}
