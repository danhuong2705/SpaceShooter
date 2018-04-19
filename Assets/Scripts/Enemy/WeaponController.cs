using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    [SerializeField]
    GameObject shot;
    [SerializeField]
    private Transform shotSpawn;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float delay;
    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private GameObject enemyShip;


    private AudioSource audioSource;
    void Start () {
        audioSource = GetComponent<AudioSource>();
      

            InvokeRepeating("Fire",delay,fireRate);
        

    }
	
	void Fire()
    {
       
        if (enemyShip.activeInHierarchy)
        {
            GameObject obj = NewObjectPooled.current.GetBoltsEnemy();
            if (obj == null) return;
            obj.transform.position = shotSpawn.position;
            obj.transform.rotation = shotSpawn.rotation;
            obj.SetActive(true);
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            audioSource.clip = clip;
            audioSource.Play();
        }          
       
    }
}
