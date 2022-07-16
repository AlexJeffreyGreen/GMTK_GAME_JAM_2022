using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _defDisplay;
    [SerializeField]
    private TextMeshProUGUI _atkDisplay;
    private int _atkRequirement;
    private int _defRequirement;
    // Start is called before the first frame update
    void Start()
    {
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
        this._defDisplay.text = $"DEF: {this._defRequirement.ToString()}";
        this._atkDisplay.text = $"ATK: {this._atkRequirement.ToString()}";
    }

    public void CompleteQuest()
    {
        if (this._defRequirement <= GameManager.instance.TotalDefense
            && this._atkRequirement <= GameManager.instance.TotalAttack)
        {
            //int tmp = _valueRequirement / 2;
            //if (tmp <= 0) tmp = 1;
            //GameManager.instance.TotalDiceLeft += tmp;
            GameManager.instance.ClearAllSelectedDice();
            this.GenerateRandomQuest();
        }
        else { }
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
