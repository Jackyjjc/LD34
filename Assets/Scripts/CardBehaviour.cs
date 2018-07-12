using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardBehaviour : MonoBehaviour {

    private GameManager gm;

    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Start () {
        this.toggled = false;
	}

    public Player player;
    public bool toggled;

    private Card cardBacking;
    public Card card
    {
        get { return cardBacking; }
        set {
            cardBacking = value;
            gameObject.transform.Find("Panel/Text").GetComponent<Text>().text = value.name;
            gameObject.transform.Find("Texts/Text").GetComponent<Text>().text = value.text;
            if (value.textTable.ContainsKey("discardText")) {
                GameObject discardTextObj = gameObject.transform.Find("Texts/DiscardText").gameObject;
                discardTextObj.SetActive(true);
                discardTextObj.GetComponent<Text>().text = value.textTable["discardText"];
            }
            gameObject.transform.Find("CardTypeText").GetComponent<Text>().text = value.stage;
        }
    }

    public void toggleCard()
    {
        if (gm.finished)
        {
            return;
        }

        Image image = gameObject.GetComponent<Image>();
        if (toggled)
        {
            if (player.actionPointLeft < player.actionPoint)
            {
                player.actionPointLeft++;
            }

            image.color = Color.white;
            toggled = false;

        } else if (!toggled && player.actionPointLeft > 0)
        {
            player.actionPointLeft--;
           
            image.color = Color.green;
            image.color = new Color(155/255f, 1, 116/255f);
            toggled = true;
        }
    }
}
