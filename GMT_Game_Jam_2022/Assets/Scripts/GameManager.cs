using Assets.Scripts.Dialogs;
using Assets.Scripts.Scriptables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //[SerializeField]
    //private RectTransform _mainUIRectTransform;
    //[SerializeField]
    //private DialogManager _dialogManagerPrefab;
    //[HideInInspector]
    public DialogManager DialogManager;
   
    
    [SerializeField]
    private List<DialogQuestScriptableObject> dialogQuestScriptableObjects;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        this.test();

        
        //clear all dialogs from list
        //init dialog manager
        //DialogManager = Instantiate<DialogManager>(_dialogManagerPrefab, this._mainUIRectTransform.transform);

        
    }

    void test()
    {
        
        for (int i = 0; i < 10; i++)
        {
            DialogBase dialogB;
            DialogQuestScriptableObject dialogQuestScriptableObject = this.GetRandomDialog();
            if (i % 2 == 0)
            {
                dialogB = DialogFactory.Create<QuestDialog>(dialogQuestScriptableObject);
            }
            else
            {
                dialogB = DialogFactory.Create<ConversationDialog>(dialogQuestScriptableObject);
            }

            DialogManager.EnqueueDialog(dialogB);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DialogBase b = this.DialogManager.DequeueDialog();
            if (b != null)
            {
                DialogManager.GetComponentInChildren<TextMeshProUGUI>().text = b.Message;
            }
            else
                Debug.Log("No more dialogs.");
        }
    }

    //TODO: Take into consideration levels and such
    /// <summary>
    /// Get Random Dialog from Scriptables
    /// </summary>
    /// <returns></returns>
    public DialogQuestScriptableObject GetRandomDialog()
    {
        return dialogQuestScriptableObjects[Random.Range(0, dialogQuestScriptableObjects.Count - 1)];
    }

}
