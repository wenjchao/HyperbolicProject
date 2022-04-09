using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInMaze2 : MonoBehaviour
{
    public bool lft;
    public bool rght;
    public bool frwrd;
    public bool bckwrd;
    public int location;
    public int futureloc;
    public float distance;
    public float angel;
    public int[,,] MazeMatrix;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        location = 0;
        GetComponent<Transform>().rotation = GetComponent<Transform>().rotation * Quaternion.AngleAxis( 90 , Vector3.up );

        MazeMatrix = new int[6,4,6]
        {{ { 0,1,0,0,-1,5 },
        { 0,-1,0,0,1,4 },
        { 1,0,0,0,1,1 },
        { -1,0,0,0,0,0 } },

        { { 0,1,1,1,0,0 },
        { 0,-1,1,0,-1,2 },
        { 1,0,1,1,0,5 },
        { -1,0,1,0,0,0 } },

        { { 0,1,2,1,0,3 },
        { 0,-1,2,0,-1,1 },
        { 1,0,2,1,0,4 },
        { -1,0,2,0,0,0 } },

        { { 0,1,3,0,-1,4 },
        { 0,-1,3,0,1,5 },
        { 1,0,3,0,1,2 },
        { -1,0,3,0,0,0 } },

        { { 0,1,4,0,-1,0 },
        { 0,-1,4,0,1,3 },
        { 1,0,4,1,0,2 },
        { -1,0,4,0,0,0 } },

        { { 0,1,5,0,-1,3 },
        { 0,-1,5,0,1,0 },
        { 1,0,5,1,0,1 },
        { -1,0,5,0,0,0 } }};

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) lft = true;
        else lft = false;

        if (Input.GetKey(KeyCode.RightArrow)) rght = true;
        else rght = false;

        if (Input.GetKey(KeyCode.UpArrow)) frwrd = true;
        else frwrd = false;

        if (Input.GetKey(KeyCode.DownArrow)) bckwrd = true;
        else bckwrd = false;

        pos = GetComponent<Transform>().position;
        Direction();
    }
    private /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(rght) transform.Rotate(0, Time.deltaTime*60f, 0);
        if(lft) transform.Rotate(0, -Time.deltaTime*60f, 0);
        if (frwrd) transform.position += transform.forward * Time.deltaTime*3;
        if (bckwrd) transform.position -= transform.forward * Time.deltaTime*3;
    }
    void teleport(int Previous_x, int Previous_z, int Previous_loc, int Future_x, int Future_z, int Future_loc){
        //Debug.Log("teleport");
        location = Future_loc;
        
        if( Future_x == 0 && Previous_x ==0 ){
            GetComponent<Transform>().position = new Vector3( -pos.x * (Previous_x-Previous_z)*(Future_x-Future_z) , pos.y + 200*(Future_loc-Previous_loc) , 9.99f * Future_z );
        }
        if( Future_x == 0 && Previous_z ==0 ){
            GetComponent<Transform>().position = new Vector3( -pos.z * (Previous_x-Previous_z)*(Future_x-Future_z) , pos.y + 200*(Future_loc-Previous_loc) , 9.99f * Future_z );
        }
        if( Future_z == 0 && Previous_x ==0 ){
            GetComponent<Transform>().position = new Vector3( 9.99f * Future_x, pos.y + 200*(Future_loc-Previous_loc) , -pos.x * (Previous_x-Previous_z)*(Future_x-Future_z)  );
        }
        if( Future_z == 0 && Previous_z ==0 ){
            GetComponent<Transform>().position = new Vector3( 9.99f * Future_x , pos.y + 200*(Future_loc-Previous_loc) , -pos.z * (Previous_x-Previous_z)*(Future_x-Future_z) );
        }
        
        
        float angle = 45;
        if ( Previous_x * Future_x + Previous_z * Future_z == 1) angle = 180;
        else if ( Previous_x * Future_x + Previous_z * Future_z == -1) angle = 0;
        else if ( Previous_x * Future_z - Previous_z * Future_x == 1) angle = 90;
        else if ( Previous_z * Future_x - Previous_x * Future_z == 1) angle = -90;

        Quaternion rot1 = Quaternion.AngleAxis( angle , Vector3.up );
        GetComponent<Transform>().rotation = GetComponent<Transform>().rotation * rot1;
    }

    void Direction()
    {
        for (int i = 0 ; i < 6 ; i++ ){
            if (location == i) {
                EachLocation( i );
                break;
            }
        }
        if ( distance < 0 ) distance = 0;
    }

    void EachLocation( int Player_loc){
        
        if ( pos.z > 10.01 ) teleport( MazeMatrix[ Player_loc,0,0],MazeMatrix[ Player_loc,0,1],MazeMatrix[ Player_loc,0,2],MazeMatrix[ Player_loc,0,3],MazeMatrix[ Player_loc,0,4],MazeMatrix[ Player_loc,0,5] );
        else if ( pos.z <-10.01) teleport( MazeMatrix[ Player_loc,1,0],MazeMatrix[ Player_loc,1,1],MazeMatrix[ Player_loc,1,2],MazeMatrix[ Player_loc,1,3],MazeMatrix[ Player_loc,1,4],MazeMatrix[ Player_loc,1,5] );
        else if ( pos.x > 10.01) teleport( MazeMatrix[ Player_loc,2,0],MazeMatrix[ Player_loc,2,1],MazeMatrix[ Player_loc,2,2],MazeMatrix[ Player_loc,2,3],MazeMatrix[ Player_loc,2,4],MazeMatrix[ Player_loc,2,5] );
        
        if ( pos.z >= 1){
            futureloc = MazeMatrix[ Player_loc,0,5];
            distance = GetComponent<Transform>().position.z;
            angel = Vector3.SignedAngle( Vector3.forward , GetComponent<Transform>().forward , Vector3.up );
        }
        else if ( pos.z <= -1){
            futureloc = MazeMatrix[ Player_loc,1,5];
            distance = -1f * GetComponent<Transform>().position.z;
            angel = Vector3.SignedAngle( Vector3.back , GetComponent<Transform>().forward , Vector3.up );
        }

        else {
            futureloc = MazeMatrix[ Player_loc,2,5];
            distance = GetComponent<Transform>().position.x;
            angel = Vector3.SignedAngle( Vector3.right , GetComponent<Transform>().forward , Vector3.up );
        }

    }

}
