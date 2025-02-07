using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // Kéo thả đối tượng đích vào đây.
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Kiểm tra nếu NavMeshAgent không tồn tại
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent không được tìm thấy!");
            return;
        }

        agent.SetDestination(target.position);
    }
}
