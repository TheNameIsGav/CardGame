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
    public GameObject grabbedCard;
    

    private GameObject dealPoint;
    private static System.Random rng = new System.Random();
    private Deck drawDeck;
    private Deck boardDeck;

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
    public int moves = 0;

    public class Deck
    {
        public List<Card> deck = new List<Card>();

        public Deck()
        {

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
        public bool inDeck = false;

        private float x, y, z;

        public GameObject card;

        public SuitEnum Suit { get { return suit; } }
        public int Rank { get { return rank; } }

        public Vector3 Pos { get { return new Vector3(x, y, z); } }

        

        public Card(SuitEnum rsuit, int rrank, Vector2 position)
        {
            string cardName = string.Format("{1} of {0}", rsuit, rrank);
            string assetName = string.Format("Cards/{0}/card_{0}_{1}", rsuit, rrank);
            GameObject asset = GameObject.Find("CardTemplate");

            card = Instantiate(asset, position, Quaternion.identity);
            front = Resources.Load<Sprite>(assetName);
            back = instance.cardBack;
            suit = rsuit;
            rank = rrank;
            card.name = cardName;
            card.GetComponent<SpriteRenderer>().sprite = front;
            card.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = back;

            card.transform.rotation = new Quaternion(0, 180, 0, 0);
            card.GetComponent<BoxCollider>().enabled = false;
        }

        public Card(Card incCard)
        {
            suit = incCard.suit;
            rank = incCard.rank;
            front = incCard.front;
            back = incCard.back;
            inDeck = incCard.inDeck;

            x = incCard.x;
            y = incCard.y;
            z = incCard.z;

            card = incCard.card;
            card.GetComponent<SpriteRenderer>().sprite = incCard.front;
            card.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = incCard.back;
            card.GetComponent<BoxCollider>().enabled = incCard.card.GetComponent<BoxCollider>().enabled;
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

    public void ResetGame() {
        drawDeck = new Deck();
        boardDeck = new Deck();
        moves = 0;
    }

    public bool playing = false;
    // Update is called once per frame
    void Update()
    {
        if(playing) SnapCardToPosition();
    }

    public void DealCard()
    {
        if (drawDeck.deck.Count != 0)
        {
            Card selected = drawDeck.deck[0];
            selected.card.GetComponent<BoxCollider>().enabled = true;
            boardDeck.deck.Add(new Card(selected));
            drawDeck.deck.RemoveAt(0);
            selected.inDeck = false;
            dealPoint = GameObject.Find("FlipPoint");
            selected.card.transform.position = dealPoint.transform.position;
            StartFlip(selected);
        }
    }

    void SnapCardToPosition()
    {
        //Range from -15 to 15
        //Want to decrease the distance per each card in the 
        float distBetween = 30 / (boardDeck.deck.Count == 0 ? 1 : boardDeck.deck.Count); //Don't ask why this is here
        for(int i = 0; i < boardDeck.deck.Count; i++)
        {
            GameObject curr = boardDeck.deck[i].card;
            Vector3 pos = curr.transform.position;

            curr.transform.position = new Vector3(distBetween * i - 15, -3, -.01f * i);
            curr.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1); ;
        }
    }
    
    public void SpawnDeck(Vector2 posToSpawn)
    {
        playing = true;
        drawDeck = new Deck(posToSpawn);
    }

    /// <summary>
    /// Call this function to flip a card, I.E. run the animation
    /// </summary>
    /// <param name="card"></param>
    public void StartFlip(Card card)
    {
        StartCoroutine(RotateCard(card));
    }

    private IEnumerator RotateCard(Card card)
    {
        WaitForSeconds wait = new WaitForSeconds(.0025f);
        for(int i = 0; i < 180; i++)
        {
            card.card.transform.Rotate(Vector3.up);
            yield return wait;
        }
    }

    private Card AssocCard(GameObject inc)
    {
        foreach(Card c in boardDeck.deck)
        {
            if(c.card == inc)
            {
                return c;
            }
        }

        return null;
    }

    public void setGrabbedCard(GameObject card)
    {
        grabbedCard = card;
        Card tmp = AssocCard(grabbedCard);
    }

    public void updateGrabbedCard(GameObject card)
    {
        CheckCardCollisions(card);
    }

    public void removeGrabbedCard()
    {
        GameObject r = CheckCardCollisions(grabbedCard);

        if(r) CheckGameLogic(grabbedCard, r);

        grabbedCard = null;
    }

    private int FindInDeck(List<Card> d, Card c)
    {
        for(int i = 0; i < d.Count; i++)
        {
            if(d[i] == c)
            {
                return i;
            }
        }
        return -1;
    }

    private void CheckGameLogic(GameObject a, GameObject b)
    {
        if (drawDeck.deck.Count == 0 || boardDeck.deck.Count == 1)
        {
            playing = false;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        } else { 
            Card grabbed = AssocCard(a);
            Card targeted = AssocCard(b);

            int PosOfA = FindInDeck(boardDeck.deck, grabbed);
            int PosOfB = FindInDeck(boardDeck.deck, targeted);

            Debug.Log(PosOfA);
            Debug.Log(PosOfB);

            if (PosOfA == PosOfB + 3 || PosOfA == PosOfB + 1)
            {
                if (grabbed.Rank == targeted.Rank || grabbed.Suit == targeted.Suit)
                {
                    moves++;
                    Debug.Log("Should Remove Card");
                    boardDeck.deck.Remove(grabbed);
                    boardDeck.deck[PosOfB] = grabbed;
                    GameObject.Destroy(targeted.card);
                }
            }
        }
    }

    private GameObject CheckCardCollisions(GameObject c)
    {

        RaycastHit hit;
        Physics.Raycast(c.transform.position, Vector3.forward * 10, out hit); 
        if(hit.transform)
        {
            GameObject tmp = hit.transform.gameObject;
            tmp.GetComponent<SpriteRenderer>().color = new Color(.8f, 1, 1);
            return hit.transform.gameObject;
        }
        return null;
    }


    private void FixedUpdate()
    {
        
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
        playing = false;
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
