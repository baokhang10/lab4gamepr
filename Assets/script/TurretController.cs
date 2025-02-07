using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform target; // Mục tiêu (Enemy)
    public Transform firePoint; // Vị trí bắn
    public GameObject bulletPrefab; // Prefab đạn
    public float fireRate = 1f; // Tốc độ bắn (viên/giây)
    public float rotationSpeed = 5f; // Tốc độ quay của súng

    private float fireCooldown = 0f;

    void Update()
    {
        if (target != null)
        {
            // Quay súng về phía mục tiêu
            RotateTowardsTarget();

            // Bắn đạn nếu hết thời gian hồi chiêu
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate; // Đặt lại thời gian hồi chiêu
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Tạo đạn tại Fire Point
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Gán mục tiêu cho đạn
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetTarget(target);
            }
        }
        else
        {
            Debug.LogWarning("BulletPrefab hoặc FirePoint chưa được gán!");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
