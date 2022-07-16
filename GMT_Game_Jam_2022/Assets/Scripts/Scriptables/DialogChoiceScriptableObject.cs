using Assets.Scripts.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "ChoiceScriptable", menuName = "Dialogs/ChoiceScriptable")]
    public class DialogChoiceScriptableObject : ScriptableObject
    {
        public string PositiveChoiceText;
        public string NegativeChoiceText;
        public int PositiveBonus;
        public int NegativeBonus;
    }
}
