using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {
	public PlayerWeapon Weapon;

	[SerializeField] Camera _camera;
	[SerializeField] LayerMask _mask;

	private const string PLAYER_TAG = "Player";

	void Start() {
		if (_camera == null) {
			Debug.LogError("No camera found");
			this.enabled = false;
		}
	}

	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			Shoot();
		}
	}

	[Client]
	void Shoot() {
		RaycastHit hit;
		if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, Weapon.Range, _mask)) {
			if (hit.collider.CompareTag(PLAYER_TAG)) {
				CmdPlayerShot(hit.collider.name, Weapon.Damage);
			}
		}
	}

	[Command]
	private void CmdPlayerShot(string playerID, int damage) {
		Debug.Log(playerID + " has been shot.");

		Player player = GameManager.GetPlayer(playerID);
		player.TakeDamage(damage);
	}
}
