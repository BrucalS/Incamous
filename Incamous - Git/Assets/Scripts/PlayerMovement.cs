using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] [Range(1, 10)] private int sensitivity = 1;
    [SerializeField] private float minimumVerticalRotation;
    [SerializeField] private float maximumVerticalRotation;

    private float rotX = 0.0f;
    private float xAxis;
    private float yAxis;

    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
           characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = Camera.main.transform.TransformDirection(moveDir);
        moveDir.y = 0;
        
        characterController.SimpleMove(moveDir * movementSpeed);

        xAxis = sensitivity * Input.GetAxis("Mouse X");
        yAxis = sensitivity * Input.GetAxis("Mouse Y");

        transform.Rotate(0, xAxis, 0);

        rotX -= yAxis;
        rotX = Mathf.Clamp(rotX, minimumVerticalRotation, maximumVerticalRotation);

        float rotY = transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(rotX, rotY, 0);        
   }
}
