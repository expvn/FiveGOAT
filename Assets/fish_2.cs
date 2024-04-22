using UnityEngine;

public class fish_2 : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float speed;
    public float distanceBetween;
    public float bubbleSpawnInterval = 5f; // Khoảng thời gian giữa mỗi lần sinh bong bóng
    private float distance;
    private GameObject player; // Biến tham chiếu đến người chơi.

    void Start()
    {
        // Gọi hàm sinh bong bóng sau mỗi khoảng thời gian cố định
        InvokeRepeating("SpawnBubble", bubbleSpawnInterval, bubbleSpawnInterval);
    }

    void Update()
    {
        // Timf kieems nguoi chơi trong mỗi frame.
        FindPlayer();

        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance < distanceBetween)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                // Xoay cá dựa trên hướng di chuyển
                if (direction.x < 0) // Xoay qua trai
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0); // Xoay 180 độ quanh trục Y
                }
                else if (direction.x > 0) // Moving phai
                {
                    transform.rotation = Quaternion.identity; // Đặt lại vòng quay
                }
            }
        }
    }

    // Hàm sinh bong bóng
    void SpawnBubble()
    {
        Instantiate(bubblePrefab, transform.position, Quaternion.identity);
    }

    // Ham de tim kiem nguoi choi
    void FindPlayer() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distanceBetween);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player = collider.gameObject;
                return;
            }
        }
        player = null; // Set player thanh null neu k tim thay nguoi choi trong pham vi
    }
}
