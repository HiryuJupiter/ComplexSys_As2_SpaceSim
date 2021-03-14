using UnityEngine;
using System.Collections;

public class Module_Movement : ModuleBase
{
    private float moveXSmoothDampVelocity;
    private float moveZSmoothDampVelocity;

    //Ctor
    public Module_Movement(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    #region Public methods
    public override void ModuleEntry()
    {
        base.ModuleEntry();
        AnimationUpdate();
    }

    public override void TickUpdate()
    {
        base.TickUpdate();

        //CharacterRotationUpdate();
        AnimationUpdate();
    }

    public override void TickFixedUpdate()
    {
        //Modify x-velocity
        status.currentVelocity.x = Mathf.SmoothDamp(status.currentVelocity.x, GameInput.MoveX * settings.MoveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeed * Time.deltaTime);
        status.currentVelocity.z = Mathf.SmoothDamp(status.currentVelocity.z, GameInput.MoveZ * settings.MoveSpeed, ref moveZSmoothDampVelocity, settings.SteerSpeed * Time.deltaTime);
    }

    public override void ModuleExit()
    {
        base.ModuleExit();
    }
    #endregion

    //private void CharacterRotationUpdate ()
    //{
    //    float mouseX = GameInput.MoveX;

    //    player.RotateCharacter(GameInput.MoveX * RotationSpeed);
    //    //motor.RotateCharacter1(Input.GetAxis("Mouse X"));
    //}

    private void AnimationUpdate ()
    {
        if (GameInput.IsMoving)
        {
        }
        else
        {
        }
    }
}