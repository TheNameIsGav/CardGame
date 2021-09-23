using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSpawnPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SpawnDeck(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
