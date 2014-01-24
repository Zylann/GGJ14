using UnityEngine;
using System.Collections;

public interface IDirectionInputListener
{
	void Initialize();

	void Update();

	/// <summary>
	/// Gets the player direction
	/// Positive is forward, negative is backwards
	/// </summary>
	/// <returns>The direction</returns>
	float GetDirection();
}
