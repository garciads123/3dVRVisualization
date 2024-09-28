using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvSphereMapping : MonoBehaviour
{
    // The cube that's containing all the points
    public GameObject cubeToDraw;

    // The object representing all the points
    public GameObject spherePrefab;

    // Holder of all the information from the selected csv
    private DataReader infoObject;

    // All points that are going to be dressed and designed.
    List<pointValues> dataPoints;


    //Used to normalize Values
    private float minX;
    private float minY;
    private float minZ;
    private float maxX;
    private float maxY;
    private float maxZ;

    // All points 
    private List<Vector3> allPoints = new List<Vector3>();
    private Vector3 cubeSize;
    private Vector3 cubeOrigin;

    // For resizing 
    private float resizeFactX;
    private float resizeFactY;
    private float resizeFactZ;

    private float factorResize;
    
    // List of all gameObjects that are ploted
    private List<GameObject> ListSphere = new List<GameObject>();
    private List<SphereColorSelector> ListOfColorSel = new List<SphereColorSelector>();

    // Axis Information.
    private AxisNamesChange axisName;

    private int rM;
    void Start()
    {
        // Data Read in
        infoObject = GetComponent<DataReader>();
        // Cube Information
        cubeSize = cubeToDraw.transform.localScale;
        cubeOrigin = cubeToDraw.transform.position;
        // Axis Namer
        axisName = GetComponent<AxisNamesChange>();

        minX = float.MaxValue;
        minY = float.MaxValue;
        minZ = float.MaxValue;
        maxX = float.MinValue;
        maxY = float.MinValue;
        maxZ = float.MinValue;

        dataPoints = infoObject.allPoints;
        for(int i = 0; i < dataPoints.Count; i++) {
            // Find minimum Points on each axis
            if(dataPoints[i].xVal < minX) {
                minX = dataPoints[i].xVal;
            }
            if(dataPoints[i].yVal < minY) {
                minY = dataPoints[i].yVal;
            }
            if(dataPoints[i].zVal < minZ) {
                minZ = dataPoints[i].zVal;
            }

            // Find Maximum Points on each axis
            if(dataPoints[i].xVal > maxX) {
                maxX = dataPoints[i].xVal;
            }
            if(dataPoints[i].yVal > maxY) {
                maxY = dataPoints[i].yVal;
            }
            if(dataPoints[i].zVal > maxZ) {
                maxZ = dataPoints[i].zVal;
            }
        }
        rM = 0;
        factorResize = 1.0f;
        resizeFactX = spherePrefab.transform.localScale.x/cubeToDraw.transform.localScale.x;
        resizeFactY = spherePrefab.transform.localScale.y/cubeToDraw.transform.localScale.y;
        resizeFactZ = spherePrefab.transform.localScale.z/cubeToDraw.transform.localScale.z;
        axisName.updateAxisLabels(infoObject.XCol, infoObject.YCol, infoObject.ZCol);
        axisName.updateMaxLabels(maxX, maxY, maxZ);
        instantiateAllPoints(rM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 NormalizeToPoint(float PointX, float PointY, float PointZ, float factorResize){
        float cubeSizeMinX = (-cubeSize.x/2.0f); 
        float cubeSizeMaxX = (cubeSize.x/2.0f); 
        float cubeSizeMinY = (-cubeSize.y/2.0f); 
        float cubeSizeMaxY = (cubeSize.y/2.0f); 
        float cubeSizeMinZ = (-cubeSize.z/2.0f); 
        float cubeSizeMaxZ = (cubeSize.z/2.0f); 
        float divisorX = maxX - minX;
        float divisorY = maxY - minY;
        float divisorZ = maxZ - minZ;
        float dividendX = ((PointX - minX)*(cubeSizeMaxX - cubeSizeMinX));
        float dividendY = ((PointY - minY)*(cubeSizeMaxY - cubeSizeMinY));
        float dividendZ = ((PointZ - minZ)*(cubeSizeMaxZ - cubeSizeMinZ));
        return new Vector3((dividendX/divisorX + cubeSizeMinX), (dividendY/divisorY + cubeSizeMinY), (dividendZ/divisorZ + cubeSizeMinZ));
        
    }

    void instantiateAllPoints(int rM) {
        for(int i = 0; i < dataPoints.Count; i++) {
            allPoints.Add(NormalizeToPoint(dataPoints[i].xVal, dataPoints[i].yVal, dataPoints[i].zVal, 1.0f)); 
            Vector3 position = cubeOrigin + allPoints[i];
            GameObject tempSphere = Instantiate(spherePrefab, position, Quaternion.identity);
            float xSize = resizeFactX*cubeToDraw.transform.localScale.x;
            float ySize = resizeFactY*cubeToDraw.transform.localScale.y;
            float zSize = resizeFactZ*cubeToDraw.transform.localScale.z;
            ListOfColorSel.Add(tempSphere.GetComponent<SphereColorSelector>());
            ListOfColorSel[i].renderMode = rM;
            ListOfColorSel[i].ColorByPosition(allPoints[i], cubeSize);
            ListSphere.Add(tempSphere); 
            ListSphere[i].transform.parent = transform;
            ListSphere[i].transform.localScale = new Vector3(xSize, ySize, zSize);
        }
    }

    public void UpdatePointsForResize() {
        allPoints.Clear();
        allPoints = new List<Vector3>();

        foreach(GameObject obj in ListSphere) {
            if(obj != null) {
                Destroy(obj);
            }
        }
        foreach(SphereColorSelector obj in ListOfColorSel) {
            if(obj != null) {
                Destroy(obj);
            }
        }
        ListSphere.Clear();
        ListOfColorSel.Clear();
        ListSphere = new List<GameObject>();
        factorResize = factorResize * 0.90f;
        rM = 1;
        UpdateCubeForResize(factorResize, factorResize, factorResize);
        instantiateAllPoints(1);
    }

    public void UpdateCubeForResize(float factorResizeX, float factorResizeY, float factorResizeZ) {
        cubeToDraw.transform.localScale = new Vector3(cubeToDraw.transform.localScale.x * factorResizeX, cubeToDraw.transform.localScale.y * factorResizeY, cubeToDraw.transform.localScale.z * factorResizeZ);
        cubeSize = cubeToDraw.transform.localScale;
    }
}
