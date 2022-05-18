using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinandProject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool lft;
    public bool rght;
    public bool frwrd;
    public bool bckwrd;
    public Vector3 POS;
    public Matrix4x4 Matx;
    public float SinSpeed;
    public float CosSpeed;
    public Vector4 OutAxis;
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
    void FixedUpdate() {
        POS = GetComponent<Transform>().position;

        if( rght && !lft ){ Spin(SinSpeed,CosSpeed,POS,Vector3.forward); }
        else if ( !rght && lft ){ Spin(-SinSpeed,CosSpeed,POS,Vector3.forward); }
        if ( frwrd && !bckwrd ){ Spin(-SinSpeed,CosSpeed,POS,Vector3.right); }
        else if ( bckwrd && !frwrd ){ Spin(SinSpeed,CosSpeed,POS,Vector3.right); }
        Vector3 projector = new Vector3(0,10,0);
        Vector3 axis = new Vector3(0,1,0);
        Vector3 dotonplane = new Vector3(0,0,0);
        Project(POS,projector,axis,dotonplane);

    }

    void Spin(float sintheta, float costheta, Vector3 dot, Vector3 axis){
        axis = axis / axis.magnitude;
        Matx.SetColumn( 0, new Vector4( costheta + axis.x*axis.x*(1-costheta), axis.x*axis.y*(1-costheta)-axis.z*sintheta, axis.x*axis.z*(1-costheta)+axis.y*sintheta,0 ) );
        Matx.SetColumn( 1, new Vector4( axis.x*axis.y*(1-costheta)+axis.z*sintheta, costheta + axis.y*axis.y*(1-costheta), axis.y*axis.z*(1-costheta)-axis.x*sintheta,0 ) );
        Matx.SetColumn( 2, new Vector4( axis.x*axis.z*(1-costheta)-axis.y*sintheta, axis.y*axis.z*(1-costheta)+axis.x*sintheta, costheta + axis.z*axis.z*(1-costheta),0 ) );
        Matx.SetColumn( 3, new Vector4( 0,0,0,1 ) );

        OutAxis = Matx * new Vector4(dot.x,dot.y,dot.z,1);
        transform.position = new Vector3(OutAxis.x,OutAxis.y,OutAxis.z);
    }
    void Project( Vector3 dot, Vector3 projector, Vector3 axis, Vector3 dotonplane ){
        float dot2plane = Dot2PlaneLength(dot, axis, dotonplane);
        float projector2plane = Dot2PlaneLength(projector, axis, dotonplane);
        
        POS = dot2plane / ( projector2plane - dot2plane ) * ( dot - projector ) + dot ;
    }

    float Dot2PlaneLength( Vector3 dot, Vector3 axis, Vector3 dotonplane ){
        float temp =  Vector3.Dot( (dotonplane-dot) ,axis ) / axis.magnitude ;
        return temp;
    }
    
}
