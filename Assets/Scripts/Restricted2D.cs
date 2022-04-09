using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restricted2D : MonoBehaviour
{
    public int roadnum;
    public int previousdot;
    public int nextdot;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    public GameObject point5;
    public GameObject point6;
    public GameObject point7;
    public GameObject point0;
    public Vector3 posObj;
    public Vector3 posA;
    public Vector3 posB;
    // Start is called before the first frame update
    void Start()
    {
        roadnum = 1;
        previousdot = 101;
        nextdot = 102;
    }

    // Update is called once per frame
    void Update()
    {
        if (roadnum > 99){

            Vector3 ownpos = GetComponent<Transform>().position;
            Vector3 owndotpos = new Vector3(0,0,0);
            int roadcount = 0;
            int[] Roadindex = new int[1];
            Vector3[] Destinationpos = new Vector3[1];
            float max = 0;
            int maxindex = -1;

            if(roadnum == 100){
                owndotpos = point0.GetComponent<Transform>().position;
                roadcount = 2;
                Roadindex = new int[roadcount];
                Destinationpos = new Vector3[roadcount];

                Roadindex[0] = 1;
                Roadindex[1] = 2;
                Destinationpos[0] = point1.GetComponent<Transform>().position;
                Destinationpos[1] = point2.GetComponent<Transform>().position;
            }
            if(roadnum == 101){
                owndotpos = point1.GetComponent<Transform>().position;
                roadcount = 2;
                Roadindex = new int[roadcount];
                Destinationpos = new Vector3[roadcount];

                Roadindex[0] = 0;
                Roadindex[1] = 3;
                Destinationpos[0] = point0.GetComponent<Transform>().position;
                Destinationpos[1] = point4.GetComponent<Transform>().position;
            }
            else if(roadnum == 102){
                owndotpos = point2.GetComponent<Transform>().position;
                roadcount = 2;
                Roadindex = new int[roadcount];
                Destinationpos = new Vector3[roadcount];

                Roadindex[0] = 2;
                Roadindex[1] = 4;
                Destinationpos[0] = point0.GetComponent<Transform>().position;
                Destinationpos[1] = point5.GetComponent<Transform>().position;
            }
            else if(roadnum == 103){
                owndotpos = point3.GetComponent<Transform>().position;
                roadcount = 2;
                Roadindex = new int[roadcount];
                Destinationpos = new Vector3[roadcount];

                Roadindex[0] = 5;
                Roadindex[1] = 6;
                Destinationpos[0] = point5.GetComponent<Transform>().position;
                Destinationpos[1] = point6.GetComponent<Transform>().position;
            }
            else if(roadnum == 104){
                owndotpos = point4.GetComponent<Transform>().position;
                roadcount = 2;
                Roadindex = new int[roadcount];
                Destinationpos = new Vector3[roadcount];

                Roadindex[0] = 3;
                Roadindex[1] = 7;
                Destinationpos[0] = point1.GetComponent<Transform>().position;
                Destinationpos[1] = point7.GetComponent<Transform>().position;
            }
            else if(roadnum == 105){
                owndotpos = point5.GetComponent<Transform>().position;
                roadcount = 2;
                Roadindex = new int[roadcount];
                Destinationpos = new Vector3[roadcount];

                Roadindex[0] = 4;
                Roadindex[1] = 5;
                Destinationpos[0] = point2.GetComponent<Transform>().position;
                Destinationpos[1] = point3.GetComponent<Transform>().position;
            }
            else if(roadnum == 106){
                owndotpos = point6.GetComponent<Transform>().position;
                roadcount = 2;
                Roadindex = new int[roadcount];
                Destinationpos = new Vector3[roadcount];

                Roadindex[0] = 6;
                Roadindex[1] = 8;
                Destinationpos[0] = point3.GetComponent<Transform>().position;
                Destinationpos[1] = point7.GetComponent<Transform>().position;
            }
            else if(roadnum == 107){
                owndotpos = point3.GetComponent<Transform>().position;
                roadcount = 2;
                Roadindex = new int[roadcount];
                Destinationpos = new Vector3[roadcount];

                Roadindex[0] = 7;
                Roadindex[1] = 8;
                Destinationpos[0] = point4.GetComponent<Transform>().position;
                Destinationpos[1] = point7.GetComponent<Transform>().position;
            }

            if ( (owndotpos-ownpos).magnitude > 0.01 ){
                for(int i = 0 ; i < roadcount ; i++){
                    float tempAngle = CosDot2Dot2DotAngle( Destinationpos[i], owndotpos, ownpos );
                    if ( max < tempAngle ) {
                        max = tempAngle;
                        maxindex = i;
                    }
                }
                
                if( maxindex == -1 ) transform.position = owndotpos;
                else roadnum = Roadindex[ maxindex ];
            }

            
        }
        else{
            if (roadnum == 1){
                posA = point0.GetComponent<Transform>().position;
                posB = point1.GetComponent<Transform>().position;
                previousdot = 100;
                nextdot = 101;
            }
            else if (roadnum == 2){
                posA = point0.GetComponent<Transform>().position;
                posB = point2.GetComponent<Transform>().position;
                previousdot = 100;
                nextdot = 102;
            }
            else if (roadnum == 3){
                posA = point1.GetComponent<Transform>().position;
                posB = point4.GetComponent<Transform>().position;
                previousdot = 101;
                nextdot = 104;
            }
            else if (roadnum == 4){
                posA = point2.GetComponent<Transform>().position;
                posB = point5.GetComponent<Transform>().position;
                previousdot = 102;
                nextdot = 105;
            }
            else if (roadnum == 5){
                posA = point3.GetComponent<Transform>().position;
                posB = point5.GetComponent<Transform>().position;
                previousdot = 103;
                nextdot = 105;
            }
            else if (roadnum == 6){
                posA = point3.GetComponent<Transform>().position;
                posB = point6.GetComponent<Transform>().position;
                previousdot = 103;
                nextdot = 106;
            }
            else if (roadnum == 7){
                posA = point4.GetComponent<Transform>().position;
                posB = point7.GetComponent<Transform>().position;
                previousdot = 104;
                nextdot = 107;
            }
            else if (roadnum == 8){
                posA = point6.GetComponent<Transform>().position;
                posB = point7.GetComponent<Transform>().position;
                previousdot = 106;
                nextdot = 107;
            }

            posObj = GetComponent<Transform>().position;
            Vector3 axis = posB-posA;
            axis.Normalize();
            transform.position = posA + axis * Dot2PlaneLength( posA , posB-posA , posObj );

        }
        


        

        //Debug.Log(CosDot2Dot2DotAngle (point1.GetComponent<Transform>().position, point2.GetComponent<Transform>().position,point3.GetComponent<Transform>().position));
    }

    float Dot2PlaneLength( Vector3 dot, Vector3 axis, Vector3 dotonplane ){
        float temp =  Vector3.Dot( (dotonplane-dot) ,axis ) / axis.magnitude;
        if(temp < 0){
            roadnum = previousdot;
            return 0;
        } 
        else if (temp > axis.magnitude){
            roadnum = nextdot;
            return axis.magnitude;
        }
        else return temp ;
    }

    float CosDot2Dot2DotAngle( Vector3 Dotinit, Vector3 Dotmid, Vector3 Dotfin){
        Vector3 line1 = Dotinit-Dotmid;
        Vector3 line2 = Dotfin-Dotmid;
        return Vector3.Dot( line1 , line2 )/line1.magnitude / line2.magnitude;
    }

    int MaxIndex (float[] intArray){
        float max = intArray[0];
        int maxindex = 0;
        for (int i = 0; i < intArray.Length; i++) {
            if (intArray[i] > max) {
                max = intArray[i];
                maxindex = i;
            }
        }
        return maxindex;
    }

}
