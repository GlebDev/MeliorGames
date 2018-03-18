using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

	[SerializeField] private Image HPBarImage;
	[SerializeField] private Text HPtext;

	public void SetHPBar(float hp, float full_hp){
		HPBarImage.fillAmount = hp / full_hp;
		HPtext.text = hp.ToString ();
	}
}
