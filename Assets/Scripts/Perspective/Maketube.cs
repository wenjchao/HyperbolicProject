using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maketube : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public float diameter;
    public int verticesinround;
    public int intnum;

    public int index;
    public GameObject[] pointss = new GameObject[16];

    // Start is called before the first frame update
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    void Start()
    {
        //decide pointA and pointB
        int[] array = new int[66];
        InitialArray(array);
        pointA = pointss[array[index * 2]];
        pointB = pointss[array[index * 2 + 1]];
        
    }

    // Update is called once per frame
    void Update()
    {
        InitialMesh();
        UpdateMesh();
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void InitialMesh()
    {
        Vector3 posA = pointA.GetComponent<Transform>().position;
        Vector3 posB = pointB.GetComponent<Transform>().position;
        //Quaternion Rota = GetComponent<Transform>().rotation;

        Vector3 BminusA = posB - posA ;
        Vector4 verticalBA = new Vector4(BminusA.y, -BminusA.x, 0, 0);
        if (verticalBA.magnitude == 0) verticalBA = new Vector4(1, 1, 0, 0);
        verticalBA.Normalize();
        Vector4 normBA = new Vector4 (BminusA.x, BminusA.y, BminusA.z, 0);
        normBA.Normalize();

        //create rotation matrix spinning around BA axis
        Matrix4x4[] RotationthroughBA = new Matrix4x4[verticesinround];
        float costheta = Mathf.Cos(2 * Mathf.PI / (float)verticesinround);
        float sintheta = Mathf.Sin(2 * Mathf.PI / (float)verticesinround);
        RotationthroughBA[0].SetColumn(0, costheta * new Vector4(1, 0, 0, 0) + sintheta * new Vector4(0, -normBA.z, normBA.y, 0) + (1 - costheta) * normBA.x * normBA);
        RotationthroughBA[0].SetColumn(1, costheta * new Vector4(0, 1, 0, 0) + sintheta * new Vector4(normBA.z, 0, -normBA.x, 0) + (1 - costheta) * normBA.y * normBA);
        RotationthroughBA[0].SetColumn(2, costheta * new Vector4(0, 0, 1, 0) + sintheta * new Vector4(-normBA.y, normBA.x, 0, 0) + (1 - costheta) * normBA.z * normBA);
        RotationthroughBA[0].SetColumn(3, Vector4.zero);
        for (int j = 1; j < verticesinround; j++) RotationthroughBA[j] = RotationthroughBA[0] * RotationthroughBA[ j - 1 ];
        //for (int j = 0; j < verticesinround; j++) Debug.Log(RotationthroughBA[j]);

        vertices = new Vector3[ (intnum+1) * verticesinround ];
        triangles = new int[ intnum * verticesinround * 6 ];
        for(int i = 0; i <= intnum; i++){
            Vector3 midpoint = posA + BminusA * (float)i / (float)intnum;
            for (int j = 0; j < verticesinround; j++)
            {
                Vector4 temp = RotationthroughBA[j] * verticalBA;
                vertices[i * verticesinround + j] = midpoint + new Vector3(temp.x,temp.y,temp.z) * diameter ; 
            }
        }
        for (int i = 0; i < intnum; i++){
            for (int j = 0; j < verticesinround - 1; j++)
            {
                triangles[0 + j * 6 + verticesinround * 6 * i] = 0 + j + verticesinround * i;
                triangles[1 + j * 6 + verticesinround * 6 * i] = triangles[4 + j * 6 + verticesinround * 6 * i] = verticesinround + j + verticesinround * i;
                triangles[2 + j * 6 + verticesinround * 6 * i] = triangles[3 + j * 6 + verticesinround * 6 * i] = 1 + j + verticesinround * i;
                triangles[5 + j * 6 + verticesinround * 6 * i] = verticesinround + 1 + j + verticesinround * i;
            }
            triangles[0 + (verticesinround - 1) * 6 + verticesinround * 6 * i] = verticesinround - 1 + verticesinround * i;
            triangles[1 + (verticesinround - 1) * 6 + verticesinround * 6 * i] = triangles[4 + (verticesinround - 1) * 6 + verticesinround * 6 * i] = verticesinround + verticesinround - 1 + verticesinround * i;
            triangles[2 + (verticesinround - 1) * 6 + verticesinround * 6 * i] = triangles[3 + (verticesinround - 1) * 6 + verticesinround * 6 * i] = verticesinround * i; 
            triangles[5 + (verticesinround - 1) * 6 + verticesinround * 6 * i] = verticesinround + verticesinround * i;
        }
    }

    void InitialArray(int[] array)
    {
        Vector4[] current_pos = new Vector4[32];
        int arraycount = 0;
        for (int intindex = 0; intindex < 16; intindex++)
        {
            current_pos[intindex] = new Vector4(1, 1, 1, 1);
            if (intindex / 8 == 0) current_pos[intindex].x = -current_pos[intindex].x;
            if ((intindex / 4) % 2 == 0) current_pos[intindex].y = -current_pos[intindex].y;
            if ((intindex / 2) % 2 == 0) current_pos[intindex].z = -current_pos[intindex].z;
            if (intindex % 2 == 0) current_pos[intindex].w = -current_pos[intindex].w;
        }
        for (int intindex = 0; intindex < 16; intindex++)
        {
            for (int i = 0; i < intindex; i++)
            {
                if ((current_pos[intindex] - current_pos[i]).magnitude == 2)
                {
                    array[arraycount] = i;
                    arraycount++;
                    array[arraycount] = intindex;
                    arraycount++;
                }
            }
        }
    }
}
