using UnityEngine;
using System.Collections;

public interface IJumpInputListener
{
	void Initialize();
	
	void Update();
	
	/// <summary>
	/// Returns true if a jump impulse has been input this frame
	/// </summary>
	/// <returns>True if jump impulse this frame, false otherwise/returns>
	bool GetJumpImpulse();

	/// <summary>
	/// Returns true if the jump is being held
	/// </summary>
	/// <returns>True if jump was held this frame, false otherwise</returns>
	bool GetJumpHeld();
}
