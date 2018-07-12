public class Player
{
    public string name;
    public int age;
    public int health;
    public int happiness;
    public int knowledge;
    public int social;
    public int love;
    public int actionPoint;
    public int actionPointLeft;
    public int numCards;
    public int money;
    public bool inRelationship;
    public bool married;
    public bool haveKid;
    public bool parentDied;
    public bool partnerDied;

    public Player(string name)
    {
        this.name = name;
        this.age = 1;
        this.health = 1;
        this.happiness = 1;
        this.knowledge = 0;
        this.social = 0;
        this.love = 0;
        this.money = 0;
        this.actionPoint = 1;
        this.numCards = 2;
        this.inRelationship = false;
        this.married = false;
        this.haveKid = false;
        this.parentDied = false;
        this.partnerDied = false;
        this.actionPointLeft = this.actionPoint;
    }
}
