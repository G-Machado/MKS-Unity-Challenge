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

    private void Awake() {
        // Nav mesh agent configuration
        agent.updateRotation = false;
    }

    private void FixedUpdate() {
        
        // Damps the rotation by simulating water damp
        rotVector *= (1 - waterDamp);

        // Damps the speed by simulating water damp
        agent.speed = moveSpeed *.1f;
        moveVector *= (1 - waterDamp);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "bullet")
        {
            life -= 1;
            if(life <= 0) 
                OnDeath.Invoke();
        }
    }

    public void MoveForward(float input) 
    {
        if(input > .1f)
            moveVector = transform.forward * moveSpeed * .1f * Mathf.Clamp(input, 0, 1);
        agent.SetDestination(transform.position + moveVector);
    }

    public void MoveDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void Rotate(float input)
    {
        rotVector = new Vector3(0, rotSpeed * 10 * Mathf.Clamp(input, -1, 1), 0);
        if(Mathf.Abs(input) > .1f)
            moveVector = transform.forward * moveSpeed * .05f * Mathf.Abs(input);

        Quaternion deltaRotation = Quaternion.Euler(rotVector * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
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
