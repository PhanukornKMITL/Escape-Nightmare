using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    private CharacterController charCon;
    public Transform camTrans;
    private Vector3 moveInput;
    private float runSpeed = 8, gravityModifier = 2;
    private float moveSpeed = 5;
    private float jumpPower = 5;
    private bool canJump;
    public float mouseSensitivity = 1;
    public Vector3 crouchPos, standPos;


    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        charCon = GetComponent<CharacterController>();
        
        standPos = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        crouchPos = new Vector3(0f, standPos.y - 0.5f, 0f);
    }

    // Update is called once per frame
    void Update() {
        //store y velocity

        float ystore = moveInput.y;
       

        //เพื่อให้มันหันตรงด้าน
        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");


        moveInput = horiMove + vertMove;
        moveInput.Normalize();

        if (Input.GetKey(KeyCode.LeftShift)) {
            moveInput *= runSpeed;
        }
        else {
            moveInput *= moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            // transform.localScale =  Vector3.Slerp(transform.localScale, crouchPos, 3f);
            //charCon.height /= 2;
           charCon.height =  Mathf.Lerp(charCon.height, 1, 5f);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) {
            //transform.localScale = Vector3.Slerp(crouchPos, standPos, 1f); ;
            charCon.height = Mathf.Lerp(charCon.height,   2, 5f);
        }
        //moveInput *= moveSpeed;

        moveInput.y = ystore;
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;


        if (charCon.isGrounded) {
            canJump = true;
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            //Handle Jumping
            if (Input.GetKeyDown(KeyCode.Space) && canJump) {
                moveInput.y = jumpPower;
                canJump = false;
               
            }
        }

        //Mouse Input
        //Controll Camera Roatation because mouse move in 2D use vector2
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        //convert 4 to 3 rotation (x, y, z, w) -> (x, y, z)
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        //ถ้าไม่ใส่บรรทัดนี้ มันจะหันขึ้นลงไม่ได้เพราะว่ามันหันแค่ตัวละครแต่ว่า camera point ไม่ได้หันด้วย กล้องเราจับที่ point ไม่ใช่ที่ตัวละคร
        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        charCon.Move(moveInput * Time.deltaTime);
    }
}
