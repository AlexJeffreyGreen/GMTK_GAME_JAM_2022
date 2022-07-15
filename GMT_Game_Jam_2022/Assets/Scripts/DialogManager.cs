using Assets.Scripts.Dialogs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update

    //public Queue<Dialog> _dialog;
    [SerializeField]
    private Dialog _dialogPrefab;
    private Queue<DialogBase> dialogList;

    void Start()
    {
        dialogList = new Queue<DialogBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnqueueDialog(DialogBase d)
    {
        this.dialogList.Enqueue(d);
    }

    public DialogBase DequeueDialog()
    {
        if (dialogList.Count > 0)
            return this.dialogList.Dequeue();
        else
            return null;
    }

    public void DeleteAllExistingDialogs()
    {
        while(this.dialogList.Count > 0)
            this.dialogList.Dequeue();
    }
}
