using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

}
