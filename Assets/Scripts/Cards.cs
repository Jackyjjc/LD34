using System.Collections.Generic;
public abstract class Card
{
    public int sort;
    public string name;
    public string text;
    public string stage;
    public bool oneTime;

    public Dictionary<string, string> textTable;

    public string activateText;
    public string[] effects;
    public Card(int sort, bool oneTime, string name, string text, string stage, string activateText)
    {
        this.sort = sort;
        this.oneTime = oneTime;
        this.name = name;
        this.text = text;
        this.stage = stage;
        this.activateText = activateText;
        this.textTable = new Dictionary<string, string>();
    }
    public abstract Card[] Activate(Player player, GameManager gm);
    public virtual Card[] Discard(Player player, GameManager gm) { return null; }
}

public class Tutorial : Card
{
    public Tutorial() : base(0, false, "Choose a card", "Each turn you have to choose a certain amount of cards to activate", "Tutorial", "")
    {
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

public class Tutorial1 : Card
{
    public Tutorial1() : base(0, false, "Activate a card", "Click the 'Grow up' button to activate the cards selected", "Tutorial", "")
    {
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

public class Tutorial2 : Card
{
    public Tutorial2() : base(0, false, "Stats", "Some of the effects changes the stats lists on the top right. Hover them to see more info.", "Tutorial", "")
    {
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

public class Tutorial3 : Card
{
    public Tutorial3() : base(0, false, "Card Effect", "Each card will have an effect when activated.", "Tutorial", "")
    {
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

public class Tutorial4 : Card
{
    public Tutorial4() : base(0, false, "The deck", "All cards in hand will go back to deck (except a few). You then draw a new hand.", "Tutorial", "")
    {
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

public class Tutorial5 : Card
{
    public Tutorial5() : base(0, false, "Negative Effect", "", "Tutorial", "")
    {
        textTable["discardText"] = "The effect in red activates when you don't choose the card.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

public class Tutorial6 : Card
{
    public Tutorial6() : base(0, false, "Goal", "Play as long as possible :P", "Tutorial", "")
    {}
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

public class Tutorial7 : Card
{
    public Tutorial7() : base(0, false, "End Condition", "Health reaches 0", "Tutorial", "")
    {}
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

public class Tutorial8 : Card
{
    public Tutorial8() : base(0, false, "Enjoy!", "@jackyjjc LD34 submission", "Tutorial", "")
    { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        return null;
    }
}

// Infancy cards

public class WellFed : Card
{
    public WellFed() : base(0, false, "Well Fed", "\u0081 + 1", "Infancy", "He was very well fed by his parents.") {
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health++;
        return null;
    }
}

public class TakenCare : Card
{
    public TakenCare() : base(0, false, "Taken Care", "\u0081  + 1", "Infancy", "His parents has taken a great care of him when he was a baby.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health++;
        return null;
    }
}

public class InfancyFlu : Card
{
    public InfancyFlu() : base(1, false, "Flu", "\u0081  - 1", "Infancy", "He caught a flu at a time which made him weaker.")
    {
        textTable["death"] = "Unfortunately, he died from the flu since he is still an infant.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health--;
        return null;
    }
}

// Toddler cards
public class ToddlerWellFed : Card
{
    public ToddlerWellFed() : base(0, false, "Well Fed", "\u0081  + 1", "Toddler", "His parent cook him very nice porridge.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health++;
        return null;
    }
}

public class ToddlerCold : Card
{
    public ToddlerCold() : base(1, false, "Common Cold", "\u0081  - 1", "Toddler", "He caught a cold when playing outside in the garden.")
    {
        textTable["death"] = "The cold got worse and took his life.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health--;
        return null;
    }
}

// Toddler play
public class PlayWithToy : Card
{
    public PlayWithToy() : base(0, false, "Play with toy", "\u0080  + 1\nShuffle 'Choking' into deck", "Toddler", "He played with toys his parents bought him.")
    { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness++;
        return null;
    }
}

public class Chock : Card
{
    public Chock() : base(1, false, "Choking", "\u0080  - 1", "Infancy", "He choke on something when playing with the toys.\nHis parent immediately took him to the hospital.")
    {
        textTable["death"] = "He died from choking on something.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health--;
        return null;
    }
}

public class Walk : Card
{
    public Walk() : base(0, true, "Learn to walk", "\u0081  + 1\nShuffle 'Injury' into deck", "Toddler", "He had learned how to walk at age {1}.")
    {
        textTable["discard"] = "He still don't know how to walk. His parent starts to worry.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness++;
        return new Card[] { new ToddlerInjury() };
    }
}

public class ToddlerInjury : Card
{
    public ToddlerInjury() : base(0, false, "Injury", "\u0080  - 1", "Toddler", "He fell down and hurted himself when he was {1}.")
    {
        textTable["death"] = "The injury was not treated promptly and it got worse. The infection on the wound took his life.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health--;
        return new Card[] { };
    }
}

public class LearnToSpeak : Card
{
    public LearnToSpeak() : base(0, true, "Learned to speak", "\u0081  + 1\nShuffle 'Wonder about the world' into deck", "Toddler", "He started to pick up speaking. His parents are full of joys.")
    {
        textTable["discard"] = "He still don't know how to speak. His parent is a bit worried.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness++;
        return new Card[] { new WonderAboutTheWorld() };
    }
}

// Child
public class Fever : Card
{
    public Fever() : base(1, false, "Fever", "\u0080  - 1", "Teenager", "He caught a fever.")
    {
        textTable["death"] = "Unfortunately, he died from a served fever.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health -= 1;
        return null;
    }
}

public class ChildWellFed : Card
{
    public ChildWellFed() : base(0, false, "Well Fed", "\u0080  + 1\n10% chance shuffle 'Obesity' into deck", "Teenager", "His parent cook delicious food every day.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health++;
        if (GameManager.percentChance(10))
        {
            return new Card[] { new Obesity() };
        }
        return null;
    }
}

public class SweetTooth: Card
{
    public SweetTooth() : base(1, false, "Sweet Tooth", "\u0081  + 1\n30% chance shuffle 'Obesity' into deck", "Teenager", "He likes to eat a lot of sweet."){ }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness++;
        if (GameManager.percentChance(30))
        {
            return new Card[] { new Obesity() };
        }
        return null;
    }
}

public class Obesity : Card
{
    public Obesity() : base(1, false, "Obesity", "\u0081  - 1, Social -1", "Teenager", "His friends called him fat."){ }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness--;
        player.social--;
        return null;
    }
}

public class WonderAboutTheWorld : Card
{
    public WonderAboutTheWorld() : base(0, false, "Curious", "Knowledge +1\n10% chance shuffle 'Impatient Parent' into deck", "Child", "He started asking questions about everything around him.")
    {}
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.knowledge++;
        return new Card[] { new ImpatientParent() };
    }
}

public class ImpatientParent : Card
{
    public ImpatientParent() : base(1, false, "Impatient Parent", "\u0081  - 1", "Child", "He felt like his parents don't want to deal with him anymore."){}
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness--;
        return null;
    }
}

public class PlayWithOtherKids : Card
{
    public PlayWithOtherKids() : base(0, false, "Play with others", "Happiness +1, Social+1\nShuffle 1x 'Make friends' and 1x 'Angry parent' into deck", "Child", "He played with the other kids a lot."){ }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.social++;
        player.happiness++;
        return new Card[] { new MakeFriends(), new AngryParent() };
    }
}

public class MakeFriends : Card
{
    public MakeFriends() : base(0, false, "Make friends", "Social +2\n20% chance shuffle 'Best friend' into deck", "Teenager", "He made some friends."){ }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.social+=2;
        if (GameManager.percentChance(20) && gm.numInDeck("Make friends") == 0)
        {
            return new Card[] { new TeenagerBestFriend() };
        }
        return null;
    }
}

public class PlaySport : Card
{
    public PlaySport() : base(0, false, "Play Sports", "Social +1, Health +1\n15% Shuffle 'Angry Parent' into the deck.", "Teenager", "He played sports regularly with his friends."){ }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.social++;
        player.health++;
        if (GameManager.percentChance(15))
        {
            return new Card[] { new AngryParent() };
        }
        return null;
    }
}

public class PlayVideoGame : Card
{
    public PlayVideoGame() : base(0, false, "Play video game", "Happiness +3, Social -1\n40% Shuffle 'Angry Parent' into the deck.", "Teenager", "He played sports regularly with his friends.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.social --;
        player.health +=3;
        if (GameManager.percentChance(40))
        {
            return new Card[] { new AngryParent() };
        }
        return null;
    }
}

public class TeenagerBestFriend : Card
{
    public TeenagerBestFriend() : base(0, false, "Best friend", "Happiness +1, Social +1", "Teenager", "He made a best friend.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness++;
        player.social++;
        return null;
    }
}

public class BoringSchoolWork : Card
{
    public BoringSchoolWork() : base(1, false, "School Work", "Happiness -1, Knowledge +1", "Teenager", "{0} had to endure the school work.")
    {
        textTable["discardText"] = "50% chance shuffle 'Angry parent' into deck.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness--;
        player.knowledge++;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        if (GameManager.percentChance(50))
        {
            return new Card[] { new AngryParent() };
        }
        return null;
    }
}

public class EnjoyableSchoolWork : Card
{
    public EnjoyableSchoolWork() : base(1, false, "Enjoy school", "Social -1, Knowledge +1", "Teenager", "{0} enjoyed his school work. People at school thinks he is a nerd.")
    {
        textTable["discardText"] = "20% chance shuffle 'Angry parent' into deck.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.social--;
        player.knowledge++;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {

        if (GameManager.percentChance(20))
        {
            return new Card[] { new AngryParent() };
        }
        return null;
    }
}

public class AngryParent : Card
{
    public AngryParent() : base(1, false, "Angry parent", "Happiness -1", "Teenager", "He pissed off his parents sometimes.")
    {
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness--;
        return null;
    }
}

public class Depression : Card
{
    public Depression() : base(1, false, "Depression", "Happiness = 0\n 50% chance shuffle 'Suicide' into deck.", "Adult", "He got depressed.")
    {
        textTable["discardText"] = "Happiness -1";
        textTable["discard"] = "He felt like he was drowning in his depressing thoughts";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness = 0;
        if (GameManager.percentChance(50))
        {
            return new Card[] { new Suicide() };
        }
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness--;
        return null;
    }
}

public class Suicide : Card
{
    public Suicide() : base(1, false, "Suicide", "Health = 0", "Adult", "")
    {
        textTable["death"] = "He committed suicide.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health = 0;
        return null;
    }
}

public class FallInLove : Card
{
    public FallInLove() : base(0, true, "Fall in love", "love +10, happiness +5\nShuffle 'Relationship' cards into deck", "Adult", "{1} fell in love. He is so happy that he felt like he was in heaven.")
    {
        textTable["discardText"] = "Social -1, happiness -2";
        textTable["discard"] = "He was ashamed of himself because he couldn't confest his feelings.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love += 10;
        player.happiness += 5;
        player.inRelationship = true;
        return new Card[] { new FirstDate(), new GiftPlanning(), new RelationshipArgument() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.social--;
        player.happiness -= 2;
        return null;
    }
}

public class GiftPlanning : Card
{
    public GiftPlanning() : base(0, false, "Anniversary", "love +5", "Adult", "{1} was having an anniversary with his lover.")
    {
        textTable["discardText"] = "Love -10";
        textTable["discard"] = "His lover was very upset that he forgot their anniversary.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love += 5;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.love -= 10;
        return null;
    }
}

public class FirstDate : Card
{
    public FirstDate() : base(0, true, "FirstDate", "Love +10, happiness +3\nShuffle 'Date' cards into deck", "Adult", "{1} had his first date and it was wonderful.")
    {
        textTable["discardText"] = "love -10, happiness -3. Shuffle 'Breakup' into the deck";
        textTable["discard"] = "He missed his first date date. His lover was very upset.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love += 10;
        player.happiness += 5;
        return new Card[] { new Dating() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.social--;
        player.happiness -= 2;
        return new Card[] { new Breakup() };
    }
}

public class Dating : Card
{
    public Dating() : base(0, false, "Date", "Love +10, Happiness +1\n40% chance shuffle 'Romantic Sex' cards into the deck", "Adult", "{1} went on a date again with his lover.")
    {
        textTable["discardText"] = "Love -10, Happiness -3. Shuffle 'Breakup' into the deck";
        textTable["discard"] = "He missed his first date date. His lover was very upset.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love += 10;
        player.happiness += 5;
        if (GameManager.percentChance(30) && gm.numInDeck("Romantic Sex") == 0)
        {
            return new Card[] { new RomanticSex() };
        }
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.social--;
        player.happiness -= 2;
        return new Card[] { new Breakup() };
    }
}

public class RelationshipArgument : Card
{
    public RelationshipArgument() : base(1, false, "Argument", "Love -1, Happiness -1\n1% shuffle 'Breakup' into deck", "Adult", "")
    {
        textTable["discardText"] = "50% chance Love -20";
        textTable["discard"] = "His lover doesn't like him avoiding each other.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love --;
        player.happiness--;

        List<Card> shuffleInto = new List<Card>();
        if (GameManager.percentChance(1))
        {
            shuffleInto.Add(new Breakup());
        }
        return shuffleInto.ToArray();
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        if (GameManager.percentChance(50))
        {
            player.happiness -= 10;
        }
        return null;
    }
}

public class RomanticSex : Card
{
    public RomanticSex() : base(0, false, "Romantic Sex", "Love +5, Happiness +5\n20% chance shuffle 'Mediocre Sex'", "Adult", "'Sex is such a wonderful thing!' {0} thought.")
    {
        textTable["discardText"] = "Love -10, Happiness -3. 5% chance shuffle 'Breakup' into the deck";
        textTable["discard"] = "He rejected his lover's sex request.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love += 5;
        player.happiness += 5;
        if (GameManager.percentChance(20) && gm.numInDeck("Mediocre Sex") == 0)
        {
            return new Card[] { new MediocreSex() };
        }
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.love -= 10;
        player.happiness -= 3;
        if (!player.married && GameManager.percentChance(5))
        {
            return new Card[] { new Breakup() };
        }
        return null;
    }
}

public class MediocreSex : Card
{
    public MediocreSex() : base(0, false, "Mediocre Sex", "love -5, happiness -1", "Adult", "{0} was not in the mood for sex.")
    {
        textTable["discardText"] = "love -10, happiness -2. 5% chance shuffle 'Breakup' into the deck";
        textTable["discard"] = "His lover looked quite upset when he brushed off the request.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love -= 5;
        player.happiness --;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.love -= 10;
        player.happiness -= 2;
        if (!player.married && GameManager.percentChance(5))
        {
            return new Card[] { new Breakup() };
        }
        return null;
    }
}

public class Breakup : Card
{
    public Breakup() : base(1, true, "Breakup", "Love = 0, Happiness -5\nSuffle 'Fall In Love' to the deck", "Adult", "")
    {
        textTable["discardText"] = "Happiness -3";
        textTable["discard"] = "He couldn't bring himself to ask for a breakup.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love = 0;
        player.happiness -= 5;
        player.inRelationship = false;
        gm.RemoveRelationshipCards();
        return new Card[] { new FallInLove() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness -= 3;
        return null;
    }
}

public class Work : Card
{
    public Work() : base(0, false, "Work", "money +3\n10% chance shuffle 'Promotion' cards into the deck (if knowledge >= 8)", "Adult", "{0} went to work like everyone else.")
    {
        textTable["discardText"] = "money -3, happiness -1.";
        textTable["discard"] = "His boss was very angry that he missed work";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.money += 3;
        if (player.knowledge >= 8 && GameManager.percentChance(10) && gm.numInDeck("Promotion") == 0)
        {
            return new Card[] { new Promotion() };
        }
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.money -= 3;
        player.happiness --;
        return null;
    }
}

public class PartTimeWork : Card
{
    public PartTimeWork() : base(0, false, "PartTime Work", "money +5\n5% chance shuffle 'Promotion' cards into the deck", "Adult", "{0} was doing some part time work in his own time.")
    {}
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.money += 5;
        if (GameManager.percentChance(5) && gm.numInDeck("Promotion") == 0)
        {
            return new Card[] { new Promotion() };
        }
        return null;
    }
}

public class SeniorWork : Card
{
    public SeniorWork() : base(0, false, "Senior Work", "money +10\n5% chance shuffle 'Promotion' (if knowledge >= 15) and 2% shuffle 'BurnOut' card into the deck", "Adult", "{0} went to work like everyone else. Except he earned a bit more now.")
    {
        textTable["discardText"] = "money -5, happiness -3.";
        textTable["discard"] = "His boss was very angry that he missed work. Given that he is a senior employee.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.money += 10;
        List<Card> cards = new List<Card>();
        if (player.knowledge >= 15 && GameManager.percentChance(5) && gm.numInDeck("Senior Promotion") == 0)
        {
            cards.Add(new SeniorPromotion());
        }

        // FIXME: fix
        if (GameManager.percentChance(2))
        {
            cards.Add(new BurnOut());
        }
        return cards.ToArray();
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.money -= 10;
        player.happiness -= 3;
        return null;
    }
}

public class SeniorPromotion : Card
{
    public SeniorPromotion() : base(0, true, "Senior Promotion", "money +20\nShuffle 'Managment Work' cards into the deck", "Adult", "{0} got promoted to a management role.")
    {
        textTable["discardText"] = "happiness -5.";
        textTable["discard"] = "He is super upset that he missed out a great promotion opportunity";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.money += 20;
        return new Card[] { new ManagementWork(), new ManagementWork() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness -= 5;
        return null;
    }
}

public class ManagementWork : Card
{
    public ManagementWork() : base(0, false, "Management Work", "money +15", "Adult", "{0} felt that he was different from everyone since he was a manager.")
    {
        textTable["discardText"] = "money -10, happiness -5.";
        textTable["discard"] = "His boss was very angry that he missed work. Given that he is a manager.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.money += 15;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.money -= 10;
        player.happiness -= 5;
        return null;
    }
}

public class Promotion : Card
{
    public Promotion() : base(0, true, "Promotion", "money +5\nShuffle 2x'Senior Work' cards into the deck", "Adult", "{0} got promoted to a senior role.")
    {
        textTable["discardText"] = "happiness -3.";
        textTable["discard"] = "He is very upset that he missed out a promotion opportunity";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.money += 5;
        return new Card[] { new SeniorWork(), new SeniorWork() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness -= 3;
        return null;
    }
}

public class AdultMakeFriends : Card
{
    public AdultMakeFriends() : base(0, false, "Make friends", "Social +3, Money -1", "Adult", "He made some friends.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.social += 3;
        player.money--;
        return null;
    }
}

public class SocialNetworking : Card
{
    public SocialNetworking() : base(0, false, "Social Networking", "Social +3, Knowledge +2", "Adult", "He went to some social networking events for the company.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.social += 3;
        player.knowledge +=2;
        return null;
    }
}

public class CouchPotato : Card
{
    public CouchPotato() : base(0, false, "Couch Potato", "Social -1, Happiness +3", "Adult", "He watched a lot of tv.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.social --;
        player.happiness +=3;
        return null;
    }
}

public class LudumDare : Card
{
    public LudumDare() : base(0, false, "Ludum Dare", "Happiness +3", "Adult", "He participated in a Ludum Dare game jam.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness += 3;
        return null;
    }
}

public class BurnOut : Card
{
    public BurnOut() : base(1, true, "Burned Out", "Happiness -3, One less action this turn.", "Adult", "He burned out at work.")
    {
        textTable["discardText"] = "Two less action this turn";
        textTable["discard"] = "He is seriously burned out, he couldn't work efficiently.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness -= 3;
        player.actionPointLeft--;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.actionPointLeft -= 2;
        return null;
    }
}

public class InDebt : Card
{
    public InDebt() : base(1, true, "In Debt", "Money = 5, Happiness -3, Love -5\nShuffle 'Argument' into deck", "Adult", "He was in debt but his parent paid him out")
    {
        textTable["discardText"] = "Money -1, Happiness -2, Love -10";
        textTable["discard"] = "He is in debt and that is making his life difficult";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.money = 5;
        player.happiness -= 3;
        player.love -= 5;
        if ((player.inRelationship || player.married) && !player.partnerDied)
        {
            return new Card[] { new RelationshipArgument() };
        }
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.money--;
        player.happiness -= 2;
        player.love -= 10;
        return null;
    }
}

public class PartnerCheating : Card
{
    public PartnerCheating() : base(1, true, "Partner Cheating", "Love = 0, Happiness -3\n50% chance shuffle 'Breakup' or 'Divorce' into the deck", "Late Adult", "He found out his partner was cheating on him, he was devastated.")
    {
        textTable["discardText"] = "Happiness -2\nShuffle 'Breakup' or 'Divorce' into the deck";
        textTable["discard"] = "He didn't want to deal with this relationship anymore";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love = 0;
        if (GameManager.percentChance(50))
        {
            if (player.married)
            {
                return new Card[] { new Divorce() };
            } else
            {
                return new Card[] { new Breakup() };
            }
        }
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness -= 2;
        if (player.married)
        {
            return new Card[] { new Divorce() };
        }
        else
        {
            return new Card[] { new Breakup() };
        }
    }
}

public class Gym : Card
{
    public Gym() : base(0, false, "Gym", "Health +1, Money -1", "Adult", "He went to the gym.") {
        textTable["discardText"] = "15% chance of obesity";
        textTable["discard"] = "He is very upset that he missed out a promotion opportunity";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health += 1;
        player.money--;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        if (GameManager.percentChance(15))
        {
            return new Card[] { new AdultObesity() };
        }
        return null;
    }
}

public class AdultObesity : Card
{
    public AdultObesity() : base(1, false, "Obesity", "Happiness -1, Love -5", "Adult", "His lover wanted him to keep in shape.")
    {
        textTable["discardText"] = "Love -10\n10% chance shuffle 'Argument' into the deck";
        textTable["discard"] = "His partner is very unhappy about him not dealing with the obesity problem";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love-=5;
        player.happiness--;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.love -= 10;
        if ((player.married || player.inRelationship) && !player.partnerDied && GameManager.percentChance(10))
        {
            return new Card[] { new RelationshipArgument() };
        }
        return null;
    }
}

public class Diabetes : Card
{
    public Diabetes() : base(1, false, "Diabetes", "Health -2", "Late Adulthood", "His doctor told him that he has diabetes.")
    {
        textTable["discardText"] = "30% chance shuffle 'Diabetes' into the deck";
        textTable["discard"] = "He kept ignoring his diabetes and it is getting worse.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health -= 2;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        if (GameManager.percentChance(30))
        {
            return new Card[] { new Diabetes() };
        }
        return null;
    }
}

public class Marriage : Card
{
    public Marriage() : base(0, true, "Marriage", "Money -20, Love +10, Happiness +10. Shuffle 'Housework' cards into the deck", "Adult", "He married his lover.")
    {
        textTable["discardText"] = "Love -10\n";
        textTable["discard"] = "His partner is very unhappy about him avoiding marriage";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love += 10;
        player.happiness += 10;
        player.money -= 20;
        player.married = true;
        return new Card[] { new Chore(), new HavingKid() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.love -= 10;
        if (GameManager.percentChance(20))
        {
            return new Card[] { new RelationshipArgument() };
        }
        return null;
    }
}

public class HavingKid : Card
{
    public HavingKid() : base(0, true, "Having A Kid", "Love +5, Happiness +10. Shuffle 3x'Chore' into the deck", "Adult", "They were having a kid!")
    {
        textTable["discardText"] = "Love -10\n30% chance shuffle 'Argument' into the deck";
        textTable["discard"] = "His partner wanted a baby but {1} didn't want one.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love += 5;
        player.happiness += 10;
        player.haveKid = true;
        return new Card[] { new Chore(), new Chore(), new Chore() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.love -= 10;
        if ((player.married || player.inRelationship) && !player.partnerDied && GameManager.percentChance(20))
        {
            return new Card[] { new RelationshipArgument() };
        }
        return null;
    }
}

public class KidVisiting : Card
{
    public KidVisiting() : base(0, false, "Kid Visiting", "Happiness +2.", "Adult", "They were having a kid!")
    {}
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness += 2;
        return null;
    }
}

public class Chore : Card
{
    public Chore() : base(0, false, "Chore", "Love +1", "Adult", "He did housework.")
    {
        textTable["discardText"] = "Love -10\n5% chance shuffle 'Argument' into the deck";
        textTable["discard"] = "His partner was angry about him not doing the chores";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.love += 1;
        return new Card[] { };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.love -= 10;
        if ((player.married || player.inRelationship) && !player.partnerDied && GameManager.percentChance(5))
        {
            return new Card[] { new RelationshipArgument() };
        }
        return null;
    }
}

public class MidlifeCrisis : Card
{
    public MidlifeCrisis() : base(0, true, "Midlife Crisis", "Happiness -3, Love -10\n5% chance shuffle 'Divorce' to deck", "Adult", "He felt a bit lost in life.")
    {
        textTable["discardText"] = "20% chance shuffle 'Burn Out' and 10% chance shuffle 'Divorce' into the deck";
        textTable["discard"] = "He felt that life is very draining";
    }

    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness -= 5;
        player.love -= 10;
        if (GameManager.percentChance(5))
        {
            return new Card[] { new Divorce() };
        }
        return null;
    }

    public override Card[] Discard(Player player, GameManager gm)
    {
        List<Card> cards = new List<Card>();
        if (GameManager.percentChance(20))
        {
            cards.Add(new BurnOut());
        }

        if (GameManager.percentChance(10))
        {
            cards.Add(new Divorce());
        }
        return null;
    }
}

public class Drinking : Card
{
    public Drinking() : base(0, false, "Drinking", "Happiness +5\n5% chance shuffle 'Alcohol Addiction' to deck", "Adult", "He drown his sorrows")
    {}

    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness += 5;
        if (GameManager.percentChance(5))
        {
            return new Card[] { new Divorce() };
        }
        return null;
    }
}

public class AlcoholAddiction : Card
{
    public AlcoholAddiction() : base(0, false, "Alcohol Addiction", "Shuffle 'Drinking', 30% chance shuffle 'ChronicDisease' into deck", "Adult", "He was addicted to alchohol.")
    {
        textTable["discardText"] = "Shuffle 'Argument' into the deck";
        textTable["discard"] = "His partner had an argument with him about his drinking problem";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        List<Card> cards = new List<Card>();
        cards.Add(new Drinking());
        if (GameManager.percentChance(30))
        {
            cards.Add(new ChronicDisease());
        }
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.love -= 10;
        if ((player.married || player.inRelationship) && !player.partnerDied && GameManager.percentChance(20))
        {
            return new Card[] { new RelationshipArgument() };
        }
        return null;
    }
}

public class ChronicDisease : Card
{
    public ChronicDisease() : base(1, true, "Chronic Disease", "Heath -2", "Late Adulthood", "He divorced with his partner. It broke his heart.")
    {
        textTable["death"] = "He died fighting the disease that has been long with him";
        textTable["discardText"] = "Health -3";
        textTable["discard"] = "His has gotten weaker from the illness";
    }

    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health -= 2;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.health -= 3;
        return null;
    }
}

public class Divorce : Card
{
    public Divorce() : base(1, true, "Divorce", "Happiness -5, Love = 0", "Late Adulthood", "He divorced with his partner. It broke his heart.")
    {
        textTable["discardText"] = "Happiness -5";
        textTable["discard"] = "He felt like he was stuck in this marriage.";
    }

    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness -= 5;
        player.love = 0;
        player.inRelationship = false;
        player.married = false;
        gm.RemoveRelationshipCards();
        return new Card[] { new FallInLove() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness -= 5;
        return null;
    }
}

public class LateEat : Card
{
    public LateEat() : base(0, false, "Eat", "Health +1", "Late Adult", "He ate like usual") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health++;
        return null;
    }
}

public class LateDrinking : Card
{
    public LateDrinking() : base(0, false, "Drinking", "Health -1", "Late Adult", "He still drinks")
    {
        textTable["death"] = "He died from over-drinking.";
        textTable["discardText"] = "Happiness -1\n";
        textTable["discard"] = "He would like to drink so that he don't have to think about the pass too much.";
    }

    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health--;
        return null;
    }

    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness--;
        return null;
    }
}

public class PlayWithGrandson : Card
{
    public PlayWithGrandson() : base(0, false, "Grandson Visit", "Happiness +1", "Late Adult", "He played with his grandson, that made him very happy.") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness++;
        return null;
    }
}

public class LateCold : Card
{
    public LateCold() : base(1, false, "Cold", "Health -2", "Late Adult", "He caught a cold.")
    {
        textTable["death"] = "He became so weak that a cold actually killed him.";
        textTable["discardText"] = "Health -1\n30% chance shuffle 'Fever' into the deck";
        textTable["discard"] = "He ignored his cold and it got worse.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health -=2;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.health --;
        if (GameManager.percentChance(30))
        {
            return new Card[] { new LateFever() };
        }
        return null;
    }
}

public class LateFever : Card
{
    public LateFever() : base(1, false, "Fever", "Health -4", "Late Adult", "His cold turned into a fever.")
    {
        textTable["death"] = "His fever took his life.";
        textTable["discardText"] = "Health -2";
        textTable["discard"] = "He felt like his body were burning";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.health -= 4;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.health -=2;
        return null;
    }
}

public class FeelingLonely : Card
{
    public FeelingLonely() : base(1, false, "Feeling Lonely", "Happiness -2", "Late Adult", "He felt lonely") { }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness++;
        return null;
    }
}

public class ParentPassAway : Card
{
    public ParentPassAway() : base(1, true, "Parent Pass Away", "Happiness -4", "Adult", "He was in deep sorrow when his parent passed away")
    {
        textTable["discardText"] = "Happiness -2\nShuffle 'Drinking' into the deck";
        textTable["discard"] = "He was very sad even though he pretended not to be. He tried to use alcohol to solve the problem.";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness -= 4;
        player.parentDied = true;
        return null;
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness -= 2;
        player.parentDied = true;
        return new Card[] { new Drinking() };
    }
} 

public class PartnerPassAway : Card
{
    public PartnerPassAway() : base(1, true, "Partner Pass Away", "Happiness -5\nShuffle 'Feeling Lonely into the deck'", "Late Adult", "His partner passed away and left him alone in the world.")
    {
        textTable["discardText"] = "Happiness -2\nShuffle 'Drinking' into the deck";
        textTable["discard"] = "He couldn't avoid the sorrow";
    }
    public override Card[] Activate(Player player, GameManager gm)
    {
        player.happiness -= 4;
        player.partnerDied = true;
        gm.RemoveRelationshipCards();
        return new Card[] { new FeelingLonely() };
    }
    public override Card[] Discard(Player player, GameManager gm)
    {
        player.happiness -= 2;
        player.parentDied = true;
        return new Card[] { new LateDrinking() };
    }
}