using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(Rigidbody))]
public class PlayerTopDown3DController : MonoBehaviour
{
    //States
    [SerializeField] private Transform spriteRenderer;

    private MotorStates currentStateType;
    private MotorStateBase currentStateClass;
    private Dictionary<MotorStates, MotorStateBase> stateClassLookup;

    private PlayerFeedbacks feedback;
    private Rigidbody rb;

    public PlayerStatus Status { get; private set; }

    #region MonoBehiavor
    private void Awake()
    {
        //Reference
        rb = GetComponent<Rigidbody>();
        feedback = GetComponentInChildren<PlayerFeedbacks>();

        //Initialize
        Status = new PlayerStatus();
        stateClassLookup = new Dictionary<MotorStates, MotorStateBase>
        {
            {MotorStates.Normal,  new MotorState_Normal(this, feedback)},
        };

        currentStateType = MotorStates.Normal;
        currentStateClass = stateClassLookup[currentStateType];
    }

    private void Update()
    {
        currentStateClass?.TickUpdate();
    }

    private void FixedUpdate()
    {
        currentStateClass?.TickFixedUpdate();

        ExecuteRigidbodyVelocity();
    }
    #endregion

    #region Public 
    public void DamagePlayer(Vector2 enemyPos, int damage)
    {
        Status.lastEnemyPosition = enemyPos;
        SwitchToNewState(MotorStates.Hurt);
    }

    public void RotateCharacter (float amount)
    {
        //Rotate the character by multiplying rotation amount to current quaternion
        Quaternion q = Quaternion.AngleAxis(amount, Vector3.up);
        var targetRot = rb.rotation * q;
        rb.rotation = targetRot;
    }

    public void SwitchToNewState(MotorStates newStateType)
    {
        if (currentStateType != newStateType)
        {
            currentStateType = newStateType;

            currentStateClass.StateExit();
            currentStateClass = stateClassLookup[newStateType];
            currentStateClass.StateEntry();
        }
    }
    #endregion

    #region Private
    private void ExecuteRigidbodyVelocity ()
    {
        rb.velocity = transform.TransformDirection(Status.currentVelocity);
    }
    #endregion
}

//private void OnGUI()
//{
//    GUI.Label(new Rect(20, 20, 500, 20), "Current State: " + currentStateType); 

//    GUI.Label(new Rect(20, 60, 290, 20), "=== GROUND MOVE === ");
//    GUI.Label(new Rect(20, 80, 290, 20), "OnGround: " + status.isOnGround);
//    GUI.Label(new Rect(20, 100, 290, 20), "onGroundPrevious: " + status.isOnGroundPrevious);
//    GUI.Label(new Rect(20, 120, 290, 20), "GameInput.MoveX: " + GameInput.MoveX);
//    GUI.Label(new Rect(20, 180, 290, 20), "currentVelocity: " + status.currentVelocity);


//    GUI.Label(new Rect(200, 0, 290, 20), "=== JUMPING === ");
//    GUI.Label(new Rect(200, 20, 290, 20), "coyoteTimer: " + status.coyoteTimer);
//    GUI.Label(new Rect(200, 40, 290, 20), "jumpQueueTimer: " + status.jumpQueueTimer);
//    GUI.Label(new Rect(200, 60, 290, 20), "GameInput.JumpBtnDown: " + GameInput.JumpBtnDown);
//    GUI.Label(new Rect(200, 80, 290, 20), "jumping: " + status.isJumping);

//    GUI.Label(new Rect(400, 0, 290, 20), "=== INPUT === ");
//    GUI.Label(new Rect(400, 20, 290, 20), "MoveX: " + GameInput.MoveX);
//    GUI.Label(new Rect(400, 40, 290, 20), "MoveZ: " + GameInput.MoveZ);

//    //GUI.Label(new Rect(300, 120,		290, 20), "testLocation: " + testLocation);
//}

/*
    public void SetFacingBasedOnMovement ()
    {
        Vector3 vel = Status.currentVelocity;
        vel.y = 0f;
        feedback.SetFacing(Quaternion.LookRotation(cameraController.NonTiltedRotationTowardsPlayer * vel, Vector3.up));
    }

    public void SetFacingToFront ()
    {
        feedback.SetFacing(Quaternion.LookRotation(cameraController.NonTiltedDirectionTowardsPlayer, Vector3.up));
    }

    private void ExecuteRigidbodyVelocity ()
    {
        Rb.velocity = cameraController.NonTiltedRotationTowardsPlayer * Status.currentVelocity;
    }
 */