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
        if(GameloopManager.instance.currentState != GameloopManager.GameState.PLAY) return;

        boat.Rotate(Input.GetAxis("Horizontal"));
        boat.MoveForward(Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.S))
            boat.ShootForward();
        if(Input.GetKeyDown(KeyCode.Q))
            boat.ShootLeft();
        if(Input.GetKeyDown(KeyCode.E))
            boat.ShootRight();
    }
}
