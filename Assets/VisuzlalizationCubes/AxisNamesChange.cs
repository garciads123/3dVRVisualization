using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisNamesChange : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text xAxis;
    public TMP_Text yAxis;
    public TMP_Text zAxis;
    public TMP_Text xMax;
    public TMP_Text yMax;
    public TMP_Text zMax; 
    void Start()
    {
        xAxis.SetText("NewXAxisLabel");
        yAxis.SetText("NewYAxisLabel");
        zAxis.SetText("NewZAxisLabel");
        xMax.SetText("max");
        yMax.SetText("max");
        zMax.SetText("max");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAxisLabels(string xAxisName,string yAxisName,string zAxisName) {
        xAxis.SetText(xAxisName);
        yAxis.SetText(yAxisName);
        zAxis.SetText(zAxisName);

    }

    public void updateMaxLabels(float xMaxLen,float yMaxLen,float zMaxLen) {
        xMax.SetText(xMaxLen.ToString("F2"));
        yMax.SetText(yMaxLen.ToString("F2"));
        zMax.SetText(zMaxLen.ToString("F2"));
    }
}
