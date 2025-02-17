using UnityEngine;

public class TrailFix : MonoBehaviour
{
    void Start()
    {
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.startWidth = 0.5f; // Giá trị độ rộng bắt đầu
            trail.endWidth = 0.1f;   // Giá trị độ rộng kết thúc
        }
    }
}
