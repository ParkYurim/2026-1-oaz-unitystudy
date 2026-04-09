using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionTest : MonoBehaviour
{
    // 인스펙터 창에서 우리가 만든 액션을 연결해줍니다.
    public InputAction moveAction; 

    void OnEnable()
    {
        moveAction.Enable(); // 액션 활성화
    }

    void OnDisable()
    {
        moveAction.Disable(); // 액션 비활성화
    }

    void Update()
    {
        // 이렇게 한 줄로 과거의 GetAxisRaw와 똑같은 -1, 0, 1 값을 얻을 수 있습니다!
        float horizontalRaw = moveAction.ReadValue<float>();
        
        Debug.Log("현재 방향 값: " + horizontalRaw);
    }
}