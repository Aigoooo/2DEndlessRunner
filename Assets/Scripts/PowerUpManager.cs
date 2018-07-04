using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    private bool doublePoints;
    public bool safeMode;

    private bool powerUpActive;

    public float powerUpTimeCounter;
    public float powerUpTimeCounter2;

    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;
    private GameManager theGameManager;

    private float normalPointsPerSecond;
    private float spikeRate; //bugged

    private PlatformDestroyer[] spikeList;

    



    // Use this for initialization
    void Start () {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();

        
		
	}
	
	// Update is called once per frame
	void Update () {
        if(powerUpActive)
        {
            powerUpTimeCounter = powerUpTimeCounter - Time.deltaTime;

            PowerUpReset();

            SafeModePowerUp();
            DoublePointsPowerUp();

            if(powerUpTimeCounter <= 0)
            {
                theScoreManager.pointsPerSecond = 5f;
                theScoreManager.shouldDouble = false;
                thePlatformGenerator.randomSpikeThreshold = 25f;
                safeMode = false;
                

                powerUpActive = false;
            }
        }		
	}
    public void PowerUpReset()
    {
        if (theGameManager.powerUpReset && powerUpTimeCounter > 0)
        {
            powerUpTimeCounter = 0f;
            theGameManager.powerUpReset = false;
        }
    }
    public void SafeModePowerUp()
    {
        if (safeMode)
        {
            thePlatformGenerator.randomSpikeThreshold = 0f;

        }

    }
    public void DoublePointsPowerUp()
    {
        if (doublePoints && !theScoreManager.shouldDouble)
        {
            theScoreManager.pointsPerSecond = normalPointsPerSecond * 10f;
            theScoreManager.shouldDouble = true;
        }
    }
    public void ActivatePowerUp (bool points, bool safe, float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerUpTimeCounter = time;

        normalPointsPerSecond = theScoreManager.pointsPerSecond;
        spikeRate = thePlatformGenerator.randomSpikeThreshold;

        if (safeMode)
        {
            spikeList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }

        powerUpActive = true;
    }
}
