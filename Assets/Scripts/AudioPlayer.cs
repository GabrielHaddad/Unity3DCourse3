using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip laserSound;
    [SerializeField] float laserVolume;

    [SerializeField] AudioClip explosionSound;
    [SerializeField] float explosionVolume;

    void Awake() 
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        int numAudioPlayers = FindObjectsOfType<AudioPlayer>().Length;

        if (numAudioPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayExplosionSound()
    {
        PlayClip(explosionSound, explosionVolume);
    }

    public void PlayLaserSound()
    {
        PlayClip(laserSound, laserVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, 
                cameraPos, volume);
        }
    }
}
