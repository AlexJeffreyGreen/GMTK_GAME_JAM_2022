using Assets.Scripts.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dialogs
{
    public abstract class DialogBase
    {
        public DialogBase() { }

        public DialogBase(DialogQuestScriptableObject scriptable)
        {
            this.Title = scriptable.Title;
            this.Message = scriptable.Message;
            this.Level = scriptable.LevelInc;
        }
        virtual public string Title { get; set; } = String.Empty;
        virtual public string Message { get; set; } = String.Empty;
        protected virtual int Level { get; set; } = 0;
    }
}
