using System;
using System.Collections.Generic;
using DG.Tweening;
using Global;
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
		[SerializeField]
		private InputData moveInput;
		[SerializeField]
		private PlayerPositionData playerPositionData;
		
		[SerializeField][Range(0f, 1f)]
		private float movementDuration = 0.3f;
		[SerializeField]
		private float movementDistance = 1f;
		[SerializeField]
		private int maxInputRegistering = 3;

		[SerializeField] private MoveEvent onMove;
		[SerializeField] private UnityEvent onStopMove;
		[SerializeField] private MoveEvent onCantMove;

		[SerializeField]
		private Vector2 raycastCenter = Vector2.zero;

		[SerializeField] private SingleUnityLayer obstacleLayer;

		private bool _isMoving = false;
		
		private readonly Queue<Vector2> _inputsToProcess = new Queue<Vector2>();

		private void Awake()
		{ playerPositionData.InitTransform(transform); }
		
		private void Start()
		{
			if (onMove == null)
				onMove = new MoveEvent();
			if (onCantMove == null)
				onCantMove = new MoveEvent();
			if (onStopMove == null)
				onStopMove = new UnityEvent();
			
			onCantMove.AddListener(playerPositionData.UpdateDirection);
			onMove.AddListener(playerPositionData.UpdateDirection);
			onStopMove.AddListener(delegate() { playerPositionData.UpdateIsMoving(false); });
			onMove.AddListener(delegate(Vector2 arg0) { playerPositionData.UpdateIsMoving(true); });
		}

		private void OnEnable()
		{
			moveInput.InputEvent += RegisterInput;
		}

		private void OnDisable()
		{
			moveInput.InputEvent -= RegisterInput;
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
			tween.OnComplete(() =>
			{
				onStopMove.Invoke();
				_isMoving = false; 
			});
		}

		private bool CanMove(Vector2 direction)
		{
			var hit = Physics2D.Raycast(
				transform.position + new Vector3(raycastCenter.x, raycastCenter.y, 0f),
				direction,
				movementDistance,
				obstacleLayer.Mask);
			return hit.collider == null;
		}
	}
}
