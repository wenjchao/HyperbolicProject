using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoStraight : MonoBehaviour
{
    public bool lft;
    public bool rght;
    public bool frwrd;
    public bool bckwrd;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if ( GetComponent<Transform>().position.z >= 15.01 ){
            GetComponent<Transform>().position =new Vector3(GetComponent<Transform>().position.x,GetComponent<Transform>().position.y, -14.99f );
        }
        if ( GetComponent<Transform>().position.z <= -15.01 ){
            GetComponent<Transform>().position =new Vector3(GetComponent<Transform>().position.x,GetComponent<Transform>().position.y, 14.99f );
        }
    }
    private /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(rght){
            transform.position += transform.right * Time.deltaTime*3;
        }
        if(lft){
            transform.position -= transform.right * Time.deltaTime*3;
        }
        if (frwrd){
            transform.position += transform.forward * Time.deltaTime*3;
        }
        if (bckwrd){
            transform.position -= transform.forward * Time.deltaTime*3;
        }
    }
}
