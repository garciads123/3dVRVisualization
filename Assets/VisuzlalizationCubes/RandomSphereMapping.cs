using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSphereMapping : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spherePrefab;
    public int numSpheres = 10;
    public GameObject cubeToDraw;
    
    void Start()
    {
        Vector3 cubeSize = cubeToDraw.transform.localScale;
        Vector3 cubeOrigin = cubeToDraw.transform.position;
        for(int i = 0; i < numSpheres; i++){
            Vector3 position = cubeOrigin + new Vector3(Random.Range((-cubeSize.x/2.0f), (cubeSize.x/2.0f)),
                                           Random.Range((-cubeSize.y/2.0f), (cubeSize.y/2.0f)),
                                           Random.Range((-cubeSize.z/2.0f), (cubeSize.z/2.0f)));
            
            GameObject sphere = Instantiate(spherePrefab, position, Quaternion.identity);
            sphere.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
