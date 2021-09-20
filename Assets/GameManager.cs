using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(instance);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
       SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ViewRules()
    {
        SceneManager.LoadScene("RulesMenu", LoadSceneMode.Single);
    }

    public void ViewCardBacks()
    {
        SceneManager.LoadScene("CardBackMenu", LoadSceneMode.Single);
    }

    public void ViewMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
