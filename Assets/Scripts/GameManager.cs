using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController thePlayer;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformList;

    private ScoreManager theScoreManager;

    public Scroll scrollBackground;
    private Material[] scrollMaterial;
    private MeshRenderer scrollRen;
   


    public DeathMenu theDeathScreen;

    public bool powerUpReset;

	// Use this for initialization
	void Start () {

        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;

        

        theScoreManager = FindObjectOfType<ScoreManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RestartGame()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);
        
        theDeathScreen.gameObject.SetActive(true);

        scrollBackground.scrollSpeed = 1;

        // StartCoroutine("RestartGameCo");
    }
    public void Reset()
    {
        theDeathScreen.gameObject.SetActive(false);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
        scrollBackground.scrollSpeed = scrollBackground.scrollSpeedStore;
        //scrollRen = scrollBackground.GetComponent<MeshRenderer>();
        //scrollMaterial = scrollRen.materials;


        //scrollMaterial[0].mainTexture.wrapMode = TextureWrapMode.Clamp;
        
        //scrollMaterial[0].mainTexture.wrapMode = TextureWrapMode.Repeat;





        powerUpReset = true;
    }

    /*public IEnumerator RestartGameCo()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for(int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }*/
}
