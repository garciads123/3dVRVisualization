using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FileManager : MonoBehaviour
{
    string path;
    

    public void OpenFileExplorer() {
        path =  EditorUtility.OpenFilePanel("Select Which file to analyze (.csv)", "", "csv");
    }
}
