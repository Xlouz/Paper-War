using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Mode
    {
        pesawat, kapal, tamia
    }

    public Mode modePlayer;
    public CharacterController characterController;
    public GroundCheck groundCheck;
    public GameObject pesawat, kapal, tamia;

    public float speedPesawat, speedKapal, speedTamia;
    public float speedVerticalPlayer, speedHorizontalPlayer;
    public float jumpForce, gravity = -9.81f;


    public Vector3 movement;
    public float horizontalInput, verticalInput, directionY;

    void Update()
    {
        ChangeMode();
    }

    void ChangeMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            modePlayer = Mode.pesawat;
            directionY = jumpForce + 1;
            groundCheck.groundDarat = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) modePlayer = Mode.kapal;
        if (Input.GetKeyDown(KeyCode.Alpha3)) modePlayer = Mode.tamia;

        if (modePlayer == Mode.pesawat)
        {
            pesawat.SetActive(true);
            kapal.SetActive(false);
            tamia.SetActive(false);

            MovePlayer();
            JumpPlayer(false);

            if (!groundCheck.groundDarat && !groundCheck.groundAir)
            {
                speedHorizontalPlayer = speedPesawat;
            }
            else
            {
                speedHorizontalPlayer = 0;
            }
        }
        else if (modePlayer == Mode.kapal)
        {
            pesawat.SetActive(false);
            kapal.SetActive(true);
            tamia.SetActive(false);

            MovePlayer();

            if (!groundCheck.groundDarat)
            {
                speedHorizontalPlayer = speedKapal;
            }
            else
            {
                speedHorizontalPlayer = 0;
            }
        }
        else if (modePlayer == Mode.tamia)
        {
            pesawat.SetActive(false);
            kapal.SetActive(false);
            tamia.SetActive(true);

            MovePlayer();

            if (!groundCheck.groundAir)
            {
                speedHorizontalPlayer = speedTamia;
            }
            else
            {
                speedHorizontalPlayer = 0;
            }
        }
    }


    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        movement = new Vector3(speedHorizontalPlayer, 0, verticalInput);

        directionY += gravity * Time.deltaTime;
        movement.y = directionY;
        if (groundCheck.groundDarat || groundCheck.groundAir) directionY = 0;


        characterController.Move(movement * speedVerticalPlayer * Time.deltaTime);
    }

    void JumpPlayer(bool android)
    {
        if (android)
        {

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                directionY = jumpForce;
                groundCheck.groundDarat = false;
            }
        }
    }
}
