using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerInstanceManager : MonoBehaviour {
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private List<GameObject> spawnPositions;

    [SerializeField] 
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start() {
        int randomSpawnIndex = Random.Range(0, spawnPositions.Count - 1);
        Vector3 playerPosition = spawnPositions[randomSpawnIndex].transform.position;
        playerPosition.z = 0;

        GameObject player;

        if (PhotonNetwork.IsConnected) {
            player = PhotonNetwork.Instantiate(this.playerPrefab.name, playerPosition, Quaternion.identity);
        } else {
            player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        }

        if (player != null) {
            player.GetComponent<PlayerMovement>().camera = mainCamera;
        }
    }
}
