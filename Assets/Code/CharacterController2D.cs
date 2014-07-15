using UnityEngine;
using System.Collections;

public class CharacterControer2D : MonoBehaviour 
{
    private const float SkinWidth = 0.2f;
    private const int TotalHorizontalRays = 8;
    private const int TotalVerticalRays = 4;

    private static readonly float SloopeLimitTangant = Mathf.Tan(75f * Mathf.Deg2Rad);

    public LayerMask PlatformMask;
    public ControllerParameters2D DefaultParamaters;

    public ControllerState2D State { get; set; }

    public void Awake()
    {

    }

    public void AdForce(Vector2 force)
    {

    }

    public void SetForce(Vector2 force)
    {

    }

    public void SetHorizontalForce(float x)
    {

    }

    public void SetVerticalForce(float y)
    {

    }

    public void Jump()
    {

    }

    private void Move(Vector2 deltaMovement)
    {

    }

    private void HandlePlatforms()
    {

    }

    private void CalculateOrigins()
    {

    }

    private void MoveHorizontally(ref Vector2 deltaMovement)
    {

    }

    private void MoveVertically(ref Vector2 deltaMovement)
    {

    }

    private void HandleVerticalSlope(ref Vector2 deltaMovement)
    {

    }

    private void HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void OnTriggerExit2D(Collider2D other)
    {

    }
}
