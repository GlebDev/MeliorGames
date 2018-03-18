using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAttackable {


	public Transform walk_target;
	public IAttackable attack_target;
	public GameManager manager;

	[SerializeField] private float MoveSpeed, Damage, AtttackDelay, StopDistance;
	[SerializeField] private Animator EnemyAnimator;
	[SerializeField] private float MaxHP;
	[SerializeField] private int reward;

	private float HP;
	private bool IsDie;

	void Awake(){
		HP = MaxHP;
	}

	void Start(){
		IsDie = false;
		StartCoroutine (WalkTo(walk_target.position));
	}
		
	private IEnumerator WalkTo(Vector2 target){
		Vector2 direction = target - (Vector2)transform.position ;
		while(Mathf.Abs(target.x - transform.position.x) > StopDistance){
			transform.Translate (new Vector2 ((direction * MoveSpeed * Time.deltaTime).x, 0));
			yield return null;
		}
		OnComeToTarget();
		yield break;
	}


	private void OnComeToTarget(){
		StartCoroutine (Attack (attack_target, AtttackDelay));
	}

	private IEnumerator Attack(IAttackable target, float delay){
		EnemyAnimator.SetTrigger ("Attack");
		while (!IsDie) {
			target.GetDamage (Damage);
			yield return new WaitForSeconds (delay);
		}
		yield break;
	}

	private void Die(){
		IsDie = true;
		EnemyAnimator.SetTrigger ("Death");
		manager.GetReward (reward);
		manager.removeEnemie (this);

	}

	// IAttackable interface realization  
	public void GetDamage(float damage){
		if (HP - damage >= 0) {
			HP -= damage;
		} else if(!IsDie){
			Die();
		}
	}

	// call from animation Death
	public void Destroy(){
		Destroy (gameObject);
	}
}
	