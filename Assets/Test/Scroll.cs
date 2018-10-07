using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    // public float speed = 0;
    // public float playerSpeed;
    public float scrollSpeed;
    public float scrollSpeedStore;
    public PlayerController playerController;
    // public GameObject playerController;

    public static Scroll current;

    float pos = 0;

    
    
    

	// Use this for initialization
	void Start () {
        current = this;
        scrollSpeedStore = scrollSpeed;
        //player = GameObject.Find("Player");
        playerController = FindObjectOfType<PlayerController>();

    }
    public void BackgroundScrollSpeed()
    {
        
        //playerController = FindObjectOfType<PlayerController>();
        pos += (playerController.moveSpeed / scrollSpeed);
        
       
    }
	
	// Update is called once per frame
	void Update () {

        // pos += (playerController.GetComponent<PlayerController>().moveSpeed / scrollSpeed);
        BackgroundScrollSpeed();
        if (pos < 1.0f)
        {
            pos -= 1.0f;
        }

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(pos, 0);
        

    }
}
