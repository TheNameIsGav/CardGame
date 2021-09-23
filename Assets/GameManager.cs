using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum SuitEnum
    {
        Hearts = 1,
        Clubs = 2,
        Diamonds = 3,
        Spades = 4
    }
   

    public static GameManager instance;

    private static System.Random rng = new System.Random();
    private Deck gameDeck;
    private List<Card> hand;

    public static void Shuffle<T>(List<T> inc)
    {
        int n = inc.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = inc[k];
            inc[k] = inc[n];
            inc[n] = value;
        }
    }

    public Sprite cardBack;

    public class Deck
    {
        private List<Card> deck = new List<Card>();

        public Deck()
        {
            GenerateDeck(new Vector2());
            Shuffle();
        }

        public Deck(Vector2 inc)
        {
            GenerateDeck(inc);
            Shuffle();
        }
        
        public void GenerateDeck(Vector2 inc)
        {

            for(int rank = 1; rank <= 13; rank++)
            {
                deck.Add(new Card(SuitEnum.Hearts, rank, inc));
            }
            for(int rank = 1; rank <= 13; rank++)
            {
                deck.Add(new Card(SuitEnum.Clubs, rank, inc));
            }
            for(int rank = 1; rank <= 13; rank++)
            {
                deck.Add(new Card(SuitEnum.Diamonds, rank, inc));
            }
            for(int rank = 1; rank <= 13; rank++)
            {
                deck.Add(new Card(SuitEnum.Spades, rank, inc));
            }

        }

        public void Shuffle()
        {
            Shuffle<Card>(deck);
        }


        public Card TakeCard()
        {
            if(deck.Count == 0)
            {
                return null;
            }

            Card card = deck[0];
            deck.RemoveAt(0);
            return card;
        }
    }

    public class Card
    {
        private SuitEnum suit;
        private int rank;
        private Sprite front;
        private Sprite back;

        public bool isBack = true;

        public SuitEnum Suit { get { return suit; } }
        public int Rank { get { return rank; } }

        private GameObject card;

        public Card(SuitEnum rsuit, int rrank, Vector2 position)
        {
            string cardName = string.Format("{1} of {0}", rsuit, rrank);
            string assetName = string.Format("Cards/{0}/card_{0}_{1}", rsuit, rrank);
            Debug.Log(assetName);
            GameObject asset = GameObject.Find("CardTemplate");

            card = Instantiate(asset, position, Quaternion.identity);
            front = Resources.Load<Sprite>(assetName);
            back = instance.cardBack;
            suit = rsuit;
            rank = rrank;
            card.name = cardName;

            

        }

        public void flipCard()
        {
            if (isBack)
            {
                isBack = false;
                for(int i = 0; i < 180; i++)
                {
                    //Wait for .1 seconds
                    //Change rotation
                    //At 90 degrees, change image
                }
            } else
            {
                isBack = true;
            }
        }
    }

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

    public void SpawnDeck(Vector2 posToSpawn)
    {
        gameDeck = new Deck(posToSpawn);
    }



    //UI Methods
    public void UpdateCardBack(Sprite inc)
    {
        //Debug.Log("Recieved " + inc.name + " as the sprites name");
        cardBack = inc;
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
