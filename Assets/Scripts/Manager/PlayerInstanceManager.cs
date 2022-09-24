using System.Collections;
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

        PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber].transform.position, Quaternion.identity);
    }
}
