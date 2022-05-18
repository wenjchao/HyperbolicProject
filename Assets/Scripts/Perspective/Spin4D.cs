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

    Vector4 current_pos;
    public Matrix4x4 Matx;
    public float SinSpeed;
    public float CosSpeed;

    void Start()
    {
        SinSpeed = Mathf.Sin( Mathf.PI*0.01f );
        CosSpeed = Mathf.Cos( Mathf.PI*0.01f );
        current_pos = new Vector4(5, 5, 5, 5);
        if ( intindex / 8 == 0 ) current_pos.x = -current_pos.x;
        if ( (intindex / 4) % 2 == 0) current_pos.y = -current_pos.y;
        if ( (intindex / 2) % 2 == 0) current_pos.z = -current_pos.z;
        if ( intindex % 2 == 0) current_pos.w = -current_pos.w;
        //edges: (1,2),(
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) press1 = true;
        else press1 = false;
        if (Input.GetKey(KeyCode.Alpha2)) press2 = true;
        else press2 = false;
        if (Input.GetKey(KeyCode.Alpha3)) press3 = true;
        else press3 = false;
        if (Input.GetKey(KeyCode.Alpha4)) press4 = true;
        else press4 = false;
        if (Input.GetKey(KeyCode.Alpha5)) press5 = true;
        else press5 = false;
        if (Input.GetKey(KeyCode.Alpha6)) press6 = true;
        else press6 = false;
    }

    void FixedUpdate() {

        if( press1 ) current_pos = Spin(1,SinSpeed,CosSpeed, current_pos);
        if( press2 ) current_pos = Spin(2,SinSpeed,CosSpeed, current_pos);
        if( press3 ) current_pos = Spin(3,SinSpeed,CosSpeed, current_pos);
        if( press4 ) current_pos = Spin(4,SinSpeed,CosSpeed, current_pos);
        if( press5 ) current_pos = Spin(5,SinSpeed,CosSpeed, current_pos);
        if( press6 ) current_pos = Spin(6,SinSpeed,CosSpeed, current_pos);

        Project( current_pos , 40 );
    }

    Vector4 Spin(int index,float sintheta, float costheta, Vector4 dot){

        //Rotation XY
        if(index == 1){
            Matx.SetColumn(0, new Vector4( costheta , sintheta , 0 , 0 ) );
            Matx.SetColumn(1, new Vector4( -sintheta , costheta , 0 , 0 ) );
            Matx.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Matx.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        //Rotation YZ
        if (index == 2){
            Matx.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Matx.SetColumn(1, new Vector4( 0 , costheta , sintheta , 0 ) );
            Matx.SetColumn(2, new Vector4( 0 , -sintheta , costheta , 0 ) );
            Matx.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        //Rotation XZ
        if (index == 3){
            Matx.SetColumn(0, new Vector4( costheta , 0 , -sintheta  , 0 ) );
            Matx.SetColumn(1, new Vector4( 0 , 1 , 0 , 0 ) );
            Matx.SetColumn(2, new Vector4( sintheta , 0 , costheta , 0 ) );
            Matx.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        //Rotation XW
        if (index == 4){
            Matx.SetColumn(0, new Vector4( costheta , 0 , 0 , sintheta ) );
            Matx.SetColumn(1, new Vector4( 0 , 1 , 0 , 0 ) );
            Matx.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Matx.SetColumn(3, new Vector4( -sintheta , 0 , 0 , costheta ) );
        }
        //Rotation YW
        if (index == 5){
            Matx.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Matx.SetColumn(1, new Vector4( 0 , costheta , 0 , -sintheta ) );
            Matx.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Matx.SetColumn(3, new Vector4( 0 , sintheta , 0 , costheta ) );
        }
        //Rotation ZW
        if (index == 6){
            Matx.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Matx.SetColumn(1, new Vector4( 0 , 1 , 0 , 0 ) );
            Matx.SetColumn(2, new Vector4( 0 , 0 , costheta , -sintheta ) );
            Matx.SetColumn(3, new Vector4( 0 , 0 , sintheta , costheta ) );
        }

        return Matx * dot;
    }

    //Project a dot onto a plane through one eye(projector)
    void Project(Vector4 dot, float projectorw){
        float dot2plane = Dot2PlaneLength( dot, new Vector4(0,0,0,1), Vector4.zero);
        float projector2plane = Dot2PlaneLength( new Vector4(0,0,0,projectorw) , new Vector4(0,0,0,1) , Vector4.zero );
        Vector4 Projection = projector2plane / ( projector2plane - dot2plane) * ( dot-new Vector4(0,0,0,projectorw) ) ;
        transform.position = new Vector3( Projection.x, Projection.y, Projection.z );
    }

    float Dot2PlaneLength(Vector4 dot, Vector4 axis, Vector4 dotonplane)
    {
        float temp = Vector4.Dot((dotonplane - dot), axis) / axis.magnitude;
        return temp;
    }
}
