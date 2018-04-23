using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private Boundary boundary;
    [SerializeField]
    private float tilt;
    private Rigidbody rb;
    private  AudioSource audio;
    [SerializeField]
    private GameObject shot;
    [SerializeField]
    private Transform[] shotSpawns;
    [SerializeField]
    private float fireRate;
    private float nextFire;
    private int[] arr = { -1, 0, 1 };

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
       InvokeRepeating("Fire", 0.05f, 0.05f);
    }
    void Fire()
    {
        List<GameObject> obj = NewObjectPooled.current.GetPooledObject();
        if (obj == null) return;
        if (Input.GetButton("Fire1") && gameObject.activeInHierarchy)
        {
                   int count = 0;
                   foreach (var shotSpawn in shotSpawns)
                    {
                        shotSpawn.eulerAngles = new Vector3(0, arr[count] * 15, 0);
                        obj[count].transform.position = shotSpawn.position;
                        obj[count].transform.rotation = shotSpawn.rotation;
                        obj[count].SetActive(true);
                        count++;
                    }

                    audio.Play();
        }
        

    }


    Vector3 movement;
    Vector3 limitArea;
    Vector3 rotation;
    void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
        movement.x = moveHorizontal;
        movement.y = 0.0f;
        movement.z = moveVertical;
        rb.velocity = movement * speed;
        limitArea.x = Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax);
        limitArea.y = 0.0f;
        limitArea.z = Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax);
        rb.position = limitArea;
        rotation.x = 0.0f;
        rotation.y = 0.0f; ;
        rotation.z = rb.velocity.x * -tilt;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
