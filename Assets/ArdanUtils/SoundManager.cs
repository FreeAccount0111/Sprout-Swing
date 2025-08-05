//using System.Collections.Generic;
//using UnityEngine;
////using Lofelt.NiceVibrations;

//public class SoundManager : MonoBehaviour
//{
//    public static SoundManager Instance;
    
//    [Header("Music")]
//    public AudioSource musicSource;
//    public List<AudioClip> clipBgs;
//    [Header("Sound")]
//    public AudioSource[] soundSources;
//    public AudioSource[] loopSources;
    
//    private Queue<AudioSource> _queueSources;
//    private Queue<AudioSource> _queueLoops;
//    private Dictionary<AudioClip, AudioSource> _dicLoop = new();
    
//    [Header("UI Sound")]
//    public AudioClip click;
//    public AudioClip pickUp, throwDown, openBox, moveShelf, placeShelf, placeProduct, order, paidMoney, paidCoin, tit, //footStep,
//        closeDoor, laptop, keyBoard, complete, carGoto, openMarket, yawn, sleep, mopping, glassSqueak, thunderSound;
//    public List<AudioClip> carPipPip;
    
//    protected virtual void Awake()
//    {
//        Instance = this;
//        _queueSources = new Queue<AudioSource>(soundSources);
//        _queueLoops = new Queue<AudioSource>(loopSources);
//        UpdateMute();
//        PlayBg();
//    }

//    public AudioClip RandomPipPip()
//    {
//        return carPipPip[Random.Range(0, carPipPip.Count)];
//    }

//    public void PlayBg()
//    {
//        musicSource.Play();
//    }
    
    
//    public void UpdateMute()
//    {
//        ChangeMusic();
//        ChangeSound();
//    }
    
//    void ChangeMusic()
//    {
//        if (musicSource != null)
//        {
//            musicSource.mute = !DataController.Music;
//        }
//    }

//    void ChangeSound()
//    {
//        foreach (var sound in soundSources)
//        {
//            sound.mute = !DataController.Sfx;
//        }
//        foreach (var sound in loopSources)
//        {
//            sound.mute = !DataController.Sfx;
//        }
//    }
    
    
//    public virtual void PlayShot(AudioClip clip, float volume = 1f)
//    {
//        // if (!UIController.Instance.uiGamePlay.showing)
//        // {
//        //     return;
//        // }
//        if (clip == null)
//        {
//            return;
//        }
//        var source = _queueSources.Dequeue();
//        source.volume = volume;
//        source.PlayOneShot(clip);
//        _queueSources.Enqueue(source);
//    }
    
//    public void PlayEmphasis(long milliseconds = 50)
//    {
//        if (!PrefData.Vibrate)
//        {
//            return;
//        }
//        Vibration.Vibrate(milliseconds);
//    }
    
//    public void PlayEmphasis(float amplitude, float frequency)
//    {
//        if (!PrefData.Vibrate)
//        {
//            return;
//        }
//        //HapticPatterns.PlayEmphasis(amplitude, frequency);
//    }

//    public void PlayConstant(float amplitude, float frequency, float duration)
//    {
        
//        if (!PrefData.Vibrate)
//        {
//            return;
//        }
//        //HapticPatterns.PlayConstant(amplitude, frequency, duration);
//    }
    
//    public void PlayLoop(AudioClip clip, float volume = 1f)
//    {
//        if (clip == null || _dicLoop.ContainsKey(clip))
//        {
//            return;
//        }
//        var loopSource = _queueLoops.Dequeue();
//        loopSource.volume = volume;
//        loopSource.clip = clip;
//        loopSource.Play();
//        _dicLoop.Add(clip, loopSource);
//    }

//    public void StopLoop(AudioClip clip)
//    {
//        if (!clip || !_dicLoop.ContainsKey(clip))
//        {
//            return;
//        }
//        var loopSource = _dicLoop[clip];
//        if (loopSource)
//        {
//            loopSource.Stop();
//            _queueLoops.Enqueue(loopSource);
//            _dicLoop.Remove(clip);
//        }
//    }
//}
