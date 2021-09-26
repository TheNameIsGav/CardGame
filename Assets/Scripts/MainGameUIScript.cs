using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DealCardOnClick();
        }
        
    }

    public void DealCardOnClick()
    {
        GameManager.instance.DealCard();
    }

    public void DebugGameOver()
    {
        GameManager.instance.playing = false;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        GameManager.instance.ReturnToMainMenu();
    }
}
