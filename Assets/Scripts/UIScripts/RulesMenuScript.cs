using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("returning to main menu");
        GameManager.instance.ReturnToMainMenu();
    }
}