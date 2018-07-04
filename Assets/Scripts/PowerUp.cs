using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public bool doublePoints;
    public bool safeMode;

    public float powerUpTime;

    private PowerUpManager thePowerUpManager;

    public Sprite[] powerUpSprites;

	// Use this for initialization
	void Start () {
        thePowerUpManager = FindObjectOfType<PowerUpManager>();
	}
	void Awake ()
    {
        int powerUpSelector = Random.Range(0, 2);

        switch(powerUpSelector)
        {
            case 0: doublePoints = true;
                break;

            case 1: safeMode = true;
                break;
        }

        GetComponent<SpriteRenderer>().sprite = powerUpSprites[powerUpSelector];

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            thePowerUpManager.ActivatePowerUp(doublePoints, safeMode, powerUpTime);
        }
        gameObject.SetActive(false);
    }

}
