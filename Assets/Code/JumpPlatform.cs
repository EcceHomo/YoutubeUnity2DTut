﻿using UnityEngine;
using System.Collections;

public class JumpPlatform : MonoBehaviour 
{
    public float JumpMagnitute = 20;
    public AudioClip JumpSound;

    public void ControllerEnter2D(CharacterController2D controller)
    {
        if(JumpSound != null)
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
        controller.SetVerticalForce(JumpMagnitute);
    }
}
