using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Tốc độ đạn
    private Transform target; // Mục tiêu của đạn
    public GameObject explosionPrefab;  // Prefab hiệu ứng nổ

    private bool hasExploded = false;  // Kiểm tra xem đạn đã nổ chưa

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
        if (direction.magnitude <= distanceThisFrame && !hasExploded)
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
        // Đảm bảo chỉ gọi nổ một lần
        if (hasExploded) return;

        // Kiểm tra nếu mục tiêu là một Enemy
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(2f); // Gây 2 sát thương
        }

        // Tạo hiệu ứng nổ tại vị trí của đạn
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Đánh dấu là đã nổ và hủy đạn
        hasExploded = true;
        Destroy(gameObject);
    }
}
