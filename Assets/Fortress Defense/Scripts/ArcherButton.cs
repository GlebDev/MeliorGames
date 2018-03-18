using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ArcherButton : MonoBehaviour, IPointerClickHandler {

	public event System.Action<ArcherButton> OnClick;
	public int UnitLvl;
	public UnitAI Archer;

	[SerializeField] private GameObject ArcherIcon;
	[SerializeField] private SpriteRenderer UpIcon;
	[SerializeField] private Sprite UpIconEnabled,UpIconDisabled;
	[SerializeField] private TextMesh UpPrice;


	public void OnPointerClick (PointerEventData eventData) {
		if (OnClick != null){
			OnClick(this);
		}
	}

	public void SetArcherIconVisibility(bool value) {
		ArcherIcon.SetActive (value);
	}

	//determine the possibility of increasing the archer's level
	public void SetUpEnabled(bool value) {
		switch(value){
		case true:
			UpIcon.sprite = UpIconEnabled;
			break;
		case false:
			UpIcon.sprite = UpIconDisabled;
			break;
		}
	}

	public void SetUpIconVisability(bool value) {
		UpIcon.gameObject.SetActive (value);
	}

	public void SetUpPriceText(string value) {
		UpPrice.text = value;
	}

}
