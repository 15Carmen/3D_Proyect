using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Joystick joystickBolita;
    public Rigidbody rb;

    public float speed = 10f;

     void CharacterMove()
    {
        rb.velocity = new Vector3((joystickBolita.Horizontal * speed) + Input.GetAxis("Horizontal"),
                                    rb.velocity.y, joystickBolita.Vertical * speed + Input.GetAxis("Vertical"));
    }

    private void Update()
    {
        CharacterMove();
    
    }



}
