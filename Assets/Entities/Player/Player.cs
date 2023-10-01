using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

enum PlayerState
{
    Normal,
    Item,
    Talking,
    Lose
}

public class PlayerController : MonoBehaviour
{
    [Header("Input")][SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction useAction;
    [SerializeField] private InputAction pauseAction;

    [FormerlySerializedAs("moveSpeed")][FormerlySerializedAs("walkSpeed")][Header("Movement")][SerializeField] private float maxMoveSpeed;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Collider2D collider;


    // [Header("Animation")] [SerializeField] private Animator animator;
    // private static readonly int XDirection = Animator.StringToHash("XDirection");
    // private static readonly int YDirection = Animator.StringToHash("YDirection");

    [Header("Input")] private Vector2 _movementInput = Vector2.zero;
    private Vector2 _currentVelocity = Vector2.zero;
    private Vector2 _moveDirection = Vector2.zero;

    [Header("Player State")]
    // generally the player starts in the IdleShort state on spawning unless some game state logic says otherwise. An
    // example of this is if the player is spawned during some cutscene, and as such may start in some different state
    private PlayerState _playerState = PlayerState.Normal;

    [FormerlySerializedAs("_playerStats")]
    [Header("Player Stats")]
    [SerializeField]
    private PlayerStats playerPlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        // enable actions
        moveAction.Enable();
        useAction.Enable();
        pauseAction.Enable();

        // load player items and stats from the save file

        // get other scripts

    }

    void Update()
    {
        _movementInput = moveAction.ReadValue<Vector2>();
        // Debug.Log(_movementInput);

        switch (_playerState)
        {
            case PlayerState.Normal:
                UpdateNormal();
                break;
            case PlayerState.Item:
                UpdateItem();
                break;
            case PlayerState.Talking:
                break;
        }

        // if (_movementInput != Vector2.zero)
        // {
        //     animator.SetFloat(XDirection, _movementInput.x);
        //     animator.SetFloat(YDirection, _movementInput.y);
        // }
    }

    public void Annoy(float annoyance)
    {
        // function that is called by enemies when they are in proximity to the player
        // annoyance is a per time value that is deducted from your "personal space" stat

        playerPlayerStats.CurrentPersonalSpace -= annoyance;
        Debug.Log("Being annoyed");
        if (playerPlayerStats.IsAnnoyed())
        {
            // you are annoyed, so you lose the game. This is a game over state
            _playerState = PlayerState.Lose;
        }
    }

    public void Exercise(float fitness)
    {
        // function that is called by objects when they are in proximity to the player
        // fitness is a value that is deducted from your "curentHealth" stat

        playerPlayerStats.CurrentHealth += fitness;
        Debug.Log("Doing exercise");
        if (playerPlayerStats.IsUnfit())
        {
            // you are unfit, so you collapse. This is a game over state
            _playerState = PlayerState.Lose;
        }
    }
    public void Entertain(float entertainment)
    {
        // function that is called by enemies when they are in proximity to the player
        // annoyance is a per time value that is deducted from your "personal space" stat

        playerPlayerStats.CurrentBoredom += entertainment;
        Debug.Log("Being Entertained");
        if (playerPlayerStats.IsBored())
        {
            // you are bored, so you lose the game. This is a game over state
            _playerState = PlayerState.Lose;
        }
    }
    public void Feed(float food)
    {
        // function that is called by enemies when they are in proximity to the player
        // annoyance is a per time value that is deducted from your "personal space" stat

        playerPlayerStats.CurrentHunger += food;
        Debug.Log("Being Fed");
        if (playerPlayerStats.IsStarved())
        {
            // you are bored, so you lose the game. This is a game over state
            _playerState = PlayerState.Lose;
        }
    }


    void UpdateNormal()
    {
        // the player is moving, so we need to update the velocity based on the acceleration
        // if the the move input stops then we need to handle deceleration, if the player velocity reaches 0 then we
        // move back to the idle state

        if (!DoActionInputChecks())
        {
            ApplyMovement();
        }
        else
        {
            // what input action happened
        }
    }

    void UpdateItem()
    {
        // set player velocity to 0
        // play the sword swing animation and spawn hitbox for the sword
        // when the animation is done, transition back to idle unless resulting hit results in knockback

        // don't do anything here, we are waiting for the animation to finish
        // we can't move in this state

        _currentVelocity = Vector2.zero;
        ApplyMovement();
    }

    void UpdateTalking()
    {
        _currentVelocity = Vector2.zero;
        ApplyMovement();
    }

    void ApplyMovement()
    {
        // if (_currentVelocity == Vector2.zero && _movementInput != Vector2.zero)
        // {
        //     // instant set of move direction
        //     _moveDirection = _movementInput;
        //     _currentVelocity += moveAcceleration * _moveDirection * Time.deltaTime;
        // }
        if (_movementInput != Vector2.zero)
        {
            // use slerp to rotate the movement direction towards the input direction
            _moveDirection = _movementInput;//Vector3.Slerp(_moveDirection, _movementInput, turnSpeed * Time.deltaTime);
            _currentVelocity +=
                _moveDirection * maxMoveSpeed * Time.deltaTime; //moveAcceleration * _moveDirection * Time.deltaTime;
        }
        else
        {
            // stop
            _currentVelocity = Vector2.zero;
            _moveDirection = Vector2.zero;
        }

        // limit the velocity to the maximum move speed
        if (_currentVelocity.magnitude > (maxMoveSpeed * Time.deltaTime))
        {
            _currentVelocity = _currentVelocity.normalized * maxMoveSpeed * Time.deltaTime;
        }

        transform.position += new Vector3(_currentVelocity.x, _currentVelocity.y, 0);
    }

    // Check if an action occurred and if so return true.
    bool DoActionInputChecks()
    {
        // function to be called in idle/walking states to decide what to do next

        if (useAction.WasPressedThisFrame())
        {
            // this is the use action -> could be say use the fridge, or talk to wife etc

            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        // draw a sphere at the player's position
        // Gizmos.color = Color.green;
        // Gizmos.DrawSphere(transform.position, 1);
    }
}
