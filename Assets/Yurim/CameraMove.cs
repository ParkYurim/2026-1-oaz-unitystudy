using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;
    public float followspeed=5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // 플레이어의 위치를 따라가는 카메라.
    void LateUpdate(){
        Vector3 newPosition = playerTransform.position + Offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followspeed);
    }
    
    void Awake(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  
        //Find : 이름으로 찾기 , 지금 명령어는 tag로 찾는 것.
        Offset = transform.position - playerTransform.position;
    }
}
