using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    [Tooltip("플레이어의 이동 속도를 조절합니다.")]
    public float moveSpeed = 5.0f;

    [Header("점프 설정")]
    [Tooltip("플레이어가 점프하는 힘을 조절합니다.")]
    public float jumpForce = 5.0f;

    // 무한 점프를 막기 위해 바닥에 닿아있는지 확인하는 변수
    private bool isGrounded = true;

    // 물리 효과를 적용하기 위한 컴포넌트 변수
    private Rigidbody rb;

    void Start()
    {
        // 게임 시작 시, 이 오브젝트에 붙어있는 Rigidbody 컴포넌트를 가져와 연결합니다.
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. 이동 처리 (기존 코드)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 2. 점프 처리 (추가된 코드)
        // 스페이스바(Jump)를 눌렀고, 동시에 바닥에 닿아있을 때(isGrounded가 true)만 점프 실행
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // 위쪽(Vector3.up) 방향으로 순간적인 힘(Impulse)을 가합니다.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // 점프를 했으므로 바닥에서 떨어진 상태(false)로 변경합니다.
            isGrounded = false;
        }
    }

    // 3. 바닥 감지 (착지 확인)
    // 플레이어가 다른 오브젝트와 충돌했을 때 자동으로 실행되는 유니티 내장 함수입니다.
    private void OnCollisionEnter(Collision collision)
    {
        // 부딪힌 대상의 태그(Tag)가 "Ground"라면 바닥에 착지한 것으로 간주합니다.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 다시 점프할 수 있는 상태로 변경
        }
    }
}