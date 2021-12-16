using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footstepSound;

    [SerializeField]
    private AudioClip[] footstepClip;

    private CharacterController characterController;

    public float volumeMin, volumeMax;

    private float accumlatedDistance;
    public float stepDistance;

    void Awake()
    {
        footstepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        // player가 땅에 닿지 않는 다면 
        if (!characterController.isGrounded)
            return;

        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumlatedDistance += Time.deltaTime;

            // 움직이고 있다면 
            if (accumlatedDistance > stepDistance)
            {
                footstepSound.volume = Random.Range(volumeMin, volumeMax);
                footstepSound.clip = footstepClip[Random.Range(0, footstepClip.Length)];
                footstepSound.Play();

                accumlatedDistance = 0f;
            }
        }
        else
        {
            accumlatedDistance = 0f;
        }
    }
}
