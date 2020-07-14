using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Camera theCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //ให้มันอัปเดตทีหลัง Player
    void LateUpdate() {
        transform.position = target.position;
        transform.rotation = target.rotation;

        
    }

}
