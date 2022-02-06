using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] int value = 20;
    [SerializeField] int hitPoints = 2;
    bool canCollide = true;

    Rigidbody rb;
    GameObject parent;
    Score score;
    AudioPlayer audioPlayer;

    void Awake() 
    {
        score = FindObjectOfType<Score>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        parent = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void Start() 
    {
        AddRigidbody();
    }

    void OnParticleCollision(GameObject other) 
    {
        if (canCollide)
        {
            canCollide = false;
            Debug.Log($"{name} -- {other.name}");
            ProcessHit();
            CheckEnemyHealth();            
            
        }
    }

    void AddRigidbody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void CheckEnemyHealth()
    {
        if (hitPoints <= 0)
        {
            score.IncreaseScore(value);
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoints--;
        InstantiateEffect(hitParticles);
        canCollide = true;
    }

    void KillEnemy()
    {
        audioPlayer.PlayExplosionSound();
        InstantiateEffect(explosionParticle);
        Destroy(gameObject);
    }

    void InstantiateEffect(ParticleSystem particleSystem)
    {
        ParticleSystem instance = Instantiate(particleSystem, transform.position, Quaternion.identity, parent.transform);
                        
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
