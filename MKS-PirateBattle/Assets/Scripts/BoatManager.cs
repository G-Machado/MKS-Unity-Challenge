using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class BoatManager : MonoBehaviour
{
    [Header("Movement Variables")]
    public Rigidbody rb;
    public float moveSpeed= 1;
    private Vector3 moveVector;
    [Range(0, 1)]
    public float waterDamp = .8f;
    public NavMeshAgent agent;

    [Header("Rotation Variables")]
    public float rotSpeed = 10;
    private Vector3 rotVector;

    [Header("Bullets Variables")]
    public GameObject bulletPrefab;
    public Transform bulletForwardPoint;
    public Transform[] bulletSidePoints;
    public float bulletCD = 1;
    private float lastBulletTime;

    [Header("Boat Variables")]
    public int life = 10;
    public UnityEvent OnDeath;

    private void Start() {
        moveVector = transform.forward * moveSpeed;

        // Nav mesh agent configuration
        agent.updateRotation = false;
    }

    private void FixedUpdate() {
        
        // Damps the rotation by simulating water damp
        rotVector *= (1 - waterDamp);
        Rotate(Input.GetAxis("Horizontal"));

        // Damps the speed by simulating water damp
        float inputForward = Input.GetAxis("Vertical");
        if(inputForward > .1f)
            moveVector = transform.forward * moveSpeed * .1f * inputForward;
        agent.speed = moveSpeed *.1f;
        moveVector *= (1 - waterDamp);

        //rb.velocity = Vector3.Lerp(rb.velocity, moveVector, .2f);
        agent.SetDestination(transform.position + moveVector);
        Quaternion deltaRotation = Quaternion.Euler(rotVector * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if(Input.GetKeyDown(KeyCode.Space))
            ShootForward();
        if(Input.GetKeyDown(KeyCode.Tab))
            ShootSideways();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "bullet")
        {
            life -= 1;
            if(life <= 0) 
                OnDeath.Invoke();
        }
    }

    public void MoveForward() 
    {
        moveVector = transform.up * moveSpeed;
    }

    public void Rotate(float input)
    {
        rotVector = new Vector3(0, rotSpeed * 10 * Mathf.Clamp(input, -1, 1), 0);
        if(Mathf.Abs(input) > .1f)
            moveVector = transform.forward * moveSpeed * .05f * Mathf.Abs(input);
    }

    public void ShootForward()
    {
        if(Time.time - lastBulletTime < bulletCD) return;
        lastBulletTime = Time.time;

        GameObject bulletClone = (GameObject) Instantiate(bulletPrefab, bulletForwardPoint.position, Quaternion.Euler(90, 0, 0));
        bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * 5;

        Destroy(bulletClone, 2f);
    }

    public void ShootSideways()
    {
        if(Time.time - lastBulletTime < bulletCD) return;
        lastBulletTime = Time.time;

        for (int i = 0; i < bulletSidePoints.Length; i++)
        {
            GameObject bulletClone = (GameObject) Instantiate(bulletPrefab, bulletSidePoints[i].position, Quaternion.Euler(90, 0, 0));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.right * 5;
            Destroy(bulletClone, 2f);
        }
    }
}
