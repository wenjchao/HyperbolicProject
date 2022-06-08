using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDObject : MonoBehaviour
{
    public int num;
    public float width;
    public bool[] Matrix;
    public bool[] Display;
    public GameObject[] cubes;
    public GameObject[] cube2ds;

    public int switches;
    public int layer;

    public Material mat;
    public float alpha_y = 1 ;
    public float alpha_n = 0.3f;//half transparency

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        Matrix = GetComponent<Save2D>().cube5;
        Display = new bool[num];
        layer = 0;
        cubes = new GameObject[num];
        InitialCube( cubes ,num);
        cube2ds = new GameObject[num*num];
        InitialCube2d(cube2ds, num * num);
        SetCubePosition();
        SetCube2dPositionAndColor();
    }

    // Update is called once per frame
    void Update()
    {
        InitialMesh();
        UpdateMesh();
        ChangeAlpha(mat, 1);
        if (Input.GetKeyDown(KeyCode.LeftArrow) && layer > 0) layer--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && layer < num - 1) layer++;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < num; i++)
        {
            if (switches == 1) Display[i] = Matrix[i + num * layer];
            else if (switches == 2) Display[i] = Matrix[layer + i * num];

            if (Display[i]) ChangeAlpha(cubes[i],alpha_y);
            else ChangeAlpha(cubes[i], alpha_n);
        }
    }

    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }

    void ChangeAlpha(GameObject obj, float alphaVal)
    {
        Color oldColor = obj.GetComponent<Renderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        obj.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

    void InitialCube(GameObject[] array, int arraynum)
    {
        //string str = "Sphere (" + i.ToString() + ")";
        for (int i = 0; i < arraynum; i++) array[i] = GameObject.Find("Cube (" + i.ToString() + ")");
    }

    void SetCubePosition()
    {
        for (int i = 0; i < num; i++)
        {
            //cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cubes[i].name = "Cube_" + i;
            if (switches == 1) cubes[i].transform.position = new Vector3((i * 2 + 1 - num) * width, 0, 0);
            else cubes[i].transform.position = new Vector3(0, 0, 2 - (i * 2 + 1 - num) * width);
            cubes[i].GetComponent<Renderer>().material = mat;
        }
    }

    void InitialCube2d(GameObject[] array, int arraynum)
    {
        for (int i = 0; i < arraynum; i++) array[i] = GameObject.Find("Cube2d (" + i.ToString() + ")");
    }

    void SetCube2dPositionAndColor()
    {
        for (int i = 0; i < num * num; i++)
        {
            cube2ds[i].transform.position = new Vector3(((i % num) * 2 + 1 - num) * width - 18, 0, (num - ((i / num) * 2 + 1)) * width);
            cube2ds[i].GetComponent<Renderer>().material = mat;
            if (Matrix[i]) ChangeAlpha(cube2ds[i], alpha_y);
            else ChangeAlpha(cube2ds[i], alpha_n);
        }
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
        triangles = new int[6];
        vertices = new Vector3[6];

        vertices[0] = new Vector3(-18-8, 0.6f, (num - layer * 2 - 1) * width);
        vertices[1] = new Vector3(-18-10, 0.6f, (num - layer * 2 - 1) * width - 0.7f);
        vertices[2] = new Vector3(-18-10, 0.6f, (num - layer * 2 - 1) * width + 0.7f);
        vertices[3] = new Vector3(-18+8, 0.6f, (num - layer * 2 - 1) * width);
        vertices[5] = new Vector3(-18+10, 0.6f, (num - layer * 2 - 1) * width - 0.7f);
        vertices[4] = new Vector3(-18+10, 0.6f, (num - layer * 2 - 1) * width + 0.7f);
        if (switches == 2) for (int i = 0; i < 6; i++) vertices[i] = new Vector3(-vertices[i].z - 18f, 0.6f, vertices[i].x + 18);

        for (int i = 0; i < 6; i++) triangles[i] = i;
    }

    public void Switchswitches()
    {
        if (switches == 1) switches = 2;
        else switches = 1;
        layer = 0;
        SetCubePosition();
    }
}