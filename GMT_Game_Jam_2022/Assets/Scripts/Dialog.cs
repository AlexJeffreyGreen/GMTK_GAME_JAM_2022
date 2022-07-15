using Assets.Scripts.Scriptables;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    //// Start is called before the first frame update
    //[SerializeField]
    //private TextMeshProUGUI _dialogText;
    ////[SerializeField]
    //private DialogQuestScriptableObject _questLine;
    //private int currentPosition = 0;
    //[SerializeField]
    //private float _delay;
    
    void Start()
    {
        //_dialogText = this.GetComponentInChildren<TextMeshProUGUI>();
        //this._questLine = GameManager.instance.GetRandomDialog();
        //this._dialogText.text = String.Empty;
        //this.text = this._questLine.DialogQuestLine;
        //this.StartTyping();
    }

    //public void StartTyping()
    //{
    //   
    //    StartCoroutine(TypeText());
    //}

    //IEnumerator TypeText()
    //{
    //    while (true)
    //    {
    //        if (this.currentPosition < _questLine.DialogQuestLine.Length)
    //        {
    //            this._dialogText.text += this._questLine.DialogQuestLine[this.currentPosition++];
    //            yield return 0;
    //        }
    //        yield return new WaitForSeconds(this._delay);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

}
