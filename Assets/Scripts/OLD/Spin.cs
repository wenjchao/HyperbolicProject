using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public bool lft;
    public bool rght;
    public bool frwrd;
    public bool bckwrd;
    public float angle = 0.0f;
    public Vector3 axis = Vector3.zero;
    public float Speed = 1f;
    public float SinSpeed;
    public float CosSpeed;
    public float X;
    public float Y;
    public float Z;
    public Matrix4x4 Coronal;
    public Matrix4x4 Sagittal;
    public Vector4 OutAxis;

    // Start is called before the first frame update
    void Start()
    {
        SinSpeed = Mathf.Sin( Mathf.PI*0.01f );
        CosSpeed = Mathf.Cos( Mathf.PI*0.01f );
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
    }

    void FixedUpdate()
    {
        X = GetComponent<Transform>().position.x;
        Y = GetComponent<Transform>().position.y;
        Z = GetComponent<Transform>().position.z-20;

        if( rght && !lft ){
            Coronal.SetColumn(0, new Vector4( CosSpeed , 0 , SinSpeed  , 0 ) );
            Coronal.SetColumn(1, new Vector4(  0 , 1 , 0 , 0 ) );
            Coronal.SetColumn(2, new Vector4( -SinSpeed , 0 , CosSpeed , 0 ) );
            Coronal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        else if ( !rght && lft ){
            Coronal.SetColumn(0, new Vector4( CosSpeed , 0 , -SinSpeed  , 0 ) );
            Coronal.SetColumn(1, new Vector4(  0 , 1 , 0 , 0 ) );
            Coronal.SetColumn(2, new Vector4( SinSpeed , 0 , CosSpeed , 0 ) );
            Coronal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        else{
            Coronal.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Coronal.SetColumn(1, new Vector4( 0 , 1 , 0 , 0 ) );
            Coronal.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Coronal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }

        if ( frwrd && !bckwrd ){
            Sagittal.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Sagittal.SetColumn(1, new Vector4( 0 , CosSpeed , -SinSpeed , 0 ) );
            Sagittal.SetColumn(2, new Vector4( 0 , SinSpeed , CosSpeed , 0 ) );
            Sagittal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        else if ( bckwrd && !frwrd ){
            Sagittal.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Sagittal.SetColumn(1, new Vector4( 0 , CosSpeed , SinSpeed , 0 ) );
            Sagittal.SetColumn(2, new Vector4( 0 , -SinSpeed , CosSpeed , 0 ) );
            Sagittal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }
        else{
            Sagittal.SetColumn(0, new Vector4( 1 , 0 , 0 , 0 ) );
            Sagittal.SetColumn(1, new Vector4( 0 , 1 , 0 , 0 ) );
            Sagittal.SetColumn(2, new Vector4( 0 , 0 , 1 , 0 ) );
            Sagittal.SetColumn(3, new Vector4( 0 , 0 , 0 , 1 ) );
        }

        OutAxis = Sagittal * (Coronal * new Vector4(X,Y,Z,1) );
        transform.position = new Vector3(OutAxis.x,OutAxis.y,OutAxis.z+20);
        
    }
}
