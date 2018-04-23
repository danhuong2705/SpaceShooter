using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightBoss : MonoBehaviour {
    [SerializeField]
    private GameObject explosions;
    [SerializeField]
    private GameObject playerExplosions;
    [SerializeField]
    private int scoreValue;
    public Slider bloodSlider;
    public static FightBoss current;
    private GameController gameController;
    void Awake()
    {
        current = this;
    }
    // Use this for initialization
    void Start () {
        bloodSlider = GetComponent<Slider>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController script'");
        }
    }

    // Update is called once per frame
    void DestroyContact(GameObject other)
    {
        other.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bolt"))
        {
            bloodSlider.value -= 1;
          
        }

        if (bloodSlider.value <= 0)
        {
            Instantiate(playerExplosions, transform.position, transform.rotation);
             DestroyContact(gameObject);
            if (!gameObject.activeInHierarchy)
               gameController.AddScore(500);

        }
            if (other.tag == "Player")
        {
         
           Instantiate(playerExplosions, transform.position, transform.rotation);
           DestroyContact(gameObject);
           DestroyContact(other.gameObject);
           gameController.GameOver();
        }
       
    }
}
