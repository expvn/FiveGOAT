using UnityEngine;

public class fish_2 : MonoBehaviour
{
    public GameObject player;
    public GameObject bubblePrefab;
    public float speed;
    public float distanceBetween;
    public float bubbleSpawnInterval = 5f; // Khoảng thời gian giữa mỗi lần sinh bong bóng
    private float distance;

    void Start()
    {
        // Gọi hàm sinh bong bóng sau mỗi khoảng thời gian cố định
        InvokeRepeating("SpawnBubble", bubbleSpawnInterval, bubbleSpawnInterval);
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.identity;
        }
    }

    // Hàm sinh bong bóng
    void SpawnBubble()
    {
        Instantiate(bubblePrefab, transform.position, Quaternion.identity);
    }
}
