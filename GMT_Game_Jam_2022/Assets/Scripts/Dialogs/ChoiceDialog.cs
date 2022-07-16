using Assets.Scripts.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dialogs
{
    public class ChoiceDialog : DialogBase
    {

        private DialogChoiceScriptableObject _choiceScriptableObject;
        public ChoiceDialog() : base()
        {
            this.Title = "Conversation Title";
            this.Message = "Converation Text";
            this.Level = 0;
        }

        public ChoiceDialog(DialogQuestScriptableObject scriptable) : base(scriptable)
        {
            this.Title = scriptable.Title;
            this.Message = scriptable.Message;
            this.Level = 0;
        }

        public ChoiceDialog(DialogQuestScriptableObject scriptableObject, DialogChoiceScriptableObject choiceScriptable) : base(scriptableObject)
        {
            this._choiceScriptableObject = choiceScriptable;
        }
    }
}
