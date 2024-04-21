using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ca1 : MonoBehaviour
{
    public float explosionDuration = 0.5f; // Thời gian kéo dài của hiệu ứng nổ
    private Animator animator; // Animator của cá

    public Transform tamnhin; // lấy vị trí tầm nhìn raycast
    public LayerMask layerMask; //lấy lớp ca1 tương tác
    public float distanceThreshold = 5f; // Ngưỡng khoảng cách để tiến lại gần người chơi
    public float originalPositionX; // Vị trí ban đầu của cá

    private bool isChasing = false; // Biến kiểm tra xem cá đang đuổi theo nhân vật hay không
    public float rotationSpeed = 5f; // Tốc độ quay của con cá
    public float movementSpeed = 2f; // Tốc độ di chuyển của con cá
    public float acceleration = 0.1f; // Tốc độ tăng dần

    private float currentSpeed; // Tốc độ hiện tại của con cá
    private bool facingRight = true; // Biến theo dõi hướng di chuyển của con cá

    // Start được gọi trước khi khung hình đầu tiên được cập nhật
    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator của cá
        originalPositionX = transform.position.x; // Lưu lại vị trí ban đầu của cá
        currentSpeed = movementSpeed; // Khởi tạo tốc độ hiện tại bằng tốc độ ban đầu
    }

    // Update được gọi mỗi khung hình
    void Update()
    {
        RayTamNhin(); // Gọi hàm xử lý tầm nhìn mỗi khung hình
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("nv")) // Kiểm tra xem nhân vật có tag "Player" hay không
        {
            Explode(); // Gọi hàm tạo hiệu ứng nổ
        }
    }

    void Explode()
    {
        // Kích hoạt animation để chạy hiệu ứng nổ
        animator.SetTrigger("no");

        // Bắt đầu coroutine để phá hủy cá sau khi thời gian kéo dài của hiệu ứng nổ
        StartCoroutine(DestroyAfterDuration());
    }

    IEnumerator DestroyAfterDuration()
    {
        // Chờ đợi cho đến khi kết thúc thời gian kéo dài của hiệu ứng nổ
        yield return new WaitForSeconds(explosionDuration);

        // Phá hủy cá
        Destroy(gameObject);
    }

    public void RayTamNhin()
    {
        Collider2D[] hitca1 = Physics2D.OverlapCircleAll(tamnhin.position, 5f, layerMask);
        if (hitca1 != null) //chạm nhân vật
        {
            foreach (Collider2D collider in hitca1)
            {
                if (collider.CompareTag("nv"))
                {
                    Debug.DrawRay(tamnhin.position, (collider.transform.position - tamnhin.position).normalized * 5f, Color.red);
                    // Lấy khoảng cách từ cá đến người chơi
                    float distanceToPlayer = Vector2.Distance(transform.position, collider.transform.position);

                    // Nếu khoảng cách nhỏ hơn ngưỡng và có va chạm, tiến lại gần người chơi
                    if (distanceToPlayer < distanceThreshold)
                    {
                        Vector3 targetDirection = collider.transform.position - transform.position;
                        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                        // Di chuyển con cá về phía nhân vật với tốc độ hiện tại
                        transform.Translate(targetDirection.normalized * currentSpeed * Time.deltaTime, Space.World);

                        // Xác định hướng của con cá và áp dụng flip
                        if (targetDirection.x > 0 && !facingRight)
                        {
                            Flip();
                        }
                        else if (targetDirection.x < 0 && facingRight)
                        {
                            Flip();
                        }

                        isChasing = true; // Đặt cờ hiệu để biết rằng cá đang đuổi theo nhân vật
                        return;
                    }
                }
            }
        }

        // Giảm tốc độ dần dần khi không có va chạm
        currentSpeed -= acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, movementSpeed); // Giới hạn tốc độ tối thiểu
    }

    // Phương thức này thực hiện flip hình dạng của con cá
    private void Flip()
    {
        // Đảo ngược hướng của con cá
        facingRight = !facingRight;

        // Lấy scale hiện tại
        Vector3 scale = transform.localScale;

        // Đảo ngược scale theo trục X để flip hình dạng
        scale.x *= -1;

        // Cập nhật lại scale
        transform.localScale = scale;
    }
}
