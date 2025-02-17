using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f; // Máu của quái vật

    // Hàm giảm máu khi bị bắn
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            Die(); // Khi hết máu, gọi hàm Die() để xóa quái vật
        }
    }

    // Quái vật chết, xóa quái vật khỏi scene
    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // Hủy quái vật
    }
}
