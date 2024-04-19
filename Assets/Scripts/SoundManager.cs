using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static ObjectPool;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;
    private ObjectPool objectPool;

    private AudioSource musicAudioSource;             
    public AudioClip musicClip;                       
    public List<AudioSource> EffectAudioSource;

    private void Awake()
    {
        instance = this;
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;

        objectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);

        //test
        //for(int i = 0; i< objectPool.poolDictionary.Values.Count; i++)
        //{
        //    GameObject obj = instance.objectPool.SpawnFromPool("SoundSource");
        //    obj.SetActive(false);
        //    AudioSource audioSource = obj.GetComponent<AudioSource>();
        //    EffectAudioSource.Add(audioSource);
        //}

        //foreach (var pool in objectPool.poolDictionary)
        //{
        //    string tag = pool.Key; // Pool�� �±׸� �����ɴϴ�.
        //    Queue<GameObject> queue = pool.Value; // �� Pool�� Queue�� �����ɴϴ�.
        //    int queueCount = queue.Count; // �� Pool�� Queue�� ������ �����ɴϴ�.

        //    for (int i = 0; i < queueCount; i++)
        //    {
        //        GameObject obj = instance.objectPool.SpawnFromPool(tag); // �ش� Pool�� �±׷κ��� ������Ʈ�� �����ɴϴ�.
        //        obj.SetActive(false); // ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        //        AudioSource audioSource = obj.GetComponent<AudioSource>(); // AudioSource ������Ʈ�� �����ɴϴ�.
        //        EffectAudioSource.Add(audioSource); // EffectAudioSource ����Ʈ�� �߰��մϴ�.
        //    }
        //}
    }

    public static void ChangeBackGroundMusic(AudioClip music)
    {
        instance.musicAudioSource.Stop();
        instance.musicAudioSource.clip = music;
        instance.musicAudioSource.Play();
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
        }
    }
    public static void PlayClip(AudioClip clip)
    {
        GameObject obj = instance.objectPool.SpawnFromPool("SoundSource");
        obj.SetActive(true);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }
}