using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Sounds
{
    public static class SoundManager
    {
        public static void PlaySound(bool _loop, AudioClip _clip)
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = _clip;
            audioSource.loop = _loop;
            audioSource.volume = GameManager.instance.MasterVolume;
            audioSource.Play();
        }
    }
}
