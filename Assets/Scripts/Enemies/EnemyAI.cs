using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    [SerializeField] public Transform target;
    private NavMeshAgent agent;

    private void Start() {
        // target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update() {
        agent.SetDestination(target.position);

        if (agent.velocity.x < 0.01f) {
            this.transform.localScale = new Vector3(-1, 1, 1);
        } else if (agent.velocity.x > 0.01f) {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
