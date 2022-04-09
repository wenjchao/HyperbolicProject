using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin4D : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int intindex;
    public bool press1;
    public bool press2;
    public bool press3;
    public bool press4;
    public bool press5;
    public bool press6;
    public float W;
    public float X;
    public float Y;
    public float Z;
    public Matrix4x4 Matx;
    public float SinSpeed;
    public float CosSpeed;
    public Vector4 OutAxis;
    void Start()
    {
        SinSpeed = Mathf.Sin( Mathf.PI*0.01f );
        CosSpeed = Mathf.Cos( Mathf.PI*0.01f );
        W=5;
        X=5;
        Y=5;
        Z=5;
        if (intindex == 1){
        }
        if (intindex == 2){
            W = -W;
        }
        if (intindex == 3){
            X = -X;
        }
        if (intindex == 4){
            Y = -Y;
        }
        if (intindex == 5){
            Z = -Z;
        }
        if (intindex == 6){
            W = -W;
            X = -X;
        }
        if (intindex == 7){
            W = -W;
            Y = -Y;
        }
        if (intindex == 8){
            W = -W;
            Z = -Z;
        }
        if (intindex == 9){
            X = -X;
            Y = -Y;
        }
        if (intindex == 10){
            Y = -Y;
            Z = -Z;
        }
        if (intindex == 11){
            X = -X;
            Z = -Z;
        }
        if (intindex == 12){
            W = -W;
            X = -X;
            Y = -Y;
        }
        if (intindex == 13){
            W = -W;
            X = -X;
            Z = -Z;
        }
        if (intindex == 14){
            W = -W;
            Z = -Z;
            Y = -Y;
        }
        if (intindex == 15){
            Z = -Z;
            X = -X;
            Y = -Y;
        }
        if (intindex == 16){
            W = -W;
            Z = -Z;
            X = -X;
            Y = -Y;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.Alpha1)){
            press1 = true;
        }
        else{
            press1 = false;
        }
        if (Input.GetKey(KeyCode.Alpha2)){
            press2 = true;
        }
        else{
            press2 = false;
        }
        if (Input.GetKey(KeyCode.Alpha3)){
            press3 = true;
        }
        else{
            press3 = false;
        }
        if (Input.GetKey(KeyCode.Alpha4)){
            press4 = true;
        }
        else{
            press4 = false;
        }
        if (Input.GetKey(KeyCode.Alpha5)){
            press5 = true;
        }
        else{
            press5 = false;
        } 
        if (Input.GetKey(KeyCode.Alpha6)){
            press6 = true;
        }
        else{
            press6 = false;
        } 
    }
    void FixedUpdate() {
        
        if( press1 ) Spin(1,SinSpeed,CosSpeed,W,X,Y,Z);
        if( press2 ) Spin(2,SinSpeed,CosSpeed,W,X,Y,Z);
        if( press3 ) Spin(3,SinSpeed,CosSpeed,W,X,Y,Z);
        if( press4 ) Spin(4,SinSpeed,CosSpeed,W,X,Y,Z);
        if( press5 ) Spin(5,SinSpeed,CosSpeed,W,X,Y,Z);
        if( press6 ) Spin(6,SinSpeed,CosSpeed,W,X,Y,Z);

        Project(W,X,Y,Z,40);

    }

    void Spin(int index,float sintheta, float costheta, float dotw, float dotx, float doty, float dotz){
        
        if(index == 1){
            Matx.SetColumn(0, new Vector4( costheta , sintheta , 0 , 0 ) );
            Matx.SetColumn(1, new Vector4( -sintheta , costheta , 0 , 0 ) );
            Matx.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Matx.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        if(index == 2){
            Matx.SetColumn(0, new Vector4( 1 , 0 , 0  , 0 ) );
            Matx.SetColumn(1, new Vector4(  0 , costheta , sintheta , 0 ) );
            Matx.SetColumn(2, new Vector4( 0 , -sintheta , costheta , 0 ) );
            Matx.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        if(index == 3){
            Matx.SetColumn(0, new Vector4( costheta , 0 , -sintheta  , 0 ) );
            Matx.SetColumn(1, new Vector4(  0 , 1 , 0 , 0 ) );
            Matx.SetColumn(2, new Vector4( sintheta , 0 , costheta , 0 ) );
            Matx.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        if(index == 4){
            Matx.SetColumn(0, new Vector4( costheta , 0 , 0 , sintheta ) );
            Matx.SetColumn(1, new Vector4(  0 , 1 , 0 , 0 ) );
            Matx.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Matx.SetColumn(3, new Vector4( -sintheta , 0 , 0 , costheta ) );
        }
        if(index == 5){
            Matx.SetColumn(0, new Vector4( 1 , 0 , 0  , 0 ) );
            Matx.SetColumn(1, new Vector4(  0 , costheta , 0 , -sintheta ) );
            Matx.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Matx.SetColumn(3, new Vector4( 0 , sintheta , 0 , costheta ) );
        }
        if(index == 6){
            Matx.SetColumn(0, new Vector4( 1 , 0 , 0  , 0 ) );
            Matx.SetColumn(1, new Vector4(  0 , 1 , 0 , 0 ) );
            Matx.SetColumn(2, new Vector4( 0 , 0 , costheta , -sintheta ) );
            Matx.SetColumn(3, new Vector4( 0 , 0 , sintheta , costheta ) );
        }

        OutAxis = Matx * new Vector4(dotw,dotx,doty,dotz);
        W=OutAxis.x;
        X=OutAxis.y;
        Y=OutAxis.z;
        Z=OutAxis.w;
    }
    void Project(float dotw, float dotx, float doty, float dotz, float projectorw){
        float dot2plane = Dot2PlaneLength(dotw, dotx, doty, dotz, 1, 0, 0, 0, 0, 0, 0, 0);
        float projector2plane = Dot2PlaneLength(projectorw,0,0,0 , 1,0,0,0,0,0,0,0);
        
        OutAxis.w = dot2plane / ( projector2plane - dot2plane ) * ( dotw - projectorw ) + dotw ;
        OutAxis.x = dot2plane / ( projector2plane - dot2plane ) * ( dotx ) + dotx ;
        OutAxis.y = dot2plane / ( projector2plane - dot2plane ) * ( doty ) + doty ;
        OutAxis.z = dot2plane / ( projector2plane - dot2plane ) * ( dotz ) + dotz ;
        //Debug.Log(OutAxis);
        transform.position = new Vector3(OutAxis.x,OutAxis.y,OutAxis.z);
    }

    float Dot2PlaneLength(float dotw, float dotx, float doty, float dotz, float axisw, float axisx, float axisy, float axisz, float dotonplanew, float dotonplanex, float dotonplaney, float dotonplanez ){
        float temp = ((dotonplanew-dotw)*axisw + (dotonplanex-dotx)*axisx +  (dotonplaney-doty)* axisy+ (dotonplanez-dotz)* axisz) / Mathf.Sqrt(axisw*axisw + axisx*axisx + axisy*axisy+axisz*axisz) ;
        return temp;
    }
    
}
