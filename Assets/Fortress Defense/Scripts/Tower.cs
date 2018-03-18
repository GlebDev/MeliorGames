using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IAttackable {

	public event System.Action OnDie;

	[SerializeField] private float MaxHP;
	[SerializeField] private Animator BaseAnimator;
	[SerializeField] private HPBar TowerHPBar;


	private float HP;

	// Use this for initialization
	void Start () {
		HP = MaxHP;
		TowerHPBar.SetHPBar (HP,MaxHP);
	}
		
	public void GetDamage(float damage){
		if (HP - damage > 0) {
			HP -= damage;
			TowerHPBar.SetHPBar (HP,MaxHP);
			BaseAnimator.Play ("Destroy", 0, 1 - HP / MaxHP);
		} else {
			TowerHPBar.SetHPBar (0,MaxHP);
			if (OnDie != null){
				OnDie ();
			}

		}
	}


}
	
