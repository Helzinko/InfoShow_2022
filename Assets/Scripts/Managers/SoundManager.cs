using System;
using UnityEngine;

public enum SoundTypes
{
    music = 0,
    bird_hurt = 1,
    bird_merge = 2,
    box_buy = 3,
    box_open = 4,
    bird_move = 5,
    bird_punch = 6,
    eagle_spawn = 7,
    ui_error = 8,
}

[Serializable]
public class Sound
{
    public SoundTypes type;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSO soundsSo;

    public static SoundManager instance;

    [SerializeField] private GameObject _musicSource, _effectSource;

    private AudioSource music;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(SoundTypes clip)
    {
        var sound = Instantiate(_effectSource, transform);
        var audioClip = FindClip(clip, soundsSo.sounds);
        sound.GetComponent<AudioSource>().PlayOneShot(audioClip);
        Destroy(sound, audioClip.length);
    }

    public void PlayMusic(SoundTypes clip)
    {
        var sound = Instantiate(_musicSource, transform);
        var audioClip = FindClip(clip, soundsSo.sounds);

        if (music) music.Stop();

        music = sound.GetComponent<AudioSource>();
        music.clip = audioClip;
        music.loop = true;
        music.Play();
    }

    private AudioClip FindClip(SoundTypes clip, Sound[] sounds)
    {
        foreach (var sound in sounds)
        {
            if (sound.type == clip)
                return sound.clip;
        }

        return null;
    }
}
