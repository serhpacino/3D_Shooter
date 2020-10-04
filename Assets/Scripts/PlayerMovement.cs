using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]//it will make sure that our object has component we need
public class PlayerMovement : MonoBehaviour
{
    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrength = 20f;
    public float verticalRotationLimit = 80;
    float forwardMovement;
    float sidewaysMovement;
    float verticalVelocity;
    float verticalRotation = 0;
    CharacterController cc;
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;//itll lock our cursor in the center of the screen
    }
    void Update()
    {
        //Rotating
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);
        //vertical rotating
        verticalRotation -= Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);//this function takes 3 arguments
                                                                                                        //First one is what variable we want to limit.Example:verticalRotation
                                                                                                        //Next is min value , that is "-verticalRotationLimit"
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        //Movement
        if(cc.isGrounded){ 
        forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
        sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;
        //Rush
        if (Input.GetKey(KeyCode.LeftShift))
        {
            forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
        } 
        }
        //Jump
        verticalVelocity += Physics.gravity.y*Time.deltaTime;//default Unity variable which set to around 10
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpStrength;
        }
        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);
        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }
}
