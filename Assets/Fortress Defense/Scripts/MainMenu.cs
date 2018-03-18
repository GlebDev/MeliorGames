using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadScene(string SceneName){
		SceneManager.LoadSceneAsync (SceneName);
	}

	public void Exit(){
		Application.Quit ();
	}

	public void OpenURL(string url){
		Application.OpenURL(url);
	}
}
