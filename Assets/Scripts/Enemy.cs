using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] Transform parent;
    [SerializeField] int value = 20;
    [SerializeField] int hitPoints = 2;
    bool canCollide = true;

    Score score;

    void Awake() 
    {
        score = FindObjectOfType<Score>();
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

    void CheckEnemyHealth()
    {
        if (hitPoints <= 0)
        {
            score.IncreaseScore(value);
            KillEnemy();
        }

        canCollide = true;
    }

    void ProcessHit()
    {
        hitPoints--;
        InstantiateEffect(hitParticles);
    }

    void KillEnemy()
    {
        InstantiateEffect(explosionParticle);
        Destroy(gameObject);
    }

    void InstantiateEffect(ParticleSystem particleSystem)
    {
        ParticleSystem instance = Instantiate(particleSystem, transform.position, Quaternion.identity, parent);
                        
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
