using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;

    private Rigidbody rb;
    private Camera playerCamera;

    private Transform currPos;



    // Raycast informatoin for detecting visualization cube
    private float raycastRange = 10.0f;
    private KeyCode resizeKey = KeyCode.R;


    public string targetTag = "VisualizationCube";
    public GameObject menuUI;

    public GameObject VisCube;
    public float spawnDistance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        currPos = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        spawnDistance =4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical   = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        movement.Normalize();

        rb.MovePosition(transform.position + movement * movementSpeed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        playerCamera.transform.Rotate(Vector3.left * mouseY); 
        if (Input.GetKeyDown(KeyCode.M)) {
            openCloseMenu();
        }
        if (Input.GetKeyDown(resizeKey)) {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit; 

            if(Physics.Raycast(ray, out hit, raycastRange)) {
                Debug.Log(hit.transform.name);
                CsvSphereMapping objectInteraction = hit.transform.GetComponent<CsvSphereMapping>();

                if(objectInteraction != null) {
                    objectInteraction.UpdatePointsForResize();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            spawnVisCube();
        }
    }

    void openCloseMenu()
    {
        menuUI.SetActive(!menuUI.activeSelf);
    }
    void spawnVisCube() {
        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward  * spawnDistance; 
        GameObject tempSphere = Instantiate(VisCube, spawnPosition, Quaternion.identity);
    }
}
