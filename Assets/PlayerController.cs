﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Global.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField][Range(0f, 1f)]
	private float movementDuration = 0.3f;

	[SerializeField]
	private float movementDistance = 1f;

	[SerializeField]
	private int maxInputRegistering = 3;

	private bool _isMoving = false;
	private readonly Queue<Vector2> _inputsToProcess = new Queue<Vector2>();

	void OnEnable()
	{
		if(InputManager.IsReady) InputManager.ActionMaps.Player.Move.performed += RegisterInput;
	}

	void OnDisable()
	{
		if(InputManager.IsReady) InputManager.ActionMaps.Player.Move.performed -= RegisterInput;
	}

	private void RegisterInput(InputAction.CallbackContext context)
	{
		if (_inputsToProcess.Count >= maxInputRegistering) return;

		var direction = context.ReadValue<Vector2>();
		// Discard diagonals inputs
		if (!(Mathf.Abs(direction.x) > 0.9f || Mathf.Abs(direction.y) > 0.9f)) return;

		_inputsToProcess.Enqueue(direction);
	}

	private void Update()
	{
		if (_isMoving || _inputsToProcess.Count == 0) return;
		Move(_inputsToProcess.Dequeue());
	}


	private void Move(Vector2 direction)
	{
		_isMoving = true;
	    var move = direction * movementDistance;
	    var lastPosition = transform.position;
	    var newPosition = new Vector2(lastPosition.x, lastPosition.y) + move;

	    var tween = transform.DOMove(newPosition, movementDuration);
	    tween.OnComplete(() => { _isMoving = false; });

	}
}
