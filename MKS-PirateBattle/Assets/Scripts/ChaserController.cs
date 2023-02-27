using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : MonoBehaviour
{
    private PlayerController player;
    public BoatManager boat;

    void Start()
    {
        player = PlayerController.instance;
        boat.agent.updateRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player == null) return;

        /*
        Vector3 targetDir = boat.agent.nextPosition - transform.position;
        float angleForward = Vector3.Angle(targetDir, transform.forward);
        float angleRight = Vector3.Angle(targetDir, transform.right);

        if (angleForward > 20f)
        {
            if(angleRight < 90)
                boat.Rotate(1);
            else
                boat.Rotate(-1);
        }
        */
        
        //boat.MoveForward(1);

        boat.MoveDestination(player.transform.position);
    }
}
