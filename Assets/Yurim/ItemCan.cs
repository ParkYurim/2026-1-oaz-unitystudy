using UnityEngine;

public class ItemCan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotateSpeed;
    // AudioSource audio;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
         // 월드 좌표계 방향을 기준으로 회전 진행.
        // audio=GetComponent<AudioSource>();
    }
}
