using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        //private float LowPitchAdjustmentRange = .95f;
        //public float HighPitchAdjustmentRange = 1.05f;
        public float _volumeMasterLevel;
        public bool _musicPaused = false;
        public AudioSource EffectsSource;
        public AudioSource MusicSource;
        public AudioClip[] MusicClips;
        public AudioClip[] EffectsClips;
        public AudioClip[] AttackClips;
        public AudioClip[] QuestAttackClips;
        public AudioClip[] DefenseClips;
        public float LowPitchAdjustmentRange = .95f;
        public float HighPitchAdjustmentRange = 1.05f;
        public Slider VolumeSlider;

        public static SoundManager instance = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            EffectsSource.volume = this._volumeMasterLevel;
            MusicSource.volume = this._volumeMasterLevel;
        }

        public void PlayEffect(AudioClip clip)
        {
            EffectsSource.clip = clip;
            EffectsSource.Play();
        }

        public void PlayMusic(AudioClip clip, bool loop, float delay = 0.0f)
        {
            if (clip == null) { clip = MusicClips[0]; }
            MusicSource.clip = clip;
            MusicSource.loop = loop;
            StartCoroutine(_internal_PlayMusic(delay));
        }

        IEnumerator _internal_PlayMusic(float delay)
        {
            yield return new WaitForSeconds(delay);
            MusicSource.Play();
        }
        public void RandomSoundEffect(AudioClip[] clips)
        {
            int randomIndex = UnityEngine.Random.Range(0, clips.Length);
            float randomPitch = UnityEngine.Random.Range(LowPitchAdjustmentRange, HighPitchAdjustmentRange);

            EffectsSource.pitch = randomPitch;
            EffectsSource.clip = clips[randomIndex];
            EffectsSource.Play();
        }
        public void ToggleMusic()
        {
            this._musicPaused = !this._musicPaused;
            if (this._musicPaused)
                this.MusicSource.Pause();
            else
                this.MusicSource.Play();
        }

        private void Update()
        {
            this.EffectsSource.volume = this._volumeMasterLevel;
            this.MusicSource.volume = this._volumeMasterLevel;

            //Debug.Log(this._volumeMasterLevel);
            
        }

   
        //public static void PlaySound(bool _loop, AudioClip _clip)
        //{
        //    GameObject soundGameObject = new GameObject("Sound");
        //    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        //    audioSource.clip = _clip;
        //    audioSource.loop = _loop;
        //    audioSource.volume = GameManager.instance.MasterVolume;
        //    audioSource.Play();
        //}

        //public static void PlaySound(bool _loop, AudioClip[] _audioClip)
        //{
        //    GameObject soundGameObject = new GameObject("Sound");
        //    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            
        //    audioSource.clip = _audioClip[Random.Range(0,_audioClip.Length -1)];
        //    audioSource.loop = _loop;
        //    audioSource.volume = GameManager.instance.MasterVolume;
        //    audioSource.Play();
        //}
    }
}
