using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "DialogQuestScriptable", menuName = "Dialogs/QuestDialogScriptable")]
    public class DialogQuestScriptableObject : ScriptableObject
    {
        public string Title;
        public string Message;
        public int LevelInc;
    }
}
