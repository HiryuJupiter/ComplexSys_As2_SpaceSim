using UnityEngine;
using System.Collections;

public class Module_Rotation: ModuleBase
{
    const float RotationSpeed = 2f;

    //Ctor
    public Module_Rotation(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
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

        CharacterRotationUpdate();
        AnimationUpdate();
    }

    public override void TickFixedUpdate() 
    {
    }

    public override void ModuleExit()
    {
        base.ModuleExit();
    }
    #endregion

    private void CharacterRotationUpdate()
    {
        player.RotateCharacter(GameInput.MoveX * RotationSpeed);
    }

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