using Assets.Scripts.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dialogs
{
    internal class QuestDialog : DialogBase
    {
        public QuestDialog() : base()
        {
            this.Title = "Title";
            this.Message = "Text";
            this.Level = 1;
        }

        public QuestDialog(DialogQuestScriptableObject scriptable) : base(scriptable)
        {
            this.Title = scriptable.Title;
            this.Message = scriptable.Message;
            this.Level = scriptable.LevelInc;
        }
    }
}
