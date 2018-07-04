using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlatformGenerator : MonoBehaviour {

    public GameObject thePlatform;
    // public GameObject[] thePlatforms;
    public Transform generationPoint;
    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;


    private float platformWidth;
    private float[] platformWidths;

    private int platformSelector;

    public ObjectPooler[] theObjectPools;

    private float minHeight;

    public Transform maxHeightPoint;

    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;
    public ObjectPooler coinPool;
    public float distanceBetweenCoins;

    public float randomSpikeThreshold;
    public ObjectPooler spikePool;

    public float powerUpHeight;
    public ObjectPooler powerUpPool;
    public float randomPowerUpThreshold;



	// Use this for initialization
	void Start () {

        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[theObjectPools.Length];

        for(int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
	}
  /*  public void SpawnCoinsOnPlatform()
    {
        if (Random.Range(0f, 100f) < randomCoinThreshold)
        {
            SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));


        }
    }*/
   
   /* public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin1 = coinPool.GetPooledObject();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        coin3.SetActive(true);


    }*/

    // Update is called once per frame
    
    void Update () {
		
        if(transform.position.x < generationPoint.position.x)
        {

            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            

            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            } else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            if(Random.Range(0f, 100f) < randomPowerUpThreshold)
            {
                GameObject newPowerUp = powerUpPool.GetPooledObject();

                newPowerUp.transform.position = transform.position + new Vector3(distanceBetween / 2f, Random.Range(1f, powerUpHeight), 0f);

                newPowerUp.SetActive(true);
            }


            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);
            // Instantiate(/*thePlatform*/thePlatforms.[platformSelector], transform.position, transform.rotation);
            
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);


            //SpawnCoinsOnPlatform();
            if (Random.Range(0f, 100f) < randomSpikeThreshold)
            {
                GameObject newSpike = spikePool.GetPooledObject();

                float spikeXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f);


                Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);

                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
                if (Random.Range(0f, 100f) < randomCoinThreshold)
                {
                    // SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));




                    GameObject coin1 = coinPool.GetPooledObject();
                    Vector3 startPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                    coin1.transform.position = startPosition;
                    coin1.SetActive(true);

                    GameObject coin2 = coinPool.GetPooledObject();
                    coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
                    coin2.SetActive(true);

                    GameObject coin3 = coinPool.GetPooledObject();
                    coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
                    coin3.SetActive(true);
                    try
                    {



                        if (coin1 || coin2 || coin3)
                        {
                            if (newSpike.GetComponent<Renderer>().bounds.Intersects(coin1.GetComponent<Renderer>().bounds) || (newSpike.GetComponent<Renderer>().bounds.Intersects(coin2.GetComponent<Renderer>().bounds)) || (newSpike.GetComponent<Renderer>().bounds.Intersects(coin3.GetComponent<Renderer>().bounds)))  //renderer.bounds.Intersects(obstacleInstance.renderer.bounds))
                            {

                                //newSpike.transform.position = new Vector3(spikeXPosition, 0.5f, 0f);
                                newSpike.SetActive(false);

                            }
                        }
                    }
                    catch(System.Exception E)
                    {
                        Debug.Log("Fuck!"+ E);
                    }

                    
                }

            }
            
           

            // SpawnCoinsOnPlatform();




            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }
        

	}

    
}
