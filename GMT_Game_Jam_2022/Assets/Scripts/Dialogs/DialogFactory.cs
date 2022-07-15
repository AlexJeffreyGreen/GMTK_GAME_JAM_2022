using Assets.Scripts.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dialogs
{
    public class DialogFactory
    {
        private static Type _outputType = typeof(DialogBase);

        public static T Create<T>() where T : DialogBase, new()
        {
            return (T)Activator.CreateInstance(_outputType) as T;
        }

        public static T Create<T>(DialogQuestScriptableObject scriptableObject) where T : DialogBase, new()
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { scriptableObject });
        }
    }
}
