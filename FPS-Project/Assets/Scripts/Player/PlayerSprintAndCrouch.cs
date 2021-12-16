using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerCtrl3 playerMovement;

    public float sprint_Speed = 10f;
    public float move_Speeed = 5;
    public float crouch_Speed = 2;

    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;

    private bool is_Crouching;

    //private PlayerFootSteps player_Footsteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;


    void Awake()
    {
        playerMovement = GetComponent<PlayerCtrl3>();

        look_Root = transform.GetChild(0);

        //player_Footsteps = GetComponentInChildren<PlayerFootSteps>();
    }

    void Start()
    {
        //player_Footsteps.volumeMin = walk_Volume_Min;
        //player_Footsteps.volumeMax = walk_Volume_Max;
        //player_Footsteps.stepDistance = walk_Step_Distance;
    }

    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
        {
            playerMovement.moveSpeed = sprint_Speed;

            //player_Footsteps.stepDistance = sprint_Step_Distance;
            //player_Footsteps.volumeMin = sprint_Volume;
            //player_Footsteps.volumeMax = sprint_Volume;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
        {
            playerMovement.moveSpeed = move_Speeed;

            //player_Footsteps.volumeMin = walk_Volume_Min;
            //player_Footsteps.volumeMax = walk_Volume_Max;
            //player_Footsteps.stepDistance = walk_Step_Distance;
        }

    } // sprint

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // crouching -> stand up 
            if (is_Crouching)
            {
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.moveSpeed = move_Speeed;

                //player_Footsteps.volumeMin = walk_Volume_Min;
                //player_Footsteps.volumeMax = walk_Volume_Max;
                //player_Footsteps.stepDistance = walk_Step_Distance;

                is_Crouching = false;

            }
            else
            {
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.moveSpeed = crouch_Speed;

                //player_Footsteps.volumeMin = crouch_Volume;
                //player_Footsteps.volumeMax = crouch_Volume;
                //player_Footsteps.stepDistance = crouch_Step_Distance;

                is_Crouching = true;
            }
        }
    } // crouch 
}
