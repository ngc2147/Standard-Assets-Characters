﻿using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI for selecting a character via toggle buttons.
/// </summary>
public class SimpleMovementCharacterSelect : MonoBehaviour
{
	[Header("Game Objects")]
	[SerializeField, Tooltip("The freelook camera.")]
	private CinemachineFreeLook freeLook;

	[SerializeField, Tooltip("Characters to select.")]
	private GameObject[] characters;

	[Header("Elements")]
	[SerializeField, Tooltip("A toggle for each character.")]
	private Toggle[] toggles;
	
	/// <summary>
	/// Called when a toggle's value changed.
	/// </summary>
	public void OnToggleValueChanged(bool isOn)
	{
		for (int i = 0, len = toggles.Length; i < len; i++)
		{
			Toggle toggle = toggles[i];
			if (toggle != null &&
			    toggle.isOn)
			{
				SelectCharacter(i);
				break;
			}
		}
	}
	
	/// <summary>
	/// Select the default character controller.
	/// </summary>
	private void Start()
	{
		if (characters.Length != toggles.Length)
		{
			Debug.LogError("The number of characters and toggles are not the same.");
		}
		SelectCharacter(0);
	}

	/// <summary>
	/// Select the character by index.
	/// </summary>
	private void SelectCharacter(int index)
	{
		int len = characters.Length;
		if (index < 0)
		{
			index = len - 1;
		}
		if (index >= len)
		{
			index = 0;
		}

		GameObject activeCharacter = null;
		for (int i = 0; i < len; i++)
		{
			bool isSelected = (index == i);
			GameObject character = characters[i];
			if (character != null)
			{
				character.SetActive(false);
				if (isSelected &&
				    freeLook != null)
				{
					freeLook.LookAt = character.transform;
					freeLook.Follow = character.transform;
				}
				if (isSelected)
				{
					activeCharacter = character;
				}
			}

			Toggle toggle = toggles[i];
			if (toggle != null)
			{
				toggle.isOn = isSelected;
			}
		}
		
		// Enable the correct character after the others have been disabled, in case one of the others
		// disables the global Controls.
		if (activeCharacter != null)
		{
			activeCharacter.SetActive(true);
		}
	}
}
