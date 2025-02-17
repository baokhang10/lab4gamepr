using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string gameOverScene = "GameOverScene"; // Đặt tên scene bạn muốn load

    public void GameOver()
    {
        Debug.Log("Game Over! Loading new scene...");
        SceneManager.LoadScene("GameOver");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameOver();
        }
    }
}
