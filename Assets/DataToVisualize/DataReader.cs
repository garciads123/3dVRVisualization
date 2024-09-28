using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;
public class DataReader : MonoBehaviour
{   
    public string fileName;
    public string XCol; 
    public string YCol;
    public string ZCol;

    private string[] columnsToRead;
    public List<pointValues> allPoints = new List<pointValues>();
    // Start is called before the first frame update
    void Start()
    {
        //OpenFileExplorer()
        ReadCSVFile(OpenFileExplorer());
    }

    void ReadCSVFile(string csvFileName) {
        string filePath = csvFileName; //Path.Combine(Application.streamingAssetsPath, csvFileName);

        //string[] columnsToRead = {XCol, YCol, ZCol};

        if(!File.Exists(filePath)) {
            UnityEngine.Debug.LogError ("CSV file not found at path: " + filePath);
            return;
        }

        try {
            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(filePath);
            
            // Read column names from the first row
            string[] columnNames = lines[0].Split(',');

            // indexes of columns that we care about
            List<int> columnIndexList = new List<int>();
            foreach(string columnName in columnNames) {
                int index = Array.IndexOf(columnNames, columnName);
                if (index == 0) {
                    XCol = columnName;
                }
                if (index == 1) {
                    YCol = columnName;
                }
                if (index == 2) {
                    ZCol = columnName;
                }
                if (index != -1) {
                    columnIndexList.Add(index);
                }
                else {
                    UnityEngine.Debug.LogWarning("Column not found: " + columnName);
                }
            }

            foreach (string line in lines) {
                if(line == lines[0]){
                    continue; 
                }

                string[] values = line.Split(',');

                string[] selectedValues = new string[columnIndexList.Count];
                for(int i = 0; i < columnIndexList.Count; i++){
                    int columnIndex = columnIndexList[i];
                    if (columnIndex >= 0 && columnIndex < values.Length) {
                        selectedValues[i]  = values[columnIndex];
                    }
                    else {
                        selectedValues[i] = "";
                    }
                }

                pointValues newPoint = new pointValues();
                newPoint.xValueString = selectedValues[0];
                newPoint.yValueString = selectedValues[1];
                newPoint.zValueString = selectedValues[2];
                newPoint.xVal = float.Parse(newPoint.xValueString);
                newPoint.yVal = float.Parse(newPoint.yValueString);
                newPoint.zVal = float.Parse(newPoint.zValueString);
                allPoints.Add(newPoint);
            }
            /*foreach (string[] row in csvData){
                foreach (string value in row){
                    Debug.Log(value);
                }
            }*/
        }

        catch(System.Exception e) {
            UnityEngine.Debug.LogError("Error reading CSV file: " + e.Message);
        }
    }
    // Update is called once per frame

    public string OpenFileExplorer() {
        string path =  EditorUtility.OpenFilePanel("Select Which file to analyze (.csv)", "", "csv");
        return path;
    }

    void Update()
    {
        
    }
}

public class pointValues {
    public string xValueString;
    public string yValueString;
    public string zValueString;
    public float xVal;
    public float yVal; 
    public float zVal;
}
