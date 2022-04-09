using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInMaze : MonoBehaviour
{
    public bool lft;
    public bool rght;
    public bool frwrd;
    public bool bckwrd;
    public int location;
    public int futureloc;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        location = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)){
            lft = true;
        }
        else{
            lft = false;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            rght = true;
        }
        else{
            rght = false;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            frwrd = true;
        }
        else{
            frwrd = false;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            bckwrd = true;
        }
        else{
            bckwrd = false;
        }
        /*
        if (location == 0){
            if (GetComponent<Transform>().position.x > 10.01){
                //teleport(1,0,0,1,0,1);
                
                location = 1;
                GetComponent<Transform>().position = new Vector3(9.99f ,GetComponent<Transform>().position.y + 200, -GetComponent<Transform>().position.z);
                Quaternion rot1 = Quaternion.AngleAxis( 180 , Vector3.up );
                GetComponent<Transform>().rotation = GetComponent<Transform>().rotation * rot1;
                
            }
            if (GetComponent<Transform>().position.z > 10.01){
                location = 2;
                GetComponent<Transform>().position = new Vector3( -GetComponent<Transform>().position.x ,GetComponent<Transform>().position.y + 400,  9.99f );
                Quaternion rot1 = Quaternion.AngleAxis( 180 , Vector3.up );
                GetComponent<Transform>().rotation = GetComponent<Transform>().rotation * rot1;
            }
        }
        if (location == 1){
            if (GetComponent<Transform>().position.x > 10.01){
                location = 0;
                GetComponent<Transform>().position = new Vector3(9.99f ,GetComponent<Transform>().position.y - 200, -GetComponent<Transform>().position.z);
                Quaternion rot1 = Quaternion.AngleAxis( 180 , Vector3.up );
                GetComponent<Transform>().rotation = GetComponent<Transform>().rotation * rot1;
            }
            if (GetComponent<Transform>().position.z > 10.01){
                location = 2;
                GetComponent<Transform>().position = new Vector3(9.99f ,GetComponent<Transform>().position.y + 200, GetComponent<Transform>().position.x);
                Quaternion rot1 = Quaternion.AngleAxis( -90 , Vector3.up );
                GetComponent<Transform>().rotation = GetComponent<Transform>().rotation * rot1;
            }
        }
        if (location == 2){
            if (GetComponent<Transform>().position.z > 10.01){
                location = 0;
                GetComponent<Transform>().position = new Vector3( -GetComponent<Transform>().position.x ,GetComponent<Transform>().position.y - 400,  9.99f );
                Quaternion rot1 = Quaternion.AngleAxis( 180 , Vector3.up );
                GetComponent<Transform>().rotation = GetComponent<Transform>().rotation * rot1;
            }
        
            if (GetComponent<Transform>().position.x > 10.01){
                location = 1;
                GetComponent<Transform>().position = new Vector3( GetComponent<Transform>().position.z,GetComponent<Transform>().position.y - 200, 9.9f );
                Quaternion rot1 = Quaternion.AngleAxis( 90 , Vector3.up );
                GetComponent<Transform>().rotation = GetComponent<Transform>().rotation * rot1;
            }
        }
        */
        if (location == 0){
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,0,1,0,1);
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,0,0,1,2);
            if (GetComponent<Transform>().position.x > GetComponent<Transform>().position.z ){
                futureloc = 1;
                distance = GetComponent<Transform>().position.x;
                }
            if (GetComponent<Transform>().position.x <= GetComponent<Transform>().position.z ){
                futureloc = 2;
                distance = GetComponent<Transform>().position.z;
                }
        }
        if (location == 1){
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,1,1,0,0);
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,1,1,0,2);
            if (GetComponent<Transform>().position.x > GetComponent<Transform>().position.z ){
                futureloc = 0;
                distance = GetComponent<Transform>().position.x;
                }
            if (GetComponent<Transform>().position.x <= GetComponent<Transform>().position.z ){
                futureloc = 2;
                distance = GetComponent<Transform>().position.z;
                }
        }
        if (location == 2){
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,2,0,1,1);
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,2,0,1,0);
            if (GetComponent<Transform>().position.x > GetComponent<Transform>().position.z ) {
                futureloc = 1;
                distance = GetComponent<Transform>().position.x;
                }
            if (GetComponent<Transform>().position.x <= GetComponent<Transform>().position.z ) {
                futureloc = 0;
                distance = GetComponent<Transform>().position.z;
                }
        }
    }
    private /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(rght){
            transform.Rotate(0, Time.deltaTime*60f, 0);
        }
        if(lft){
            transform.Rotate(0, -Time.deltaTime*60f, 0);
        }
        if (frwrd){
            transform.position += transform.forward * Time.deltaTime*3;
        }
        if (bckwrd){
            transform.position -= transform.forward * Time.deltaTime*3;
        }
    }
    void teleport(int Previous_x, int Previous_z, int Previous_loc, int Future_x, int Future_z, int Future_loc){
        Debug.Log("teleport");
        location = Future_loc;
        Vector3 pos = GetComponent<Transform>().position;
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
}
