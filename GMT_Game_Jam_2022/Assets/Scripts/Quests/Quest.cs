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
    public int _atkRequirement { get; private set; }
    public int _defRequirement { get; private set; }
    private Slider AttackSlider;
    // Start is called before the first frame update
    void Start()
    {
        AttackSlider = GetComponentInChildren<Slider>();
        this.GenerateRandomQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRandomQuest()
    {
        this._defRequirement = Random.Range(1, 18);
        this._atkRequirement = Random.Range(1, 18);
        this._defDisplay.text = $"{this._defRequirement.ToString()}";
        this._atkDisplay.text = $"{this._atkRequirement.ToString()}";
        this.AttackSlider.value = this.AttackSlider.minValue;
        StartCoroutine(this.QuestAutoAttacker());
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
        if (this._defRequirement <= 0)
        {
            CompleteQuest();
        }

        this.AttackSlider.value = this.AttackSlider.minValue;
        
        //else
        //{
        //    HurtTheQuest();
        //    HurtThePlayer();
        //}
        //if (this._defRequirement <= 0)
        //{
        //    this.CompleteQuest();
        //}
    }

    public void DefendQuest()
    {

    }
    private void CompleteQuest()
    {
        GameManager.instance.ClearAllSelectedDice();
        this.GenerateRandomQuest();
        StopAllCoroutines();
        StartCoroutine(this.QuestAutoAttacker());
    }

    private void HurtTheQuest()
    {
        this._defRequirement -= GameManager.instance.TotalAttack;
       // if (this._defRequirement <= 0)
            //CompleteQuest();
    }

    private void HurtThePlayer()
    {
        int total = this._atkRequirement - GameManager.instance.TotalDefense;
        if (total > 0)
        {
            GameManager.instance.HeroHP -= total;
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
                while (this.AttackSlider.value < this.AttackSlider.maxValue)
                {
                    if (GameManager.instance.HeroHP <= 0)
                    {
                        yield break;
                    }
                    else
                    {
                        this.AttackSlider.value++;
                        yield return new WaitForSeconds(1.0f);
                    }
                }
                if (this.AttackSlider.value >= this.AttackSlider.maxValue)
                {
                    this.AttackQuest();
                    GameManager.instance.ClearAllSelectedDice();
                }
            }
        }
    }
}

//public static class SideQuests
//{
//    public static List<string> SideQuestCollection = new List<string>()
//    {
//        "Roll a 6",
//        "Roll a 14"
//    };
//}
