using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryCamera : MonoBehaviour
{
    public GameObject Player;
    public int Cameraloc;
    public int Playerloc;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Playerloc = Player.GetComponent<PlayerMoveInMaze>().location;
        if (Playerloc == 0 && Cameraloc == 1) CameraReflex(1,0,0,1,0,1);
        else if (Playerloc == 0 && Cameraloc == 2) CameraReflex(0,1,0,0,1,2);
        else if (Playerloc == 1 && Cameraloc == 0) CameraReflex(1,0,1,1,0,0);
        else if (Playerloc == 1 && Cameraloc == 2) CameraReflex(0,1,1,1,0,2);
        else if (Playerloc == 2 && Cameraloc == 0) CameraReflex(0,1,2,0,1,0);
        else if (Playerloc == 2 && Cameraloc == 1) CameraReflex(1,0,2,0,1,1);

        else if (Cameraloc == Playerloc){
            GetComponent<Transform>().position = new Vector3(Player.GetComponent<Transform>().position.x ,Player.GetComponent<Transform>().position.y, Player.GetComponent<Transform>().position.z);
            GetComponent<Transform>().rotation = Player.GetComponent<Transform>().rotation;
            cam.depth = 3;
        }
        else if(Cameraloc != Playerloc){
            cam.depth = 0;
        }

    }

    void CameraReflex( int Player_x , int Player_z , int Player_loc , int Camera_x , int Camera_z, int Camera_loc ){
        Vector3 pos = Player.GetComponent<Transform>().position;
        float angle = 45;
        if ( Player_x * Camera_x + Player_z * Camera_z == 1) angle = 180;
        else if ( Player_x * Camera_x + Player_z * Camera_z == -1) angle = 0;
        else if ( Player_x * Camera_z - Player_z * Camera_x == 1) angle = 90;
        else if ( Player_z * Camera_x - Player_x * Camera_z == 1) angle = -90;

        Quaternion rot1 = Quaternion.AngleAxis( angle , Vector3.up );

        if (Camera_x == 0 && Player_x == 0){
            GetComponent<Transform>().position = new Vector3( -pos.x *( (Player_x-Player_z)*(Camera_x-Camera_z) ) , pos.y + 200*(Camera_loc-Player_loc) , 20 - pos.z*Player_z );
        }
        else if (Camera_x == 0 && Player_z  == 0){
            GetComponent<Transform>().position = new Vector3( -pos.z *( (Player_x-Player_z)*(Camera_x-Camera_z) ) , pos.y + 200*(Camera_loc-Player_loc) , 20 - pos.x*Player_x );
        }
        else if (Camera_z == 0 && Player_x  == 0){
            GetComponent<Transform>().position = new Vector3( 20 - pos.z*Player_z , pos.y + 200*(Camera_loc-Player_loc) , -pos.x *( (Player_x-Player_z)*(Camera_x-Camera_z) ) );
        }
        else if (Camera_z == 0 && Player_z  == 0){
            GetComponent<Transform>().position = new Vector3( 20 - pos.x*Player_x , pos.y + 200*(Camera_loc-Player_loc) , -pos.z *( (Player_x-Player_z)*(Camera_x-Camera_z) ) );
        }
        
        GetComponent<Transform>().rotation = Player.GetComponent<Transform>().rotation * rot1;
        cam.depth = 2;
    }
}
