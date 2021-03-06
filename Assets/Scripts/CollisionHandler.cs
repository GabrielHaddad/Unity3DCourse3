using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float waitReloadLevel = 1f;
    [SerializeField] ParticleSystem crashParticles;
    bool isAlive = true;

    PlayerControls player;
    AudioPlayer audioPlayer;

    void Awake() 
    {
        player = GetComponent<PlayerControls>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (isAlive)
        {
            StartCrashSequence();
        }
    }

    void StartCrashSequence()
    {
        isAlive = false;
        GetComponent<PlayerControls>().enabled = false;
        crashParticles.Play();
        audioPlayer.PlayExplosionSound();
        DisableShipRender();
        player.ModifyLasers(false);
        StartCoroutine(ReloadLevel());

    }

    void DisableShipRender()
    {
        MeshRenderer[] childrenRender = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer child in childrenRender)
        {
            child.enabled = false;
        }
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(waitReloadLevel);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
