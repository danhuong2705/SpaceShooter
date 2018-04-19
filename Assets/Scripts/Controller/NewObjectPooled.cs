using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectPooled : MonoBehaviour {
    public static NewObjectPooled current;
    [SerializeField]
    private GameObject bolt;
    [SerializeField]
    private GameObject boltEnemy;
    [SerializeField]
    private int boltAmount;
    [SerializeField]
    private int enemyAmout;
    [SerializeField]
    private GameObject[] hazards;
    [SerializeField]
    private int boltEnemyAmout;
    [SerializeField]
    private int bossBoltAmout;
    private List<GameObject> bolts;
    private List<GameObject> enemys;
    private List<GameObject> boltsEmeny;
    private List<GameObject> bossBolt;
	void Awake()
    {
        current = this;
    }

	void Start () {
        bolts = new List<GameObject>();
        enemys = new List<GameObject>();
        boltsEmeny = new List<GameObject>();
        bossBolt = new List<GameObject>();
        for (int i=0;i< boltAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bolt);
            obj.SetActive(false);
            bolts.Add(obj);
        }

        for(int i = 0; i < hazards.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(hazards[i]);
            obj.SetActive(false);
            enemys.Add(obj);
            
        }
        for(int i = 0; i < boltEnemyAmout; i++)
        {
           
            GameObject obj = (GameObject)Instantiate(boltEnemy);
            obj.SetActive(false);
            boltsEmeny.Add(obj);
        }
        for (int i = 0; i < bossBoltAmout; i++)
        {

            GameObject obj = (GameObject)Instantiate(boltEnemy);
            obj.SetActive(false);
            bossBolt.Add(obj);
        }
    }
	
    public List<GameObject> GetPooledObject()
    {
        int i = 0;
        List<GameObject>list = new List<GameObject>();
        while (i < bolts.Count)
        {
            if (!bolts[i].activeInHierarchy)
            {    
                GameObject obj = (GameObject)bolts[i];
                GameObject obj1 = (GameObject)bolts[i+1];
                GameObject obj2 = (GameObject)bolts[i+2];
                list.Add(obj);
                list.Add(obj1);
                list.Add(obj2);
                return list;

            }
            i += 3;
        }    
        
        return null;
    }


    public List<GameObject> GetBossBolt()
    {
        int i = 0;
        List<GameObject> list = new List<GameObject>();
        while (i < bossBolt.Count)
        {
            if (!bossBolt[i].activeInHierarchy)
            {
                GameObject obj = (GameObject)bossBolt[i];
                GameObject obj1 = (GameObject)bossBolt[i + 1];
                GameObject obj2 = (GameObject)bossBolt[i + 2];

                list.Add(obj);
                list.Add(obj1);
                list.Add(obj2);
               
                return list;

            }
            i += 3;
        }

        return null;
    }
    public GameObject GetBoltsEnemy()
    {
        for(int i = 0; i < boltsEmeny.Count; i++)
        {
            if(!boltsEmeny[i].activeInHierarchy)
            {
                return boltsEmeny[i];
            }      
        }
        return null;
    }
    public GameObject GetEnemys()
    { 
        while (true)
        {
            GameObject hazard = enemys[Random.Range(0, hazards.Length)];
            if (!hazard.activeInHierarchy)
            {
                return hazard;
            }
        }
    }
}
