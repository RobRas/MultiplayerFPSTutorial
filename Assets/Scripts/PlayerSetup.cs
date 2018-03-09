using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    [SerializeField] Behaviour[] _componentsToDisable;

    Camera sceneCamera;

    private void Start() {
        if (!isLocalPlayer) {
            for (int i = 0; i < _componentsToDisable.Length; i++) {
                _componentsToDisable[i].enabled = false;
            }
        } else {
            sceneCamera = Camera.main;
            if (sceneCamera != null) {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable() {
        if (sceneCamera != null) {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
