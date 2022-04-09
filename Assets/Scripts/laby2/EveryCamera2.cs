using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryCamera2 : MonoBehaviour
{
    public GameObject Player;
    public int Cameraloc;
    public int Playerloc;
    public Camera cam;
    //Quaternion rot2;
    // Start is called before the first frame update
    void Start()
    {
        //rot2 = Quaternion.AngleAxis( -15 , Vector3.right );
    }

    // Update is called once per frame
    void Update()
    {
        Playerloc = Player.GetComponent<PlayerMoveInMaze2>().location;

        if (Cameraloc == Playerloc){
            GetComponent<Transform>().position = new Vector3(Player.GetComponent<Transform>().position.x ,Player.GetComponent<Transform>().position.y + 0.2f, Player.GetComponent<Transform>().position.z);
            GetComponent<Transform>().rotation = Player.GetComponent<Transform>().rotation;
            cam.depth = 5;
        }

        else if (Playerloc == 0 && Cameraloc == 1) CameraReflex(1,0,0,0,1,1);
        else if (Playerloc == 0 && Cameraloc == 5) CameraReflex(0,1,0,0,-1,5);
        else if (Playerloc == 0 && Cameraloc == 4) CameraReflex(0,-1,0,0,1,4);
        else if (Playerloc == 1 && Cameraloc == 5) CameraReflex(1,0,1,1,0,5);
        else if (Playerloc == 1 && Cameraloc == 0) CameraReflex(0,1,1,1,0,0);
        else if (Playerloc == 1 && Cameraloc == 2) CameraReflex(0,-1,1,0,-1,2);
        else if (Playerloc == 2 && Cameraloc == 4) CameraReflex(1,0,2,1,0,4);
        else if (Playerloc == 2 && Cameraloc == 3) CameraReflex(0,1,2,1,0,3);
        else if (Playerloc == 2 && Cameraloc == 1) CameraReflex(0,-1,2,0,-1,1);
        else if (Playerloc == 3 && Cameraloc == 4) CameraReflex(0,1,3,0,-1,4);
        else if (Playerloc == 3 && Cameraloc == 2) CameraReflex(1,0,3,0,1,2);
        else if (Playerloc == 3 && Cameraloc == 5) CameraReflex(0,-1,3,0,1,5);
        else if (Playerloc == 4 && Cameraloc == 0) CameraReflex(0,1,4,0,-1,0);
        else if (Playerloc == 4 && Cameraloc == 2) CameraReflex(1,0,4,1,0,2);
        else if (Playerloc == 4 && Cameraloc == 3) CameraReflex(0,-1,4,0,1,3);
        else if (Playerloc == 5 && Cameraloc == 3) CameraReflex(0,1,5,0,-1,3);
        else if (Playerloc == 5 && Cameraloc == 1) CameraReflex(1,0,5,1,0,1);
        else if (Playerloc == 5 && Cameraloc == 0) CameraReflex(0,-1,5,0,1,0);



        
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
            GetComponent<Transform>().position = new Vector3( -pos.x *( (Player_x-Player_z)*(Camera_x-Camera_z) ) , pos.y + 200*(Camera_loc-Player_loc) + 0.2f , (20 - pos.z*Player_z)*Camera_z );
        }
        else if (Camera_x == 0 && Player_z  == 0){
            GetComponent<Transform>().position = new Vector3( -pos.z *( (Player_x-Player_z)*(Camera_x-Camera_z) ) , pos.y + 200*(Camera_loc-Player_loc) + 0.2f , (20 - pos.x*Player_x)*Camera_z );
        }
        else if (Camera_z == 0 && Player_x  == 0){
            GetComponent<Transform>().position = new Vector3( (20 - pos.z*Player_z)*Camera_x , pos.y + 200*(Camera_loc-Player_loc) + 0.2f , -pos.x *( (Player_x-Player_z)*(Camera_x-Camera_z) ) );
        }
        else if (Camera_z == 0 && Player_z  == 0){
            GetComponent<Transform>().position = new Vector3( (20 - pos.x*Player_x)*Camera_x , pos.y + 200*(Camera_loc-Player_loc) + 0.2f , -pos.z *( (Player_x-Player_z)*(Camera_x-Camera_z) ) );
        }
        
        GetComponent<Transform>().rotation = Player.GetComponent<Transform>().rotation * rot1;
        cam.depth = 2;
    }
}
