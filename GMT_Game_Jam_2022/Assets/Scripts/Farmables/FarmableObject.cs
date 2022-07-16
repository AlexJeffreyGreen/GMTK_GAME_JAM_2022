using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Farmables
{
    public class FarmableObject { 


        public string Name { get; set; }
        public Vector3Int Position { get; set; }
        public int MaxLife { get; set; }
        public int CurrentLife { get; private set; } = 0;

        public FarmableObject(string name, Vector3Int position, int life)
        {
            this.Name = name;
            this.Position = position;
            this.MaxLife = life;
        }

        public IEnumerator Growth()
        {
            while (CurrentLife < MaxLife)
            {
                yield return new WaitForSeconds(5.0f);
                CurrentLife++;
            }
        }

    }
}
