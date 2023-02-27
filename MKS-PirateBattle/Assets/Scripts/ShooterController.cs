using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour 
{
    private PlayerController player;
    public BoatManager boat;
    public float shootingRange = 1;

    void Start()
    {
        player = PlayerController.instance;
    }

    void FixedUpdate()
    {
        if(GameloopManager.instance.currentState != GameloopManager.GameState.PLAY) return;
        if(player == null) return;

        if(Vector3.Distance(player.transform.position, boat.transform.position) > shootingRange)
        {
            boat.agent.updateRotation = true;
            boat.MoveDestination(player.transform.position);
        }
        else
        {
            boat.agent.updateRotation = false;

            Vector3 targetDir = player.transform.position - transform.position;
            float angleForward = Vector3.Angle(targetDir, transform.forward);
            float angleRight = Vector3.Angle(targetDir, transform.right);
            if (angleForward > 5f)
            {
                if(angleRight < 90)
                    boat.Rotate(1);
                else
                    boat.Rotate(-1);
            }

            boat.MoveDestination(transform.position);
            boat.ShootForward();
        }
    }
}
