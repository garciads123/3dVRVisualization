using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColorSelector : MonoBehaviour
{
    private Color targetColor = Color.red;
    public Renderer objectRenderer;
    // Start is called before the first frame update
    public int renderMode = 0;
    void Start()
    {
        if (renderMode == 0) {
            objectRenderer = GetComponent<Renderer>();
            objectRenderer.material.color = targetColor;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ColorByPosition(Vector3 Point, Vector3 cubeDim) {
        float red = Point.x + cubeDim.x/2.0f;
        float green = Point.y + cubeDim.y/2.0f;
        float blue = Point.z + cubeDim.z/2.0f; 
        objectRenderer.material.color = new Color(red, green, blue);

    }
}
