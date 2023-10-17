using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookP : MonoBehaviour
{
    float mouseX = 0f, mouseY =0f;
    float xRotation = 0f;
    float yRotation =0f;
    [SerializeField] float mouseSense = 0f;
    [SerializeField] Transform playerBody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void PlayerLook()
    {
        //get Mouse Input
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSense;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSense;
        xRotation -= mouseY *Time.deltaTime;
        yRotation += mouseX * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotate Cam and Player
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0f);
        playerBody.rotation = Quaternion.Euler(0f,yRotation,0f);
    }
    private void Update()
    {
       PlayerLook();
    }
}
