using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    Ray cameraRay;
    RaycastHit cameraHitInfo;
    GameObject[] hidenObjects;
    private bool buildMode = false;
    
    void Update()
    {
        DrawPointer();
        if (!buildMode) 
        {
            TriggerActionOff();
            if (Input.GetMouseButtonDown(1))
            {
                SwitchMode(); 
            }     
        }
        else
            TriggerActionOn(); 
    }
    void DrawPointer() 
    {
        cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(cameraRay, out cameraHitInfo);
        Debug.DrawLine(cameraRay.origin, cameraHitInfo.point, Color.blue);
    }
    void TriggerActionOff() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            cameraHitInfo.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
            cameraHitInfo.collider.enabled = false;
            cameraHitInfo.collider.gameObject.tag = "hiden";
        }
    }
    void SwitchMode() 
    {
        buildMode = true;
        Debug.Log("Build MODE");
        hidenObjects = GameObject.FindGameObjectsWithTag("hiden");
        foreach (GameObject hidenObject in hidenObjects)
        {
            hidenObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    void TriggerActionOn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cameraHitInfo.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
            cameraHitInfo.collider.gameObject.tag = "shown";
        }
    }
}
