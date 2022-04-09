using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MakeObjectTransparent : MonoBehaviour
{
    public GameObject Map;
    public GameObject Player;
    public GameObject Pointer;
    public int Playerloc;
    public int Futureloc;
    public int Roadloc1;
    public int Roadloc2;
    public bool NotReverse;
    public GameObject currentGameObject;
    public float alpha_y ;//half transparency
    public float alpha_n ;
    public float RotationSpeed = 50;
    //Get current material
    private Material currentMat;
    // Start is called before the first frame update

    void Start()
    {
        currentGameObject = gameObject;
        currentMat = currentGameObject.GetComponent<Renderer>().material;
        RotationSpeed = 50f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Lightup();
    }

    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }

    void Lightup(){
        Playerloc = Player.GetComponent<PlayerMoveInMaze2>().location;
        Futureloc = Player.GetComponent<PlayerMoveInMaze2>().futureloc;
        
        if (Playerloc == Roadloc1 && Futureloc == Roadloc2){
            ChangeAlpha(currentMat, alpha_y);
            Pointer.SetActive(true);
            NotReverse = true;
            RotateSmoothly();
        }
        else if (Playerloc == Roadloc2 && Futureloc == Roadloc1){
            ChangeAlpha(currentMat, alpha_y);
            Pointer.SetActive(true);
            NotReverse = false;
            RotateSmoothly();
        }
        else {
            ChangeAlpha(currentMat, alpha_n);
            Pointer.SetActive(false);
        }
    }
    void RotateSmoothly(){

        float distance = Player.GetComponent<PlayerMoveInMaze2>().distance;
        float fnum = GetComponent<MakeUI>().num;
        float fsteps = distance / 20f * fnum;
        int num = (int) fnum;
        int steps = (int) fsteps;
        //Debug.Log(steps);
        if (NotReverse == true){
            Vector3 RoadDirection = GetComponent<MakeUI>().midpoints[ steps+1 ] - GetComponent<MakeUI>().midpoints[steps];
            float RoadRotation = Vector3.SignedAngle( Vector3.forward , RoadDirection , Vector3.up );
            float PlayerRotation = Player.GetComponent<PlayerMoveInMaze2>().angel;
            /*if (RoadRotation != 0){

                Debug.Log(RoadRotation);
                Debug.Log(RoadDirection);
                Debug.Log(GetComponent<MakeUI>().midpoints[ steps+1 ]);
                Debug.Log(GetComponent<MakeUI>().midpoints[ steps ]);
            }*/
            float TotalRotation = -RoadRotation-PlayerRotation;
            if (TotalRotation > 180) TotalRotation = TotalRotation - 360;
            if (TotalRotation < -180) TotalRotation = TotalRotation + 360;
            Map.GetComponent<Transform>().Rotate(0, TotalRotation / RotationSpeed ,0);
            Pointer.GetComponent<Transform>().position = GetComponent<MakeUI>().midpoints[steps]+new Vector3(0,0.01f,0);
        } 
        else {
            Vector3 RoadDirection = GetComponent<MakeUI>().midpoints[num - steps - 1 ] - GetComponent<MakeUI>().midpoints[num - steps];
            float RoadRotation = Vector3.SignedAngle( Vector3.forward , RoadDirection , Vector3.up );
            float PlayerRotation = Player.GetComponent<PlayerMoveInMaze2>().angel;
            float TotalRotation = -RoadRotation-PlayerRotation;
            if (TotalRotation > 180) TotalRotation = TotalRotation - 360;
            if (TotalRotation < -180) TotalRotation = TotalRotation + 360;
            Map.GetComponent<Transform>().Rotate(0, TotalRotation / RotationSpeed ,0);
            Pointer.GetComponent<Transform>().position = GetComponent<MakeUI>().midpoints[num - steps]+new Vector3(0,0.01f,0);
        }
    }
}