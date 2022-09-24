using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerInstanceManager : MonoBehaviour {
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private List<GameObject> spawnPositions;

    // Start is called before the first frame update
    void Start() {
        if (spawnPositions.Count <= 0) {
            return;
        }

        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position, Quaternion.identity);
        } else {
            Instantiate(playerPrefab, spawnPositions[0].transform);
        }

    }
}
