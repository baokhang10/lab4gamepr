using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public Transform target; // Điểm đích
    public float jumpHeight = 2f; // Độ cao nhảy
    public float doubleSpeedDuration = 2f; // Thời gian tăng tốc độ
    public float normalSpeed = 3.5f; // Tốc độ bình thường

    private NavMeshAgent agent;
    private Vector3 startPoint;
    private float pathLength; // Tổng quãng đường
    private bool hasRandomizedAction = false; // Kiểm tra đã thực hiện hành động random chưa
    private State currentState = State.Moving;

    private enum State
    {
        Moving,
        Jumping,
        DoubleSpeed
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent không được tìm thấy!");
            return;
        }

        // Gán tốc độ mặc định và đặt điểm bắt đầu
        agent.speed = normalSpeed;
        startPoint = transform.position;
        pathLength = Vector3.Distance(startPoint, target.position);

        // Di chuyển tới mục tiêu
        agent.SetDestination(target.position);
    }

    void Update()
    {
        if (currentState == State.Moving)
        {
            Move();
        }
        else if (currentState == State.Jumping)
        {
            // Xử lý nhảy
        }
        else if (currentState == State.DoubleSpeed)
        {
            // Xử lý tăng tốc
        }
    }

    void Move()
    {
        if (target == null || agent == null) return;

        // Tính toán quãng đường đã di chuyển
        float distanceTravelled = Vector3.Distance(startPoint, transform.position);
        float progress = distanceTravelled / pathLength;

        // Kiểm tra đã đi được 1/3 quãng đường
        if (progress >= 0.33f && !hasRandomizedAction)
        {
            RandomizeAction();
            hasRandomizedAction = true;
        }
    }

    void RandomizeAction()
    {
        int action = Random.Range(0, 2); // Random 0 hoặc 1
        if (action == 0)
        {
            StartCoroutine(PerformJump());
        }
        else
        {
            StartCoroutine(PerformDoubleSpeed());
        }
    }

    System.Collections.IEnumerator PerformJump()
    {
        currentState = State.Jumping;

        // Lưu vị trí hiện tại
        Vector3 jumpTarget = transform.position;
        jumpTarget.y += jumpHeight;

        // Nhảy (thực hiện di chuyển lên và xuống)
        float elapsedTime = 0f;
        float jumpDuration = 0.5f; // Thời gian nhảy
        Vector3 originalPosition = transform.position;

        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPosition, jumpTarget, elapsedTime / jumpDuration);
            yield return null;
        }

        // Trở lại di chuyển bình thường
        currentState = State.Moving;
    }

    System.Collections.IEnumerator PerformDoubleSpeed()
    {
        currentState = State.DoubleSpeed;

        // Tăng gấp đôi tốc độ
        agent.speed = normalSpeed * 2f;
        yield return new WaitForSeconds(doubleSpeedDuration);

        // Trả tốc độ về bình thường
        agent.speed = normalSpeed;
        currentState = State.Moving;
    }
}
