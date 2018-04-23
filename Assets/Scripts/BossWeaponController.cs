using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController : MonoBehaviour {
    [SerializeField]
    GameObject shot;
    [SerializeField]
    private Transform[] shotSpawns;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float delay;
    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private GameObject enemyShip;
    private int[] arr = { -1, 0, 1 };
   

    private AudioSource audioSource;
    void Start () {
        audioSource = GetComponent<AudioSource>();
      

            InvokeRepeating("Fire",delay,fireRate);
        

    }
	
	void Fire()
    {
     
        if (enemyShip.activeInHierarchy)
        {
            List<GameObject> obj = NewObjectPooled.current.GetBossBolt();
            int count = 0;
            foreach (var shotSpawn in shotSpawns)
            {
                shotSpawn.eulerAngles = new Vector3(0, (arr[count] * 10)+180, 0);
                obj[count].transform.position = shotSpawn.position;
                obj[count].transform.rotation = shotSpawn.rotation;
                obj[count].SetActive(true);
                count++;
            }
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            audioSource.clip = clip;
            audioSource.Play();
        }          
       
    }
}
