using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]/*[Range(0f, 1f)] */private float soundEffectVolume = 0.1f;
    [SerializeField]/*[Range(0f, 1f)] */private float soundEffectPitchVariance = 1f;
    [SerializeField][Range(0f, 1f)] private float musicVolume;
    private ObjectPool objectPool;

    private AudioSource musicAudioSource;
    public AudioClip musicClip;
    public List<AudioSource> EffectAudioSource;

    //test
    public AudioClip attackSound;
    public AudioClip gainGem;
    public AudioClip inventoryOpenSound;
    public AudioClip inventoryCloseSound;
    public AudioClip SkillPageOpenSound;
    public AudioClip SkillPageCloseSound;
    protected override void Awake()
    {
        base.Awake();

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;

        objectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);

        //test
        foreach (var pool in objectPool.poolDictionary)
        {
            string tag = pool.Key;
            Queue<GameObject> queue = pool.Value;
            int queueCount = queue.Count;

            for (int i = 0; i < queueCount; i++)
            {
                GameObject obj = Instance.objectPool.SpawnFromPool(tag);
                obj.SetActive(false);
                AudioSource audioSource = obj.GetComponent<AudioSource>();
                EffectAudioSource.Add(audioSource);
            }
        }
    }

    public void ChangeBackGroundMusic(AudioClip music)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = music;
        musicAudioSource.Play();
    }
    public void SetMusicVolume(Slider _getslider)
    {
        musicAudioSource.volume = _getslider.value;
    }
    public void SetEffectVolume(Slider _getslider)
    {
        for (int i = 0; i < EffectAudioSource.Count; i++)
        {
            EffectAudioSource[i].volume = _getslider.value;
            soundEffectVolume = EffectAudioSource[i].volume;
        }

    }
    public void PlayClip(AudioClip clip)
    {
        GameObject obj = objectPool.SpawnFromPool("SoundSource");
        obj.SetActive(true);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, soundEffectVolume, soundEffectPitchVariance);
    }

    public void PlayAttackClip()
    {
        PlayClip(attackSound);
    }
}