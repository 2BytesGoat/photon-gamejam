using Photon.Pun;
using UnityEngine;

public class EnemyInstanceManager : MonoBehaviour {
    [SerializeField]
    private int enemiesCount;

    [SerializeField]
    private Vector3 spawnPosition;

    [SerializeField]
    private float spawnRadius;

    [SerializeField]
    private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start() {
        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient) {
            return;
        }

        for (int i = 0; i < enemiesCount; i++) {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            if (PhotonNetwork.IsConnected) {
                PhotonNetwork.Instantiate(enemyPrefab.name, spawnPosition + new Vector3(offset.x, offset.y), Quaternion.identity);
            } else {
                Instantiate(enemyPrefab, spawnPosition + new Vector3(offset.x, offset.y), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
