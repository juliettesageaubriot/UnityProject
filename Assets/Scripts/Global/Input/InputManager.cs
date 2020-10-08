﻿using UnityEngine;

namespace Global.Input
{
	public class InputManager : MonoBehaviour
	{
		public static PlayerControls ActionMaps { get; private set; }
		public static bool IsReady { get; private set; }

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);

			ActionMaps = new PlayerControls();
			ActionMaps.Enable();

			IsReady = true;
		}

		private void OnDestroy()
		{
			ActionMaps.Enable();
			IsReady = false;
		}
	}

}
