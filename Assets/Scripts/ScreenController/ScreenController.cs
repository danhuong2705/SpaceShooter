using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour {

    public Transform target;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
      
    }

    void FixedUpdate()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
       
  
    }
}
