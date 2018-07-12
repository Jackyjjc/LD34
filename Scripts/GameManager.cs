using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {
    public GameObject cardPrefab;
    public GameObject cardEffectPrefab;
    public GameObject handArea;

    public Scrollbar scrollbar;
    public GameObject growUpButton;

    public Text storyArea;

    public GameObject statusPrefab;
    public GameObject playerDetailPanel;

    public bool finished;

    public bool tutorialEnabled;
    public int tutorialSection;

    public Sprite[] icons;
    public Dictionary<string, int> iconNameLookupTable = new Dictionary<string, int>
        {
            { "age", 0},
            { "health", 1},
            { "happiness", 2 },
            { "card", 3 },
            { "knowledge", 4 },
            { "social", 5 },
            { "love", 6 },
            { "money", 7 },
        };

    public Player player;
    private List<GameObject> cardsInDeck;
    private Dictionary<String, GameObject> statusList;

    private void setStartingHand()
    {
        if (!tutorialEnabled)
        {
            AddCard(new WellFed());
            AddCard(new TakenCare());
            AddCard(new InfancyFlu());
        } else
        {
            AddCard(new Tutorial());
            AddCard(new Tutorial1());
        }
    }

    private void AddCard(Card card)
    {
        GameObject cardObj = Instantiate(cardPrefab);
        cardObj.GetComponent<CardBehaviour>().card = card;
        this.cardsInDeck.Add(cardObj);
    }

    // Use this for initialization
    void Start()
    {
        tutorialEnabled = true;
        tutorialSection = 0;

        this.cardsInDeck = new List<GameObject>();
        setStartingHand();
        this.dealing = true;
        this.player = new Player("John");
        AddStoryLine(string.Format("Let me tell you the life of {0}.", this.player.name));
        AddStoryLine("He was borned as a healthy boy.");

        statusList = new Dictionary<string, GameObject>();
        CreatePlayerStatus("card");
        CreatePlayerStatus("age");
        CreatePlayerStatus("health");

        finished = false;
    }

    private void AddStoryLine(string line)
    {
        if (line.Length > 0)
        {
            storyArea.text += line + "\n";
        }
    }

    private void CreatePlayerStatus(string name)
    {
        GameObject status = Instantiate(statusPrefab);
        int index = iconNameLookupTable[name];
        Sprite sprite = icons[index];
        status.GetComponentInChildren<Image>().sprite = sprite;
        status.transform.SetParent(playerDetailPanel.transform, false);
        statusList.Add(name, status);
    }

    private void RemovePlayerStatus(string name)
    {
        GameObject status = statusList[name];
        statusList.Remove(name);
        GameObject.Destroy(status);
    }

    private void SetPlayerStatus(string name, int newValue)
    {
        SetPlayerStatus(name, newValue.ToString());
    }

    private void SetPlayerStatus(string name, string newValue)
    {
        if (!statusList.ContainsKey(name))
        {
            return;
        }

        Text valueText = statusList[name].GetComponentInChildren<Text>();
        valueText.text = newValue;
    }

    private bool dealing;

    // Update is called once per frame
    void Update () {
	    if (!finished && dealing && handArea.transform.childCount < player.numCards && cardsInDeck.Count > 0)
        {
            GameObject c = cardsInDeck[UnityEngine.Random.Range(0, cardsInDeck.Count)];
            cardsInDeck.Remove(c);
            
            c.GetComponent<CardBehaviour>().player = player;
            c.transform.SetParent(handArea.transform, false);
            c.transform.localScale = new Vector2(1, 1);
        } else
        {
            dealing = true;
        }
        
        growUpButton.GetComponent<Button>().interactable = player.actionPointLeft == 0;

        SetPlayerStatus("card", (player.actionPoint - player.actionPointLeft) + " / " + player.actionPoint);
        SetPlayerStatus("age", player.age);
        SetPlayerStatus("health", player.health);
        SetPlayerStatus("happiness", player.happiness);
        SetPlayerStatus("knowledge", player.knowledge);
        SetPlayerStatus("social", player.social);
        SetPlayerStatus("love", player.love);
        SetPlayerStatus("money", player.money);
    }

    public void GrowUp()
    {
        if (finished)
        {
            return;
        }

        if (player.actionPointLeft > 0)
        {
            return;
        }

        if (tutorialEnabled)
        {
            tutorialSection++;
            foreach (CardBehaviour c in handArea.GetComponentsInChildren<CardBehaviour>())
            {
                GameObject.Destroy(c.gameObject);
            }
            if (tutorialSection == 1)
            {
                player.numCards = 2;
                AddCard(new Tutorial2());
                AddCard(new Tutorial3());
            }
            else if (tutorialSection == 2)
            {
                player.numCards = 2;
                AddCard(new Tutorial4());
                AddCard(new Tutorial5());
            }
            else if (tutorialSection == 3)
            {
                player.numCards = 2;
                AddCard(new Tutorial6());
                AddCard(new Tutorial7());

            } else if (tutorialSection == 4)
            {
                player.numCards = 1;
                AddCard(new Tutorial8());
            }
            else if (tutorialSection == 5)
            {
                player.numCards = 2;
                tutorialEnabled = false;
                setStartingHand();
            }
            player.actionPointLeft = player.actionPoint;
            dealing = true;
            return;
        }

        // Find all the cards the user picked
        CardBehaviour[] cardsInHand = handArea.GetComponentsInChildren<CardBehaviour>();

        IOrderedEnumerable<Card> toggledCards = cardsInHand.Where(c => c.toggled).Select(cb => cb.card).OrderBy(c => c.sort);
        IOrderedEnumerable<Card> discardCards = cardsInHand.Where(c => !c.toggled).Select(cb => cb.card).OrderBy(c => c.sort);

        foreach (Card c in toggledCards)
        {
            shuffleToDeck(c.Activate(player, this));
            AddStoryLine(string.Format(c.activateText, player.name, player.age));

            if (player.health <= 0)
            {
                if (c.textTable.ContainsKey("death"))
                {
                    AddStoryLine(c.textTable["death"]);
                }
                finished = true;
                return;
            }
        }

        foreach (Card c in discardCards)
        {
            shuffleToDeck(c.Discard(player, this));
            if (c.textTable.ContainsKey("discard"))
            {
                AddStoryLine(c.textTable["discard"]);
            }

            if (player.health <= 0)
            {
                if (c.textTable.ContainsKey("death"))
                {
                    AddStoryLine(c.textTable["death"]);
                }
                AddStoryLine(string.Format("He died at age {0} and this is the end of the story.", player.age));
                finished = true;
                return;
            }
        }

        List<CardBehaviour> notAdd = new List<CardBehaviour>();
        cardsInHand.ToList().ForEach(cb => {
            if (cb.toggled)
            {
                cb.toggleCard();
                if (cb.card.oneTime)
                {
                    notAdd.Add(cb);
                }
            }
            cb.transform.SetParent(null);
        });
        cardsInDeck.AddRange(cardsInHand.Where(cb => !notAdd.Contains(cb)).Select(cb => cb.gameObject));
        foreach(CardBehaviour cb in notAdd)
        {
            GameObject.Destroy(cb.gameObject);
        }

        player.age++;
        player.actionPointLeft = player.actionPoint;
        AddStoryLine(string.Format("{0} turns {1}!", player.name, player.age));
        //infancy
        if (player.age == 2) {
            // Early Childhood
            AddStoryLine(string.Format("Soon {0} entered his childhood.", player.name));
            CreatePlayerStatus("happiness");

            cardsInDeck.RemoveAll(cardObj => cardObj.GetComponent<CardBehaviour>().card.stage.Equals("Infancy"));
            AddCard(new ToddlerWellFed());
            AddCard(new ToddlerCold());
            AddCard(new PlayWithToy());
            AddCard(new Walk());
            AddCard(new LearnToSpeak());
        } else if (player.age == 6) {
            cardsInDeck.RemoveAll(cardObj => cardObj.GetComponent<CardBehaviour>().card.stage.Equals("Toddler"));
            AddStoryLine(string.Format("{0} starts to go to school.", player.name));
            CreatePlayerStatus("knowledge");
            CreatePlayerStatus("social");

            player.numCards++;
            player.actionPoint++;

            AddCard(new WonderAboutTheWorld());
            AddCard(new PlayWithOtherKids());
            AddCard(new MakeFriends());
            AddCard(new MakeFriends());
            AddCard(new Fever());
            AddCard(new ChildWellFed());
            AddCard(new SweetTooth());
            AddCard(new BoringSchoolWork());
            AddCard(new EnjoyableSchoolWork());
            AddCard(new PlaySport());
            AddCard(new PlayVideoGame());

        } else if (player.age == 12) {
            cardsInDeck.RemoveAll(cardObj => cardObj.GetComponent<CardBehaviour>().card.stage.Equals("Child"));
            AddStoryLine(string.Format("{0} entered his adolescence.", player.name));
            // adolescence
            player.numCards++;
        } else if (player.age == 16) {
            CreatePlayerStatus("love");
            player.actionPoint++;
        } else if (player.age == 18) {
            cardsInDeck.RemoveAll(cardObj => cardObj.GetComponent<CardBehaviour>().card.stage.Equals("Teenager"));

            CreatePlayerStatus("money");
            // early adulthood
            AddStoryLine(string.Format("{0} entered his early adulthood.", player.name));
            player.numCards++;
            AddCard(new Work());
            AddCard(new Work());
            AddCard(new AdultMakeFriends());
            AddCard(new Gym());
            AddCard(new CouchPotato());
            AddCard(new LudumDare());
        } else if (player.age == 21) {
            AddCard(new Drinking());
            AddCard(new PartTimeWork());
        } else if (player.age == 24) {
            player.actionPoint++;
        } else if (player.age == 35) {
            // midlife
            AddStoryLine(string.Format("{0} entered his middle age.", player.name));
            player.actionPoint--;
            AddCard(new MidlifeCrisis());
            AddCard(new SocialNetworking());
        } else if (player.age == 55) {
            RemovePlayerStatus("money");
            // mature adulthood
            AddStoryLine(string.Format("{0} entered his mature adulthood.", player.name));
            player.actionPoint--;
        } else if (player.age == 60) {
            RemovePlayerStatus("social");
            AddCard(new LateEat());
            AddCard(new PlayWithGrandson());
            AddCard(new LateDrinking());
        } else if (player.age == 70) {
            RemovePlayerStatus("knowledge");
            cardsInDeck.RemoveAll(cardObj => cardObj.GetComponent<CardBehaviour>().card.stage.Equals("Adult"));
            // late adulthood
            AddCard(new LateCold());
            AddCard(new LateFever());
            AddCard(new ChronicDisease());
            AddStoryLine(string.Format("{0} entered his late adulthood.", player.name));
            player.actionPoint--;
        }

        if (player.age >= 16 && player.happiness < 0 && numInDeck("Depression") < 4)
        {
            AddCard(new Depression());
        }
        
        if (player.happiness > 0 && numInDeck("Depression") >= 2)
        {
            RemoveCard("Depression");
        }

        if (player.age >= 16 && !player.inRelationship && player.social > 5 && numInDeck("Fall in love") == 0)
        {
            AddCard(new FallInLove());
        }

        if (player.money < 0 && numInDeck("In Debt") == 0)
        {
            AddCard(new InDebt());
        }

        if (player.money > 0 && numInDeck("In Debt") > 0)
        {
            RemoveCard("In Debt");
        }

        if (player.love < 0 && ((player.married && numInDeck("Divorce") == 0) || (player.inRelationship && numInDeck("Breakup") == 0)))
        {
            AddCard(new PartnerCheating());
        }

        if (player.love >= 50 && !player.married && numInDeck("Marriage") == 0)
        {
            Debug.Log(player.married);
            AddCard(new Marriage());
        }

        if (player.age >= 35 && !player.parentDied && numInDeck("Parent Pass Away") == 0 && percentChance(20))
        {
            AddCard(new ParentPassAway());
        }

        if (player.age >= 60 && !player.partnerDied && numInDeck("Partner Pass Away") == 0 && percentChance(20))
        {
            AddCard(new PartnerPassAway());
        }

        if (player.age >= 60 && numInDeck("Romantic Sex") > 0)
        {
            RemoveCard("Romantic Sex");
        }

        if (player.age >= 70 && numInDeck ("Mediocre Sex") > 0)
        {
            RemoveCard("Mediocre Sex");
        }

        dealing = true;

        Debug.Log(string.Join(",", this.cardsInDeck.Select<GameObject, string>(g => g.GetComponent<CardBehaviour>().card.name).ToArray()));
    }

    public void RemoveCard(string card)
    {
        cardsInDeck.Remove(cardsInDeck.First(co => co.GetComponent<CardBehaviour>().card.name.Equals(card)));
    }

    public void RemoveRelationshipCards()
    {
        string[] relationshipCards = new string[] { "Anniversary", "FirstDate", "Date", "Argument", "Romantic Sex", "Mediocre Sex", "Breakup", "Partner Cheating", "Having A Kid", "Chore", "Divorce", "Partner Pass Away" };
        cardsInDeck.RemoveAll(cardObj => relationshipCards.Contains(cardObj.GetComponent<CardBehaviour>().card.name));
    }

    public int numInDeck(string cardName)
    {
        return cardsInDeck.Where(cobj => cobj.GetComponent<CardBehaviour>().card.name.Equals(cardName)).Count();
    }

    private void shuffleToDeck(Card[] cards)
    {
        if (cards != null && cards.Length > 0)
        {
            foreach (Card c in cards)
            {
                AddCard(c);
            }
        }
    }

    public static bool percentChance(int chance)
    {
        return UnityEngine.Random.Range(0, 100) < chance;
    }

    public static bool percentChance(float chance)
    {
        return UnityEngine.Random.Range(0, 100) < chance * 100;
    }
}
