using UnityEngine;
using System.Collections;
using System;
//using Excel = Microsoft.Office.Interop.Excel;


public class OpenGL : MonoBehaviour {

    public Material mat;

    //Animation variable
    private float anim = 0.2f;
    private Vector3 a = new Vector3(0, 0, 0);
    private Vector3 b;
    private float[] array = {0.2f, 0.4f, 
                             0.1f, 0.6f,
                             0.4f, 0.8f,
                            0.5f, 0.3f};
  
    

	// Use this for initialization
	void Start () {
      //  b = new Vector3(anim, .2f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

        //Animation increase value
       // anim += 0.1f;
	}

    void OnPostRender()
    {
        //Animate line
        b = new Vector3(anim + .2f, .2f, 0.0f);

        GL.PushMatrix();
        GL.LoadPixelMatrix();
        GL.Viewport(new Rect(50, 50, Screen.width, Screen.height-50));

        mat.SetPass(0);
        GL.LoadOrtho();
        
        this.drawAxis();
        this.drawRadialPaired(4);



        /*
        GL.Begin(GL.LINES);
        GL.Color(new Color(1, 1, 1));
        GL.Vertex3(a.x, a.y, a.z);
        GL.Vertex3(b.x, b.y, b.z);
        GL.End();

        //Random Lines
        this.drawLine(0.0f, 0.0f, 0.7f, 0.7f, Color.blue);
        this.drawLine(0.7f, 0.7f, 0.8f, 0.90f, Color.green);
        */

        GL.PopMatrix();
    
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, Screen.height -60, Screen.width, 20));
        GUILayout.BeginHorizontal();
        GUILayout.Label("0-");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, Screen.height - 100, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.Label("1-");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    
    
    }

    void drawAxis()
    {
        //x
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex3(0.9f, 0.0f, 0.0f);
        GL.Vertex3(0.0f, 0.0f, 0.0f);
        GL.End();

        //y
        GL.Begin(GL.LINES);
        GL.Vertex3(0.0f, 0.0f, 0.0f);
        GL.Vertex3(0.0f, 0.9f, 0.0f);
        GL.End();
        

        //Increment values
        //y starting at 1
        GL.Begin(GL.LINES);
        GL.Vertex3(0.0f, 0.1f, 0.0f);
        GL.Vertex3(0.01f, 0.1f, 0.0f);
        GL.End();

        //y =2
        GL.Begin(GL.LINES);
        GL.Vertex3(0.0f, 0.2f, 0.0f);
        GL.Vertex3(0.01f, 0.2f, 0.0f);
        GL.End();

        //y =3
        GL.Begin(GL.LINES);
        GL.Vertex3(0.0f, 0.3f, 0.0f);
        GL.Vertex3(0.01f, 0.3f, 0.0f);
        GL.End();
    }

    void drawLine(float x, float y, float x2, float y2, Color c)
    {
        Vector3 one = new Vector3(x, y, 0);
        Vector3 two = new Vector3(x2, y2, 0);

        GL.Begin(GL.LINES);
        GL.Color(c);
        
        GL.Vertex3(one.x, one.y, one.z);
        GL.Vertex3(two.x, two.y, two.z);
        GL.End();
    
    
    }

    /**
     *  Draws radial graph with the input of size meaning every how often it creates 
     *  a new orgin. For example if the size is two then every two paired coordinates the 
     *  algorithm will create a new origin.
     * 
    */
    public void drawRadialPaired(int size)
    {
        Vector3 point1 = new Vector3();
        Vector3 point2 = new Vector3();

        int currentOriginX = 0;
        int currentOriginY = 0;

        for (int i = 2; i < array.Length ; i+= 2)
        {
            /*
            if (i % size == 0)
            {
                currentOriginX = i;
                currentOriginY = i + 1;
                i += 2;
            }
            */
            this.drawLine(array[currentOriginX], array[currentOriginY], array[i], array[i + 1], Color.blue);

            point1.Set(array[currentOriginX], array[currentOriginY], 0);
            point2.Set(array[i], array[i + 1], 0);

            this.drawArrowHead(point1, point2);

        }

    }

    void drawArrowHead(Vector3 point1, Vector3 point2)
    {
        float arrowHeadSize = 0.03f;

        //vector along line
        Vector3 vec = new Vector3();
        vec.x = point2.x - point1.x;
        vec.y = point2.y - point1.y;
        vec.z = point2.z - point1.z;

        vec.Normalize();

        //Perpendicular vector
        Vector3 perp1 = new Vector3();
        Vector3 perp2 = new Vector3();
        perp1.Set(-vec.y, vec.x, 0);
        perp2.Set(vec.y, -vec.x, 0);

        //Point behind endpoint
        Vector3 point3 = new Vector3();
        point3 = point2 - (vec * arrowHeadSize);


        //Point on of the corners of the triangle
        Vector3 point4 = new Vector3();
        Vector4 point5 = new Vector3();
        point4 = point3 + (perp2 * arrowHeadSize);
        point5 = point3 + (perp1 * arrowHeadSize);


        GL.Begin(GL.TRIANGLES);
        GL.Vertex3(point2.x, point2.y, 0.0f);
        GL.Vertex3(point4.x, point4.y, 0.0f);
        GL.Vertex3(point5.x, point5.y, 0.0f);
        GL.End();
    
    }
     
}


