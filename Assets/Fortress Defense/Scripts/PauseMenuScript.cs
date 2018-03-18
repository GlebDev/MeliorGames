using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour {

	[SerializeField] private GameObject MenuPanel;
	private bool PauseMenuStatus;


	public void SetVisible(bool value){
		MenuPanel.SetActive (value);
		PauseMenuStatus = value; 
	}

	public bool GetVisible(){
		return PauseMenuStatus;
	}
		
	public void LoadScene(string SceneName){
		SceneManager.LoadSceneAsync (SceneName);
	}

	public void Exit(){
		Application.Quit ();
	}

	private IEnumerator LoadLvl(string scene) {
		AsyncOperation async = SceneManager.LoadSceneAsync(scene);
		yield return async;
	}



}
