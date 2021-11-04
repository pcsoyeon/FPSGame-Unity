using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController character_Controller;

    public float volume_Min, volume_Max;

    private float accumlated_Distance;

    public float step_Distance;

    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();
    }

    
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        // player가 땅에 닿지 않는 다면 
        if (!character_Controller.isGrounded)
            return;

        if (character_Controller.velocity.sqrMagnitude > 0)
        {
            accumlated_Distance += Time.deltaTime;

            // 움직이고 있다면 
            if (accumlated_Distance > step_Distance)
            {
                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumlated_Distance = 0f;
            }
        }
        else
        {
            accumlated_Distance = 0f;
        }
    }
}
