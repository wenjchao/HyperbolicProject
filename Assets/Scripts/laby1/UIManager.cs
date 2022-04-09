using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public string UI_GamePanel_Root = "Prefabs/GamePanel/";

    public GameObject m_CanvasRoot ;
    public Dictionary<string, GameObject> m_PanelList = new Dictionary<string, GameObject>();

    bool CheckCanvasRootIsNull(){
        if(m_CanvasRoot==null){
            Debug.LogError("Root is null, please add UIRootHandler.cs");
            return true;
        }
        return false;

    }

    bool IsPanelLive(){
        return false;
    }

    public GameObject ShowPanel(){
        return null;

    }

    public void TogglePanel(){

    }

    public void ClosePanel(){

    }

    public void CloseallPanel(){

    }

    public Vector2 GetCanvasSize(){
        return Vector2.one;
    }
    
}
