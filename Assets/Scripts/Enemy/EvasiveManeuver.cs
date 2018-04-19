using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {
    [SerializeField]
    private float dodge;
    [SerializeField]
    private float smoothing;
    [SerializeField]
    private float tilt;
    [SerializeField]
    private Vector2 startWait;
    [SerializeField]
    private Vector2 maneuverTime;
    [SerializeField]
    private Vector2 maneuverWait;
    [SerializeField]
    private Boundary boundary;

    private Transform playerTransform;
    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;
    Vector3 velocity;
    Vector3 position;
    void Start () {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}
	
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            if (playerTransform != null) {
                targetManeuver = playerTransform.position.x;
                yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
                targetManeuver = 0;
                yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
            }
           
           
        }
    }

    private void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x,targetManeuver,Time.deltaTime * smoothing);
        velocity.x = newManeuver;
        velocity.y = 0.0f;
        velocity.z = currentSpeed;
        rb.velocity = velocity;

        position.x = Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax);
        position.y = 0.0f;
        position.z = Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax);
        rb.position = position;

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
