using UnityEngine;
using System.Collections;

public class JumpPlatform : MonoBehaviour 
{
    public float JumpMagnitute = 20;

    public void ControllerEnter2D(CharacterController2D controller)
    {
        controller.SetVerticalForce(JumpMagnitute);
    }
}
