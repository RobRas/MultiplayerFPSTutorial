using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {
    [SerializeField] Behaviour[] _componentsToDisable;
    [SerializeField] string remotePlayerLayerName = "RemotePlayer";

    Camera sceneCamera;

    private void Start() {
        if (!isLocalPlayer) {
            DisableComponents();
            AssignRemoteLayer();
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

        GameManager.UnregisterPlayer(transform.name);
    }

    public override void OnStartClient() {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netID, player);
    }

    void DisableComponents() {
        for (int i = 0; i < _componentsToDisable.Length; i++) {
                _componentsToDisable[i].enabled = false;
            }
    }

    void AssignRemoteLayer() {
        gameObject.layer = LayerMask.NameToLayer(remotePlayerLayerName);
    }
}
