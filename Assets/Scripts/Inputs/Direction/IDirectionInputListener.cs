using UnityEngine;
using System.Collections;

public interface IDirectionInputListener
{
	void Initialize();

	void Update();

	/// <summary>
	/// Gets the input direction
	/// Positive is forward, negative is backwards
	/// </summary>
	/// <returns>The direction, between -1f and +1f</returns>
	float GetDirection();
}
