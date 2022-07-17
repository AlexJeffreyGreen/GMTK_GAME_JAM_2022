using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private RectTransform MainMenuPanel;
    [SerializeField]
    private RectTransform HowToPlayPanel;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HowToPlay()
    {
        this.MainMenuPanel.gameObject.SetActive(false);
        this.HowToPlayPanel.gameObject.SetActive(true);
    }

    public void Back()
    {
        this.HowToPlayPanel.gameObject.SetActive(false);
        this.MainMenuPanel.gameObject.SetActive(true);
    }
}
