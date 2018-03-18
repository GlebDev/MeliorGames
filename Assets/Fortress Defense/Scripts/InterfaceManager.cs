using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {

	[SerializeField] private GameObject StartWaveButton;
	[SerializeField] private GameObject LosePanel, VictoryPanel;
	[SerializeField] private PauseMenuScript MenuPanel;
	[SerializeField] private Text ScoreText;


	public void SetStartWaveButtonVisibility(bool value){
		StartWaveButton.SetActive (value);
	}
		
	public void ShowLosePanel(){
		LosePanel.SetActive (true);
	}

	public void ShowVictoryPanel(){
		VictoryPanel.SetActive (true);
	}


	public void SetScoreText(string value){
		ScoreText.text = value;
	}

	public void TooglePauseMenuVisible(){
		MenuPanel.SetVisible(!MenuPanel.GetVisible()); 
	}

}
