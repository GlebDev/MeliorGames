using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] private EnemyAI[] EnemyPrefabs;
	[SerializeField] private UnitAI[] ArcherPrefabs;

	[SerializeField] private Transform EnemySpawnPoint;
	[SerializeField] private Transform Enemy_walk_target;
	[SerializeField] private Tower tower;
	[SerializeField] private List<EnemyWave> EnemyWaves;
	[SerializeField] private float EnenySpawnDelay;
	[SerializeField] private InterfaceManager interfaceManager;
	[SerializeField] private ArcherButton[] ArcherButtons;
	[SerializeField] private int Cash;

	private IAttackable enemy_attack_target;
	private List<EnemyAI> EnemyOnTheMapList = new List<EnemyAI>();
	private List<UnitAI> DefenderList = new List<UnitAI>();
	private int CurWaveIndex = -1;
	private int DeadEnemyCount;

	// Use this for initialization
	void Start () {
		enemy_attack_target = tower.GetComponent<IAttackable> ();
		tower.OnDie += Tower_OnDie;
		foreach(ArcherButton butn in ArcherButtons){
			butn.OnClick += ArcherButton_OnClick;
			butn.SetUpPriceText (ArcherPrefabs[butn.UnitLvl].price.ToString());
		}
		CalculateUpIconEnabled ();
		interfaceManager.SetScoreText (Cash.ToString());
		interfaceManager.SetStartWaveButtonVisibility (true);
	}
		
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			interfaceManager.TooglePauseMenuVisible ();
		}
	}
		
	private void Tower_OnDie (){
		interfaceManager.ShowLosePanel ();
		Time.timeScale = 0f;
	}
	
	private void ArcherButton_OnClick(ArcherButton button){
		if(ArcherPrefabs[button.UnitLvl].price <= Cash){
			Cash -= ArcherPrefabs[button.UnitLvl].price;
			interfaceManager.SetScoreText (Cash.ToString());
			button.SetArcherIconVisibility(false);
			if(button.Archer != null){
				DefenderList.Remove (button.Archer);
				Destroy (button.Archer.gameObject);
			}
			button.Archer = CreateDefender (ArcherPrefabs [button.UnitLvl], button.transform);
			button.UnitLvl++;
			if(button.UnitLvl < ArcherPrefabs.Length){
				button.SetUpPriceText (ArcherPrefabs[button.UnitLvl].price.ToString());
			}
			CalculateUpIconEnabled ();
		}

	}

	//determine the state of lvl-up buttons
	private void CalculateUpIconEnabled(){
		foreach(ArcherButton butn in ArcherButtons){
			if (butn.UnitLvl < ArcherPrefabs.Length) {
				bool value = Cash >= ArcherPrefabs [butn.UnitLvl].price;
				butn.SetUpEnabled (value);
			} else {
				butn.SetUpIconVisability (false);
			}
				
		}
	}

	public void GetReward(int value){
		Cash += value;
		CalculateUpIconEnabled ();
		interfaceManager.SetScoreText (Cash.ToString());
	}
		
	#region Enemy
	public void StartNextWave(){
		CurWaveIndex++;
		StartCoroutine (CreatingEnemyWave(EnemyWaves[CurWaveIndex]));
	}

	private IEnumerator CreatingEnemyWave(EnemyWave wave){
		DeadEnemyCount = 0;
		for (int i = 0; i < wave.EnemyIndexes.Length; i++) {
			yield return new WaitForSeconds (EnenySpawnDelay);
			CreateEnemy (EnemyPrefabs [wave.EnemyIndexes [i]]);
			FindTargetForDefenders ();
		}
		yield break;
	}

	private void CreateEnemy(EnemyAI EnemyPrefab){
		EnemyAI CurEnemy = Instantiate(EnemyPrefab, EnemySpawnPoint) as EnemyAI;
		CurEnemy.attack_target = enemy_attack_target;
		CurEnemy.walk_target = Enemy_walk_target;
		CurEnemy.manager = this;
		EnemyOnTheMapList.Add (CurEnemy);
	}
		
	public void removeEnemie(EnemyAI unit){
		EnemyOnTheMapList.Remove (unit);
		DeadEnemyCount++;
		if (EnemyOnTheMapList.Count <= 0 && DeadEnemyCount == EnemyWaves[CurWaveIndex].EnemyIndexes.Length ) {
			GetReward (EnemyWaves[CurWaveIndex].WaweReward);
			if (CurWaveIndex < EnemyWaves.Count - 1) {
				interfaceManager.SetStartWaveButtonVisibility(true);
			} else {
				interfaceManager.ShowVictoryPanel ();
			}
		} else {
			FindTargetForDefenders ();
		}
	}

	#endregion

	#region Defender
	private void FindTargetForDefenders(){
		if(EnemyOnTheMapList.Count > 0){
			foreach(UnitAI defender in DefenderList){
				defender.SetTarget (EnemyOnTheMapList [0]);
			}
		}
	}

	private UnitAI CreateDefender(UnitAI archerPrefab, Transform parent){
		UnitAI CurUnit = Instantiate(archerPrefab, parent.position, Quaternion.identity ,parent) as UnitAI;
		DefenderList.Add (CurUnit);
		FindTargetForDefenders ();
		return CurUnit;
	}
	#endregion

}
