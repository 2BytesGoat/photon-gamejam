using UnityEngine;
using UnityEngine.AI;

public class EnemyGFX : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start() {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (agent.velocity.x < 0.01f) {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (agent.velocity.x > 0.01f) {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
