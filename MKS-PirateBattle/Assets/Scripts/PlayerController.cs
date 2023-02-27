using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    void Awake()
    {
        instance = this;
    }

    public BoatManager boat;

    void FixedUpdate()
    {
        boat.Rotate(Input.GetAxis("Horizontal"));
        boat.MoveForward(Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
            boat.ShootForward();
        if(Input.GetKeyDown(KeyCode.Tab))
            boat.ShootSideways();
    }
}
