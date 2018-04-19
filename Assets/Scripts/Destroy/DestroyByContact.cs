using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    [SerializeField]
    private GameObject explosions;
    [SerializeField]
    private GameObject playerExplosions;
    [SerializeField]
    private int scoreValue;
    private GameController gameController;
  
    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("Cannot find 'GameController script'");
        }
    }
    public void OnEnable()
    {
        Invoke("Destroy", 4.5f);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    void DestroyContact(GameObject other)
    {
        other.gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
    //    {
    //        return;
    //    }
    //    if (explosions != null)
    //    {
    //            Instantiate(explosions, transform.position, transform.rotation);

    //    }
    //    if (other.tag == "Player")
    //    {
    //        Instantiate(playerExplosions, other.transform.position, other.transform.rotation);
    //        gameController.GameOver();
    //    }
    //    gameController.AddScore(scoreValue);
    //    DestroyContact(other.gameObject);
    //    DestroyContact(gameObject);
    //}

}
