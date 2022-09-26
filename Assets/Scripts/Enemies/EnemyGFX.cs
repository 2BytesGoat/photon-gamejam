using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGFX : MonoBehaviour, IPunObservable {
    private NavMeshAgent agent;
    private bool flipped;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(flipped);
        } else {
            flipped = (bool)stream.ReceiveNext();
        }
    }

    private void Start() {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient) {
            return;
        }

        flipped = agent.velocity.x < 0 || (agent.velocity.x == 0 && flipped);
    }

    private void LateUpdate() {
        if (flipped) {
            this.transform.localScale = new Vector3(-1, 1, 1);
        } else {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
