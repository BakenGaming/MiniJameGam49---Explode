using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class InputController : MonoBehaviour, IInputHandler
{
    private GameControls _controller;
    private InputAction _move, _dashRight, _dashLeft, _special, _pause;
    private Transform homePlanetCenter;
    private Vector2 moveInput;
    private Rigidbody2D shipRB;
    private StatSystem _stats;
    private PlayerModifiers _mod;
    public void Initialize()
    {
        _stats = PlanetHandler.i.GetStatSystem();
        _mod = PlanetHandler.i.GetModifierSystem();
        homePlanetCenter = GameManager.i.GetPlanetCenter();
        shipRB = GetComponent<Rigidbody2D>();

        _controller = new GameControls();
       
        _move = _controller.GameInput.Move;
        _move.Enable();

        _dashRight = _controller.GameInput.DashRight;
        _dashRight.performed += HandleDashRight;
        _dashRight.Enable();

        _dashLeft = _controller.GameInput.DashLeft;
        _dashLeft.performed += HandleDashLeft;
        _dashLeft.Enable();

        _special = _controller.GameInput.Special;
        _special.performed += HandleSpecial;
        _special.Enable();

        _pause = _controller.GameInput.Pause;
        _pause.performed += HandlePause;
        _pause.Enable();

    }

    private void HandlePause(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void HandleSpecial(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void HandleDashRight(InputAction.CallbackContext context)
    {
            transform.RotateAround(homePlanetCenter.position, Vector3.forward, -50);
    }

    private void HandleDashLeft(InputAction.CallbackContext context)
    {
            transform.RotateAround(homePlanetCenter.position, Vector3.forward, 50);
    }

                


    void Update()
    {
        moveInput = _move.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        if(GameManager.i.GetIsPaused() || moveInput.x == 0)
            shipRB.linearVelocity = Vector2.zero;
        else MoveShip();
    }
    private void MoveShip()
    {
        float _currentMoveSpeed = _stats.GetMoveSpeed() + _mod.GetModifierValue(ModifierType.speed);
        
        if(moveInput.x > 0)
            transform.RotateAround(homePlanetCenter.position, Vector3.forward, 
                -_currentMoveSpeed * Time.deltaTime);
        if(moveInput.x < 0)
            transform.RotateAround(homePlanetCenter.position, Vector3.forward, 
                _currentMoveSpeed * Time.deltaTime);
    }
}
