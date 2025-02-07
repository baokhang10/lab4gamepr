using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Tốc độ đạn
    private Transform target; // Mục tiêu của đạn

    // Gán mục tiêu cho đạn
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            // Nếu mục tiêu bị phá hủy hoặc biến mất, hủy đạn
            Destroy(gameObject);
            return;
        }

        // Tính toán hướng và di chuyển đạn về phía mục tiêu
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Kiểm tra nếu đạn chạm mục tiêu
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Di chuyển đạn
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target); // Hướng đạn về phía mục tiêu
    }

    private void HitTarget()
    {
        // Gây sát thương hoặc thêm hiệu ứng tại đây
        Debug.Log("Hit Target!");
        Destroy(target.gameObject); // Hủy mục tiêu (Enemy)
        Destroy(gameObject); // Hủy đạn sau khi chạm mục tiêu
    }
}
