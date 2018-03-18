using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour {

	public int price;

	[SerializeField] private float Damage, AtttackDelay;
	[SerializeField] private Animator EnemyAnimator;

	private bool IsAttack;
	private IAttackable atttackTarget;
	private Coroutine AttackCoroutine; 


	void Start(){
		IsAttack = false;
	}

	public void SetTarget(IAttackable target){
		if (atttackTarget != target) {
			atttackTarget = target;
			if(AttackCoroutine != null){
				StopCoroutine (AttackCoroutine);
			}
			AttackCoroutine = StartCoroutine (Attack (atttackTarget, AtttackDelay));
		}
	}

	private IEnumerator Attack(IAttackable target, float delay){
		EnemyAnimator.SetTrigger ("Attack");
		while (!target.Equals(null)) {  //target != null not working
			yield return new WaitForSeconds (delay);
			target.GetDamage (Damage);
		}
		EnemyAnimator.ResetTrigger ("Attack");
		EnemyAnimator.SetTrigger ("Idle");
		yield break;
	}
		
}
