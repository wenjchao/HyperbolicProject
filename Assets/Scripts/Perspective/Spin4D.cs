using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin4D : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int intindex;

    public bool[] press;
    Vector4 current_pos;
    public Matrix4x4 Matx;
    public float SinSpeed;
    public float CosSpeed;

    void Start()
    {
        press = new bool[7];
        intindex = Findnumber(GetComponent<Transform>().name);
        SinSpeed = Mathf.Sin( Mathf.PI*0.01f );
        CosSpeed = Mathf.Cos( Mathf.PI*0.01f );
        Initialpos();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) press[1] = true;
        else press[1] = false;
        if (Input.GetKey(KeyCode.Alpha2)) press[2] = true;
        else press[2] = false;
        if (Input.GetKey(KeyCode.Alpha3)) press[3] = true;
        else press[3] = false;
        if (Input.GetKey(KeyCode.Alpha4)) press[4] = true;
        else press[4] = false;
        if (Input.GetKey(KeyCode.Alpha5)) press[5] = true;
        else press[5] = false;
        if (Input.GetKey(KeyCode.Alpha6)) press[6] = true;
        else press[6] = false;
    }

    void FixedUpdate() {
        for (int i = 1; i <= 6; i++) if (press[i]) current_pos = Spin(i, SinSpeed, CosSpeed, current_pos);
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
        Vector4 Projection = projector2plane / ( projector2plane - dot2plane) * ( dot - new Vector4(0,0,0,projectorw) ) ;
        transform.position = new Vector3( Projection.x, Projection.y, Projection.z );
    }

    float Dot2PlaneLength(Vector4 dot, Vector4 axis, Vector4 dotonplane)
    {
        return Vector4.Dot((dotonplane - dot), axis) / axis.magnitude;
    }

    int Findnumber(string newstring)
    {
        int numberinstring = 0;
        for (int i = 0; i < newstring.Length; i++)
            for (int j = 0; j < 10; j++) if (char.Equals(newstring[i], j.ToString()[0])) numberinstring = numberinstring * 10 + j;
        return numberinstring;
    }

    void Initialpos()
    {
        current_pos = new Vector4(5, 5, 5, 5);
        if (intindex / 8 == 0) current_pos.x = -current_pos.x;
        if ((intindex / 4) % 2 == 0) current_pos.y = -current_pos.y;
        if ((intindex / 2) % 2 == 0) current_pos.z = -current_pos.z;
        if (intindex % 2 == 0) current_pos.w = -current_pos.w;
    }
}
