using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed=10f;

    [Header("점프 높이")]
    public float jumpPower=10;
    [Header("점프 여부-raycast로 판단")]
    public float rayDistance=1.1f; // 플레이어의 중심으로부터 바닥까지의 거리
    public LayerMask floorLayer; // 바닥 레이어를 지정하는 LayerMask
    private Rigidbody rigid;
    public int itemCount;
    AudioSource audio;
    public GameManagerLogic manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // 게임이 시작할 때 호출되는 함수(1회)
    void Start()
    {
        rigid=GetComponent<Rigidbody>();
        audio=GetComponent<AudioSource>();
    }

    // 매 프레임마다 호출되는 함수

    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame&&isGrounded()){
            // 플레이어의 중심에서 아래로 ray를 쏴서 바닥과 충돌하는지 확인
            
            rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, 0, rigid.linearVelocity.z); //점프하기 전에 y축 속도를 초기화
            rigid.AddForce(Vector3.up*jumpPower, ForceMode.Impulse);
            
        }
    }

    // 고정된 프레임마다 호출되는 함수, 물리 연산과 관련된 코드는 이 함수에서 작성하는 것이 좋다.
    void FixedUpdate()
    {
        float h=Keyboard.current.dKey.isPressed ? 1f : Keyboard.current.aKey.isPressed ? -1f : 0f;
        float v=Keyboard.current.wKey.isPressed ? 1f : Keyboard.current.sKey.isPressed ? -1f : 0f;
        Vector3 force = new Vector3(h,0,v);
        rigid.AddForce(force, ForceMode.Impulse);
        if(rigid.linearVelocity.magnitude > maxSpeed){
            rigid.linearVelocity = rigid.linearVelocity.normalized * maxSpeed;
            //rigid.velocity.normalized는 방향을 유지한 채로 속도만 조절하도록 하는 명령어
        }
    }
    // 트리거가 발생했을 때 호출되는 함수
    void OnTriggerEnter(Collider other){
        if(other.tag=="Item"){
            itemCount++;
            // AudioSource audio = other.GetComponent<AudioSource>();
            audio.Play();
            other.gameObject.SetActive(false);    //오브젝트 활성화 함수
            manager.GetItem(itemCount);
        }
        else if(other.tag=="Finish"){
            if(itemCount == manager.totalItemCount){
                SceneManager.LoadScene("ClearScene");
            }
        }
        else if(other.tag=="Trap"){
            SceneManager.LoadScene("GameOverScene");
        }
    }

    bool isGrounded(){
        return Physics.Raycast(transform.position, Vector3.down, rayDistance, floorLayer);
    }

    // 유니티 씬(Scene) 뷰에서 레이저의 길이를 눈으로 확인하기 위한 기즈모 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * rayDistance);
    }
}
