using Assets.Scripts.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Quests
{
    public class Quest
    {
        public int LevelRequirement { get; private set; }
        private Queue<DialogQuestScriptableObject> QuestItems = new Queue<DialogQuestScriptableObject>();
        public bool Success = false;

        public Quest()
        {

        }

        public void Execute()
        {

        }

        public void CalculateQuestLevelRequirements()
        {
            List<DialogQuestScriptableObject> items = this.QuestItems.ToList();
            for(int i = 0; i < QuestItems.Count; i++)
            {
                DialogQuestScriptableObject item = items[i];
                this.LevelRequirement += item.LevelInc;

            }
        }

        public void AddQuestItem(DialogQuestScriptableObject item)
        {
            QuestItems.Enqueue(item);
        }

        public DialogQuestScriptableObject RemoveQuestItem()
        {
            return QuestItems.Dequeue();
        }

    }
}
