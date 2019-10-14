using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void Attack()
    {
        playerController.Attack();
    }

    public void Jump()
    {
        playerController.Jump();
    }

    public void JumpOff()
    {
        playerController.JumpOff();
    }

    public void Slide(bool slide)
    {
        playerController.Slide(slide);
    }
}
