using Assets.Scripts.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dialogs
{
    public class ConversationDialog : DialogBase
    {
        public ConversationDialog() : base ()
        {
            this.Title = "Conversation Title";
            this.Message = "Converation Text";
            this.Level = 0;
        }

        public ConversationDialog(DialogQuestScriptableObject scriptable) : base(scriptable)
        {
            this.Title = scriptable.Title;
            this.Message = scriptable.Message;
            this.Level = 0;// scriptable.LevelInc;
        }
    }
}
