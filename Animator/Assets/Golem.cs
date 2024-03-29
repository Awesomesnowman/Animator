using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Golem : MonoBehaviour
{
    public Animator myAnime;
    public Vector3 platform = new Vector3(0, 0, 0);
    public Vector3 lastDirection;
    public Rigidbody myRig;
    public float speed = .5f;
    public float jumpSpeed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        myAnime = GetComponent<Animator>();
        if (myRig == null)
            throw new System.Exception("Player controller needs rigidbody");

    }
    public void onMove(InputAction.CallbackContext ev)
    {
        if (ev.performed)
        {
            lastDirection = ev.ReadValue<Vector2>();
            myAnime.SetInteger("DIR", 1);
        }
        if (ev.canceled)
        {
            lastDirection = Vector2.zero;
            myAnime.SetInteger("DIR", 0);
        }
    }
    public void onJump(InputAction.CallbackContext ev)
    {
        if (ev.started)
        {
            myAnime.SetInteger("DIR", 3);
        }
        if (ev.canceled)
        {
            myAnime.SetInteger("DIR", 0);
        }
    }

    public void onAttack(InputAction.CallbackContext ev)
    {
        if (ev.started)
        {
            myAnime.SetInteger("DIR", 2);
        }
        if (ev.canceled)
        {
            myAnime.SetInteger("DIR", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        myRig.angularVelocity = new Vector3(0, lastDirection.x, 0) * speed;
        myRig.velocity = transform.forward * speed * lastDirection.y + new Vector3(0, myRig.velocity.y, 0) + platform;
    }
}