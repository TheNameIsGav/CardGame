using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("HighScore") > GameManager.instance.moves)
        {
            PlayerPrefs.SetInt("HighScore", GameManager.instance.moves);
            transform.GetChild(2).GetComponent<Text>().text = "NEW HIGH SCORE \n (Lower is better) \n" + GameManager.instance.moves;
        } else
        {
            transform.GetChild(2).GetComponent<Text>().text = "High Score \n (Lower is better) \n" + GameManager.instance.moves;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        GameManager.instance.ReturnToMainMenu();
    }

}
