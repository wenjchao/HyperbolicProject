using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Draws a triangle that covers the middle of the screen
    public Material mat;

    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.TRIANGLES); // Triangle
        GL.Color(new Color(1, 1, 1, 1));
        GL.Vertex3(10, 0, 0);
        GL.Vertex3(0, 10, 0);
        GL.Vertex3(0 ,0 , 0);
        GL.End();

        GL.Begin(GL.QUADS); // Quad
        GL.Color(new Color(0.5f, 0.5f, 0.5f, 1));
        GL.Vertex3(0.5f, 0.5f, 0);
        GL.Vertex3(0.5f, 0.75f, 0);
        GL.Vertex3(0.75f, 0.75f, 0);
        GL.Vertex3(0.75f, 0.5f, 0);
        GL.End();

        GL.Begin(GL.LINES); // Line
        GL.Color(new Color(0, 0, 0, 1));
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0.75f, 0.75f, 0);
        GL.End();
        GL.PopMatrix();
    }
}
