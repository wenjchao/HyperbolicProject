using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restricted : MonoBehaviour
{
    public int vertexnum;
    public int roadnum;
    public int previousdot;
    public int nextdot;
    public GameObject[] pointss = new GameObject[3];
    public Vector3 posObj;
    public Vector3 posA;
    public Vector3 posB;
    public bool onroad;

    public int totaledgesnum;
    public int totalverticesnum;
    public int[] findvertices;
    public int[] findedges;
    public int[] sumupedges;
    // Start is called before the first frame update
    void Start()
    {
        roadnum = 0;
        onroad = true;

        //findedges { edge_0 from list_0 to list_1, edge_1 form list_2 to list_3, edge_2 from list_4 to list_5... }
        //findedges = new int[totaledgesnum * 2];
        findedges = new int[] { 0, 1, 0, 2, 1, 2 };

        totaledgesnum = findedges.Length / 2;
        totalverticesnum = pointss.Length;

        //countedges { vertex_0 have list_0 edges, vertex_1 have list_1 edges, vertex_2 have list_2 edges... }
        //countedges = new int[] { 2, 2, 2 };
        //sumupedges = new int[] { 0, 2, 4, 6 };
        sumupedges = new int[totalverticesnum + 1];
        for (int i = 0; i < totaledgesnum * 2; i++) sumupedges[findedges[i] + 1]++;
        for (int i = 1; i < totalverticesnum + 1; i++) sumupedges[i] = sumupedges[i - 1] + sumupedges[i];

        //findvertices { vertex_0 have first edge to list_0 under edge_number list_1 , same vertex_0 have second edge to list_2 under edge_number list_3,... }
        //findvertices = new int[] { 1, 0, 2, 1, 0, 0, 2, 2, 0, 1, 1, 2 };
        findvertices = new int[totaledgesnum * 4];
        int count = 0;
        for (int i = 0; i < totalverticesnum; i++){
            for (int j = 0; j < totaledgesnum * 2; j++){
                if(findedges [j] == i)
                {
                    if(j%2 == 0) findvertices[count] = findedges[j + 1];
                    else findvertices[count] = findedges[j - 1];

                    findvertices[count + 1] = j / 2;
                    count += 2;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if on vertex
        if (!onroad){

            Vector3 ownpos = GetComponent<Transform>().position;
            Vector3 owndotpos = new Vector3(0,0,0);
            int roadcount = 0;
            int[] Roadindex = new int[1];
            Vector3[] Destinationpos = new Vector3[1];
            float max = 0;
            int maxindex = -1;

            owndotpos = pointss[vertexnum].GetComponent<Transform>().position;
            roadcount = sumupedges[vertexnum + 1]- sumupedges[vertexnum];
            Roadindex = new int[roadcount];
            Destinationpos = new Vector3[roadcount];

            for (int i = 0; i < roadcount; i++){
                Destinationpos[i] = pointss[ findvertices[sumupedges[vertexnum] * 2 + 2 * i] ].GetComponent<Transform>().position;
                Roadindex[i] = findvertices[ sumupedges[vertexnum] * 2 + 2 * i + 1 ];
            }

            if ( (owndotpos-ownpos).magnitude > 0.01 ){
                for(int i = 0 ; i < roadcount ; i++){
                    float tempAngle = CosDot2Dot2DotAngle( Destinationpos[i], owndotpos, ownpos );
                    if ( max < tempAngle ) {
                        max = tempAngle;
                        maxindex = i;
                    }
                }

                if (maxindex == -1) transform.position = owndotpos;
                else{
                    roadnum = Roadindex[maxindex];
                    onroad = true;
                }
            }
        }
        //if on edge
        if (onroad){

            previousdot = findedges[2 * roadnum];
            nextdot = findedges[2 * roadnum + 1];
            posA = pointss[previousdot].GetComponent<Transform>().position;
            posB = pointss[nextdot].GetComponent<Transform>().position;

            posObj = GetComponent<Transform>().position;
            Vector3 axis = posB-posA;
            axis.Normalize();
            transform.position = posA + axis * Dot2PlaneLength( posA , posB-posA , posObj );
        }
        //Debug.Log(CosDot2Dot2DotAngle (pointss[0].GetComponent<Transform>().position, pointss[1].GetComponent<Transform>().position,pointss[2].GetComponent<Transform>().position));
    }

    float Dot2PlaneLength( Vector3 dot, Vector3 axis, Vector3 dotonplane ){
        float temp =  Vector3.Dot( (dotonplane-dot) ,axis ) / axis.magnitude;
        if(temp < 0){
            vertexnum = previousdot;
            onroad = false;
            return 0;
        } 
        else if (temp > axis.magnitude){
            vertexnum = nextdot;
            onroad = false;
            return axis.magnitude;
        }
        else return temp ;
    }

    float CosDot2Dot2DotAngle( Vector3 Dotinit, Vector3 Dotmid, Vector3 Dotfin){
        Vector3 line1 = Dotinit-Dotmid;
        Vector3 line2 = Dotfin-Dotmid;
        return Vector3.Dot( line1 , line2 )/line1.magnitude / line2.magnitude;
    }

}
