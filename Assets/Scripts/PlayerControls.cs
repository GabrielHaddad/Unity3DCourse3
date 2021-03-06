using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement Control")]

    [Tooltip("How fast ship moves")] 
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;

    [Header("Ship Rotation")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float rotationFactor = 1f;

    [Header("Firing")]

    [Tooltip("Lasers particle system game objects")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] float delayLaserSound = 1f;

    float xThrow, yThrow;
    bool canPlayLaser = true;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            ModifyLasers(true);

            if (canPlayLaser)
            {
                StartCoroutine(PlayLaserSFX());
            }
        }
        else
        {
            ModifyLasers(false);
        }
    }

    IEnumerator PlayLaserSFX()
    {
        canPlayLaser = false;
        audioPlayer.PlayLaserSound();

        yield return new WaitForSeconds(delayLaserSound);

        canPlayLaser = true;
    }

    public void ModifyLasers(bool laserState)
    {
        foreach(GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = laserState;
        }
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationFactor);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
