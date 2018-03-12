using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SerializeField] int _maxHealth = 100;

	[SyncVar]
	private int _currentHealth;

	void Awake() {
		SetDefaults();
	}

	public void SetDefaults() {
		_currentHealth = _maxHealth;
	}

	public void TakeDamage(int amount) {
		_currentHealth -= amount;
		Debug.Log(transform.name + " now has " + _currentHealth + " health.");
	}
}
