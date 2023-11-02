using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform camara;
    CharacterController control;

    public float speedCam;
    public float playerSpeed;
    public float gravityForce;
    public float jumpForce;

    float camRotation = 0f;
    float gravityMove = 0f;


    // Start is called before the first frame update
    void Start()
    {
        camara = transform.GetChild(0).GetComponent<Transform>();
        control = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    
}
    // Update is called once per frame
    void Update()
    {
        // Asignar movimiento de mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(0, mouseX, 0) * speedCam * Time.deltaTime);
        camRotation -= mouseY * speedCam * Time.deltaTime;
        camRotation = Mathf.Clamp(camRotation, -80, 80);
        camara.localRotation = Quaternion.Euler(new Vector3(camRotation, 0, 0));

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = (transform.right * moveX + transform.forward * moveZ) * playerSpeed * Time.deltaTime;

        control.Move(movement);
        control.Move(new Vector3(0, gravityMove, 0) * Time.deltaTime);

        // Si no esta en el suelo añadimos gravedad para que caiga y no siga volando cuando salta 
        if(!control.isGrounded)
        {
            gravityMove += gravityForce;
        }
        else
        {
            gravityMove = 0f;
        }
        // si se preciona el espacio y el jugador esta tocando tierra(suelo) 
        if (Input.GetKeyDown(KeyCode.Space) && control.isGrounded)
        {
            gravityMove = jumpForce;
        }
    }
}
