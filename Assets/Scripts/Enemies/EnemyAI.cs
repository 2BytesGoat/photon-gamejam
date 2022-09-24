using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public float detectionRadiusSize;
    public LayerMask detectionLayer;

    private NavMeshAgent agent;
    private Transform target;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update() {
        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient) {
            return;
        }

        DetectObject();

        if (target == null) {
            return;
        }

        agent.SetDestination(target.position);
    }

    private void DetectObject() {
        Collider2D collider = Physics2D.OverlapCircle(this.transform.position, detectionRadiusSize, detectionLayer);
        if (collider != null) {
            target = collider.transform;
        } else {
            target = null;
        }
    }
}
