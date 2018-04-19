using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltsDestroy : MonoBehaviour {
    public static BoltsDestroy current;
    [SerializeField]
    private GameObject explosions;
    [SerializeField]
    private GameObject playerExplosions;
    [SerializeField]
    private int scoreValue;
    private GameController gameController;
   
    void Awake()
    {
        current = this;
    }
	public void OnEnable()
    {
        Invoke("Destroy",2f);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    void Destroy(Collider other)
    {
        other.gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    
}
