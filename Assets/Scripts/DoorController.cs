using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{


    public bool isOpened, isClosed;
    public bool isTrigger;
    public Transform theDoor, openRot, closeRot;

    public float openSpeed;

    private Quaternion startRot;


    // Start is called before the first frame update
    void Start() {
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.F) && isTrigger) {
            if (isClosed)
                isClosed = false;
            else {
                isClosed = true;
            }
        }
       

        if (!isClosed) {
            theDoor.rotation = Quaternion.Slerp(theDoor.rotation, openRot.rotation, openSpeed * Time.deltaTime);

        }
         else {
            theDoor.rotation = Quaternion.Slerp(theDoor.rotation, closeRot.rotation, openSpeed * Time.deltaTime);
            
            
        }
        

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            isTrigger = false;
        }
    }
}
