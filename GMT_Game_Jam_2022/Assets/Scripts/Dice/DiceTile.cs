using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Dice
{
    public class DiceTile : MonoBehaviour
    {
        public string DiceType { get; private set; }

        public int DiceValue { get;private set; }
        public Vector3Int Position { get; private set; }
        public int CurrentLife { get; private set; } = 0;

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        public void SetTileDetails(Vector3Int position, string diceType, int val)
        {
            this.Position = position;
            this.DiceType = diceType;
            this.DiceValue = val;
        }
    }

}
