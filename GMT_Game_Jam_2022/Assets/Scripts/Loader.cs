using Assets.Scripts.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject soundManager;
    //public GameObject uiManager;

    // Start is called before the first frame update
    void Start()
    { 
        if (SoundManager.instance == null)
            Instantiate(soundManager);
        if (GameManager.instance == null)
            Instantiate(gameManager);
       

        //  if ( uiManager == null)
        //      Instantiate(uiManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
