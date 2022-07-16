using System;
using System.Collections;
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
        public bool isSelected { get; set; }
        public DiceData DiceData { get;private set; }

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        public void SetTileDetails(DiceData diceData)
        {
           this.DiceData = diceData;
        }

        /// <summary>
        /// Grow dice from 0 - 7
        /// </summary>
        /// <returns></returns>
        public IEnumerator GrowDice()
        {
            while (this.DiceData.Age <= this.DiceData.MaxLifeSpan)
            {
                yield return new WaitForSeconds(1.0f);
                if (!this.isSelected)
                {
                    this.DiceData.Age++;
                }
            }
        }

        public Tile GetTile()
        {
            return this.DiceData.Tile as Tile;
        }
    }

}
