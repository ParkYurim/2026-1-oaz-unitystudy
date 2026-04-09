using UnityEngine;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public TMPro.TMP_Text stageCountText; //스테이지에 있는 아이템
    public TMPro.TMP_Text playerCountText; //먹은 아이템   

     void Awake(){
        stageCountText.text="/ "+ totalItemCount.ToString();
    }
    public void GetItem(int count){
        playerCountText.text=count.ToString();
    }

}
