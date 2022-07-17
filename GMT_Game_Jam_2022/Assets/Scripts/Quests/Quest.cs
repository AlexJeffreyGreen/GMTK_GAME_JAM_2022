using Assets.Scripts.Sounds;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _defDisplay;
    [SerializeField]
    private TextMeshProUGUI _atkDisplay;
    [SerializeField]
    private Image _monsterImage;
    public int _atkRequirement { get; private set; }
    public int _defRequirement { get; private set; }
    public int _health { get; set; }
    [SerializeField]
    private Slider _attackSlider;
    [SerializeField]
    private Slider _healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        //_attackSlider = GetComponentInChildren<Slider>();
        //_monsterImage = GetComponentInChildren<Image>();
        this.GenerateRandomQuest();
    }

    // Update is called once per frame
    void Update()
    {
        this._healthSlider.value = this._health;
    }

    public void GenerateRandomQuest()
    {
        this.name = $"{this.MonsterAdj[Random.Range(0, this.MonsterAdj.Length - 1)]} {this.MonsterTypes[Random.Range(0, this.MonsterTypes.Length - 1)]}";
        this._health = Random.Range(25, 50);
        this._healthSlider.minValue = 0;
        this._healthSlider.maxValue = this._health;
        this._defRequirement = Random.Range(1, this.RollMax);
        this._atkRequirement = Random.Range(1, this.RollMax);
        this._defDisplay.text = $"{this._defRequirement.ToString()}";
        this._atkDisplay.text = $"{this._atkRequirement.ToString()}";
        this._monsterImage.sprite = GameManager.instance.MainMonsterSprites[Random.Range(1, GameManager.instance.MainMonsterSprites.Length - 1)];
        this._monsterImage.color = Color.white;
        this._attackSlider.value = this._attackSlider.minValue;
        StartCoroutine(this.QuestAutoAttacker());
    }

    public void Reroll()
    {
        this._defRequirement = Random.Range(1, this.RollMax);
        this._atkRequirement = Random.Range(1, this.RollMax);
        this._defDisplay.text = $"{this._defRequirement.ToString()}";
        this._atkDisplay.text = $"{this._atkRequirement.ToString()}";
        this._attackSlider.value = 0;
    }

    public void AttackQuest()
    {
        int attackAgainstQuest = this._defRequirement - GameManager.instance.TotalAttack;
        int attackAgainstPlayer = GameManager.instance.TotalDefense - this._atkRequirement;

        bool canThePlayerHurtTheQuest = (attackAgainstQuest <= 0);
        bool canTheQuestHurtThePlayer = (attackAgainstPlayer <= 0);

        if (canThePlayerHurtTheQuest)
        {
            HurtTheQuest();
        }
        if (canTheQuestHurtThePlayer)
        {
            HurtThePlayer();
        }
        if (this._health <= 0)
        {
            CompleteQuest();
        }
        else
        {
            Reroll();
        }

        this._attackSlider.value = this._attackSlider.minValue;

    }

    public void DefendQuest()
    {
        SoundManager.instance.RandomSoundEffect(SoundManager.instance.DefenseClips);
        HurtThePlayer();
        Reroll();
    }
    private void CompleteQuest()
    {
        GameManager.instance.ClearAllSelectedDice();
        GameManager.instance.CurrentLevel++;
        this.GenerateRandomQuest();
        StopAllCoroutines();
        StartCoroutine(this.QuestAutoAttacker());
    }

    private void HurtTheQuest()
    {
        this._health -= GameManager.instance.TotalAttack;
        this.TriggerHurtQuest();
    }

    private void HurtThePlayer()
    {
        int total = this._atkRequirement - GameManager.instance.TotalDefense;
        if (total > 0)
        {
            SoundManager.instance.RandomSoundEffect(SoundManager.instance.QuestAttackClips);
            GameManager.instance.HeroHP -= total;
            GameManager.instance.TriggerHurtPlayer();
        }
    }

    public IEnumerator QuestAutoAttacker()
    {
        while (true)
        {
            if (GameManager.instance.HeroHP <= 0)
            {
                //StopAllCoroutines();
                yield break;
            }
            else
            {
                while (this._attackSlider.value < this._attackSlider.maxValue)
                {
                    if (GameManager.instance.HeroHP <= 0)
                    {
                        yield break;
                    }
                    else
                    {
                        this._attackSlider.value++;
                        yield return new WaitForSeconds(1.0f);
                    }
                }
                if (this._attackSlider.value >= this._attackSlider.maxValue)
                {
                    this.AttackQuest();
                    this.Reroll(); //reroll stats
                    GameManager.instance.ClearAllSelectedDice();
                }
            }
        }
    }

    public void TriggerHurtQuest()
    {
        //ended up not going that route
        StartCoroutine(DelayRenderer());
        SoundManager.instance.RandomSoundEffect(SoundManager.instance.EffectsClips);
    }

    IEnumerator DelayRenderer()
    {
        //SpriteRenderer _renderer = this.HeroFace.GetComponent<SpriteRenderer>();
        Color originalColor = Color.white;
        for (int i = 0; i < 5; i++)
        {
            this._monsterImage.color = Color.red;
            yield return new WaitForSeconds(.15f);
            this._monsterImage.color = Color.white;
            yield return new WaitForSeconds(.15f);
        }

        this._monsterImage.color = originalColor;

        yield return null;
    }

    public int RollMax
    {
        get
        {
            int tmp = GameManager.instance.CurrentLevel;
            return Mathf.Min((tmp * 6), 54);
        }
    }

    private string[] MonsterTypes =
    {
        "Dog",
        "Beast",
        "Man",
        "Manticore",
        "Vampire",
        "Mutant",
        "Slime",
        "King Slime",
        "Billy",
        "Cobra",
        "Monster"
    };

    private string[] MonsterAdj =
    {
        "Abhorrent",
"Abusive",
"Accomplished",
"Accursed",
"Afraid",
"Anger",
"Annihilate",
"Appear",
"Arch",
"Artful",
"Atrocious",
"Awful",
"Bad",
"Baddest",
"Bawdy",
"Beastly",
"Berserk",
"Biggest",
"Black",
"Bloodcurdling",
"Blooded",
"Bloodthirsty",
"Bloody",
"Bold",
"Brutal",
"Bullies",
"Capture",
"Chauvinistic",
"Cheater",
"Chief",
"Clever",
"Comic",
"Complete",
"Concoct",
"Conspirator",
"Conspire",
"Consummate",
"Control",
"Conventional",
"Cowardly",
"Crafty",
"Creature",
"Creepy",
"Cringe",
"Cruel",
"Cruelty",
"Cunning",
"Cursed",
"Damned",
"Dangerous",
"Dastardly",
"Death",
"Decapitate",
"Deliberate",
"Demon",
"Desperate",
"Despicable",
"Determined",
"Detestable",
"Disappearance",
"Disguise",
"Disgusting",
"Disturbing",
"Dominant",
"Doomed",
"Dragon",
"Dread",
"Dyed",
"Escape",
"Evil",
"Execrable",
"Faced",
"False",
"Favorite",
"Female",
"Fiend",
"Folklore",
"Forceful",
"Foul",
"Freak",
"Frightening",
"Gargantuan",
"Gasp",
"Ghastly",
"Goblin",
"Goose Bumps",
"Gothic",
"Greater",
"Greatest",
"Grisly",
"Grotesque",
"Growling",
"Gruesome",
"Hag",
"Hair Raising",
"Hard",
"Hardened",
"Harm",
"Hate-Monger",
"Haunt",
"Hearted",
"Heartless",
"Heavy",
"Heinous",
"Hellbent",
"Hideous",
"Horrendous",
"Horrible",
"Huge",
"Hypocritical",
"Hysteria",
"Impudent",
"Infamous",
"Infernal",
"Influence",
"Insolent",
"Jealous",
"Jeer",
"Kidnapping",
"Killer",
"Killing",
"Kindless",
"Lecherous",
"Legerdemain",
"Lethal",
"Little",
"Lore",
"Lunatic",
"Lycanthrope",
"Macabre",
"Machiavellian",
"Maim",
"Main",
"Male",
"Malevolence",
"Malicious",
"Maniac",
"Manipulate",
"Maraud",
"Masked",
"Mean",
"Melodramatic",
"Menace",
"Minded",
"Miserable",
"Monster",
"Monstrous",
"Murderous",
"Mutilate",
"Mysterious",
"Nasty",
"Nemesis",
"Nightmare",
"Notorious",
"Obliterate",
"Obscure",
"Obvious",
"Old",
"Outlandish",
"Perfect",
"Perfidious",
"Perjured",
"Persecution",
"Poisonous",
"Possessed",
"Potent",
"Powerful",
"Prey",
"Principal",
"Prowl",
"Quest",
"Quiver",
"Real",
"Reign",
"Repel",
"Repugnant",
"Revolting",
"Ritualistic",
"Rogue",
"Ruthless",
"Scared",
"Scary",
"Scoundrel",
"Scream",
"Screeching",
"Shiver",
"Shrieking",
"Sinister",
"Skull",
"Slay",
"Sleazy",
"Snooping",
"Sociopath",
"Somber",
"Spooked",
"Spooky",
"Spy",
"Stalking",
"Stealth",
"Terror",
"Thief",
"Thorough",
"Thug",
"Torture",
"Traditional",
"Transformation",
"Treacherous",
"Tremors",
"Trickery",
"Tricks",
"True",
"Typical",
"Ugly",
"Ultimate",
"Unbelievable",
"Ungrateful",
"Unleash",
"Unmitigated",
"Unprincipled",
"Unscrupulous",
"Untrustworthy",
"Unusual",
"Utter",
"Vanish",
"Venomous",
"Vicious",
"Wail",
"Weird",
"Wicked",
"Willies",
"Wince",
"Worst",
"Wretch",
"Wrongdoing",
"Young",
    };

    //private void 
}

//public static class SideQuests
//{
//    public static List<string> SideQuestCollection = new List<string>()
//    {
//        "Roll a 6",
//        "Roll a 14"
//    };
//}


