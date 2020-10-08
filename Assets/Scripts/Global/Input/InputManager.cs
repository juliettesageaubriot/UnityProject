using System;
using UnityEngine;

namespace Global.Input
{
	public class InputManager : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}

}
