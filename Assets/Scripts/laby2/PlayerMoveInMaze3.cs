using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInMaze3 : MonoBehaviour
{
    public bool lft;
    public bool rght;
    public bool frwrd;
    public bool bckwrd;
    public int location;
    public int futureloc;
    public float distance;
    public float angel;
    public float[,] BigMatrix;
    // Start is called before the first frame update
    void Start()
    {
        location = 0;
        BigMatrix = new float[6,3] {{1,1,2},{3,4,5},{1,2,4},{5,6,7},{1,3,4},{5,6,7}};
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
        Direction();
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
        //Debug.Log("teleport");
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

    void Direction()
    {
        if (location == 0){
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,0,0,1,1);
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,0,0,-1,5);
            if (GetComponent<Transform>().position.z <-10.01) teleport(0,-1,0,0,1,4);
            if (GetComponent<Transform>().position.z >= 1){
                futureloc = 5;
                distance = GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.forward , GetComponent<Transform>().forward , Vector3.up );
            }
            else if (GetComponent<Transform>().position.z <= -1){
                futureloc = 4;
                distance = -1f * GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.back , GetComponent<Transform>().forward , Vector3.up );
            }

            else {
                futureloc = 1;
                distance = GetComponent<Transform>().position.x;
                angel = Vector3.SignedAngle( Vector3.right , GetComponent<Transform>().forward , Vector3.up );
            }
        }
        if (location == 1){
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,1,1,0,5);
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,1,1,0,0);
            if (GetComponent<Transform>().position.z <-10.01) teleport(0,-1,1,0,-1,2);
            if (GetComponent<Transform>().position.z >= 1){
                futureloc = 0;
                distance = GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.forward , GetComponent<Transform>().forward , Vector3.up );
            }
            else if (GetComponent<Transform>().position.z <= -1){
                futureloc = 2;
                distance = -1f * GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.back , GetComponent<Transform>().forward , Vector3.up );
            }
            else {
                futureloc = 5;
                distance = GetComponent<Transform>().position.x;
                angel = Vector3.SignedAngle( Vector3.right , GetComponent<Transform>().forward , Vector3.up );
            }
        }
        if (location == 2){
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,2,1,0,4);
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,2,1,0,3);
            if (GetComponent<Transform>().position.z < -10.01) teleport(0,-1,2,0,-1,1);
            if (GetComponent<Transform>().position.z >= 1){
                futureloc = 3;
                distance = GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.forward , GetComponent<Transform>().forward , Vector3.up );
            }
            else if (GetComponent<Transform>().position.z <= -1){
                futureloc = 1;
                distance = -1f * GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.back , GetComponent<Transform>().forward , Vector3.up );
            }
            else {
                futureloc = 4;
                distance = GetComponent<Transform>().position.x;
                angel = Vector3.SignedAngle( Vector3.right , GetComponent<Transform>().forward , Vector3.up );
            }
        }
        if (location == 3){
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,3,0,-1,4);
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,3,0,1,2);
            if (GetComponent<Transform>().position.z < -10.01) teleport(0,-1,3,0,1,5);
            if (GetComponent<Transform>().position.z >= 1 ){
                futureloc = 4;
                distance = GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.forward , GetComponent<Transform>().forward , Vector3.up );
            }
            else if (GetComponent<Transform>().position.z <= -1){
                futureloc = 5;
                distance = -1f * GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.back , GetComponent<Transform>().forward , Vector3.up );
            }
            else {
                futureloc = 2;
                distance = GetComponent<Transform>().position.x;
                angel = Vector3.SignedAngle( Vector3.right , GetComponent<Transform>().forward , Vector3.up );
            }
        }
        if (location == 4){
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,4,0,-1,0);
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,4,1,0,2);
            if (GetComponent<Transform>().position.z < -10.01) teleport(0,-1,4,0,1,3);
            if (GetComponent<Transform>().position.z >= 1 ){
                futureloc = 0;
                distance = GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.forward , GetComponent<Transform>().forward , Vector3.up );
            }
            else if (GetComponent<Transform>().position.z <= -1){
                futureloc = 3;
                distance = -1f * GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.back , GetComponent<Transform>().forward , Vector3.up );
            }
            else{
                futureloc = 2;
                distance = GetComponent<Transform>().position.x;
                angel = Vector3.SignedAngle( Vector3.right , GetComponent<Transform>().forward , Vector3.up );
            }
        }
        if (location == 5){
            if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,5,0,-1,3);
            if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,5,1,0,1);
            if (GetComponent<Transform>().position.z < -10.01) teleport(0,-1,5,0,1,0);
            if (GetComponent<Transform>().position.z >= 1 ){
                futureloc = 3;
                distance = GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.forward , GetComponent<Transform>().forward , Vector3.up );
            }
            else if (GetComponent<Transform>().position.z <= -1){
                futureloc = 0;
                distance = -1f * GetComponent<Transform>().position.z;
                angel = Vector3.SignedAngle( Vector3.back , GetComponent<Transform>().forward , Vector3.up );
            }
            else{
                futureloc = 1;
                distance = GetComponent<Transform>().position.x;
                angel = Vector3.SignedAngle( Vector3.right , GetComponent<Transform>().forward , Vector3.up );
            }
        }

        if (distance<0) distance = 0;
    }

    void EachLocation(){
        
        if (GetComponent<Transform>().position.z > 10.01) teleport(0,1,0,0,-1,5);
        if (GetComponent<Transform>().position.z <-10.01) teleport(0,-1,0,0,1,4);
        if (GetComponent<Transform>().position.x > 10.01) teleport(1,0,0,0,1,1);

        if (GetComponent<Transform>().position.z >= 1){
            futureloc = 5;
            distance = GetComponent<Transform>().position.z;
            angel = Vector3.SignedAngle( Vector3.forward , GetComponent<Transform>().forward , Vector3.up );
        }
        else if (GetComponent<Transform>().position.z <= -1){
            futureloc = 4;
            distance = -1f * GetComponent<Transform>().position.z;
            angel = Vector3.SignedAngle( Vector3.back , GetComponent<Transform>().forward , Vector3.up );
        }
        else {
            futureloc = 1;
            distance = GetComponent<Transform>().position.x;
            angel = Vector3.SignedAngle( Vector3.right , GetComponent<Transform>().forward , Vector3.up );
        }

    }
}
