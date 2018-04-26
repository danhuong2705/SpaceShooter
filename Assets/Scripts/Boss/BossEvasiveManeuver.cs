using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvasiveManeuver : MonoBehaviour {
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
    [SerializeField]
    private float speed;

    private Transform playerTransform;
    private Vector3 targetManeuver;
    private Rigidbody rb;
    Vector3 velocity;
    Vector3 position;
    
    void Start () {
        rb = GetComponent<Rigidbody>();
       
        if (!GameController.current.gameOver)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        position.x = 0;
        position.y = 0;
        position.z = 16;
        rb.position = position;
        rb.velocity = new Vector3(5,0,5);
        rb.rotation = Quaternion.identity;
        StartCoroutine(Evade());
	}
	
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            if (playerTransform != null) {
                targetManeuver = playerTransform.position;
                yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            }
           
           
        }
    }

    private void FixedUpdate()
    {
        float z = playerTransform.position.z * -1/5;
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver.x, Time.deltaTime * smoothing);
        velocity.x = newManeuver;
        velocity.y = 0.0f;
        velocity.z = -z*2;
        rb.velocity = velocity;

        position.x = Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax);
        position.y = 0.0f;
        position.z = Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax);
        rb.position = position;
      
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
