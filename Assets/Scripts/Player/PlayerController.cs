using System;
using System.Collections.Generic;
using DG.Tweening;
using Global.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Utils;

namespace Player
{

	[Serializable]
	public class MoveEvent : UnityEvent<Vector2> {}

	public class PlayerController : MonoBehaviour
	{
		[SerializeField][Range(0f, 1f)]
		private float movementDuration = 0.3f;
		[SerializeField]
		private float movementDistance = 1f;
		[SerializeField]
		private int maxInputRegistering = 3;

		[SerializeField] private MoveEvent onMove;
		[SerializeField] private MoveEvent onCantMove;

		[SerializeField]
		private Vector2 raycastCenter = Vector2.zero;

		[SerializeField] private SingleUnityLayer obstacleLayer;

		private bool _isMoving = false;
		private readonly Queue<Vector2> _inputsToProcess = new Queue<Vector2>();

		public Animator animator;
		private void Start()
		{
			if (onMove == null)
				onMove = new MoveEvent();
			if (onCantMove == null)
				onCantMove = new MoveEvent();
		}

		private void OnEnable()
		{
			if(InputManager.IsReady) InputManager.ActionMaps.Player.Move.performed += RegisterInput;
		}

		private void OnDisable()
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
			animator.SetBool("IsMoving", _isMoving);
			Debug.Log(_isMoving);
			if (_isMoving || _inputsToProcess.Count == 0) return;
			Move(_inputsToProcess.Dequeue());
		}


		private void Move(Vector2 direction)
		{
			if (!CanMove(direction))
			{
				onCantMove.Invoke(direction);
				return;
			}
			onMove.Invoke(direction);

			_isMoving = true;
			var move = direction * movementDistance;
			var lastPosition = transform.position;
			var newPosition = new Vector2(lastPosition.x, lastPosition.y) + move;

			var tween = transform.DOMove(newPosition, movementDuration);
			tween.OnComplete(() => { _isMoving = false; });
			
			animator.SetFloat("Horizontal", direction.x);
			animator.SetFloat("Vertical", direction.y);
		}

		private bool CanMove(Vector2 direction)
		{
			var hit = Physics2D.Raycast(
				transform.position + new Vector3(raycastCenter.x, raycastCenter.y, 0f),
				direction,
				1f,
				obstacleLayer.Mask);
			return hit.collider == null;
		}
	}
}
