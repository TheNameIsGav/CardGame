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
        public Sprite front;
        public Sprite back;

        private float x, y, z;

        public bool isBack = true;
        public int timer;

        public SuitEnum Suit { get { return suit; } }
        public int Rank { get { return rank; } }

        public GameObject GetCard { get { return card; } }

        public Vector3 Pos { get { return new Vector3(x, y, z); } }

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

    /// <summary>
    /// Call this function to flip a card, I.E. run the animation
    /// </summary>
    /// <param name="card"></param>
    public void StartFlip(Card card)
    {
        StartCoroutine(CalculateFlip(card));
    }

    public void flipCard(Card card)
    {
        if (card.isBack)
        {
            card.isBack = false;
            card.GetCard.transform.gameObject.GetComponent<SpriteRenderer>().sprite = card.front;
        }
        else
        {
            card.isBack = true;
            card.GetCard.transform.gameObject.GetComponent<SpriteRenderer>().sprite = card.back;
        }
    }

    IEnumerator CalculateFlip(Card card)
    {
        int timer = 0;
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(0.01f);
            card.GetCard.transform.Rotate(card.Pos);
            timer++;

            if (timer == 90 || timer == -90)
            {
                flipCard(card);
            }
        }
        timer = 0;
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
