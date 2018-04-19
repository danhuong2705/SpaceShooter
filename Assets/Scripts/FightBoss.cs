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
    private GameController gameController;
     Slider bloodSlider;
    // Use this for initialization
    void Start () {
        bloodSlider = GetComponent<Slider>();
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
        //if (explosions != null)
        //{
        //    Instantiate(explosions, transform.position, transform.rotation);

        //}
        if (bloodSlider.value <= 0)
        {
            Instantiate(playerExplosions, transform.position, transform.rotation);
            DestroyContact(gameObject);
        }
            if (other.tag == "Player")
        {
         
            Instantiate(playerExplosions, transform.position, transform.rotation);
            DestroyContact(gameObject);
            DestroyContact(other.gameObject);
            return;
          //  gameController.GameOver();
        }
       
      //  gameController.AddScore(scoreValue);
       
    }
}
