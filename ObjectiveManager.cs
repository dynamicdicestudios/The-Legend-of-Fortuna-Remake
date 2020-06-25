using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField]
	bool reachPoint;
	
	[SerializeField]
	bool defeatEnemies;
	
	[SerializeField]
	GameObject enemies;
	
	[SerializeField] 
	string animation; 
	
	void Update() {
		if (defeatEnemies)
			DefeatEnemies();
	}
	
	public void ReachPoint() {
		FindObjectOfType<LevelChanger>().FadeToLevel(animation);
	}
	
	public void DefeatEnemies() { 
		if (enemies.transform.childCount == 0) {
			FindObjectOfType<LevelChanger>().FadeToLevel(animation);
		}
	}
}
