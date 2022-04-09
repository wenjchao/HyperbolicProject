using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForBlue : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Player.GetComponent<Transform>().position;
        Quaternion rot1 = Quaternion.AngleAxis( 180 , Vector3.up );
        Quaternion rot2 = Quaternion.AngleAxis( 20 , Vector3.right );
        
        
        GetComponent<Transform>().position = new Vector3( 20 - pos.x , 5 + pos.y , 0 - pos.z );
        GetComponent<Transform>().rotation = Player.GetComponent<Transform>().rotation * rot1*rot2;
    }
}
