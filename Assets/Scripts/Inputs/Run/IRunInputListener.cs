using UnityEngine;
using System.Collections;

public interface IRunInputListener
{
	void Initialize();
	
	void Update();
	
	/// <summary>
	/// Returns true if a run impulse has been input this frame
	/// </summary>
	/// <returns>True if run impulse this frame, false otherwise/returns>
	bool GetRunningImpulse();
	
	/// <summary>
	/// Returns true if the run is being held
	/// </summary>
	/// <returns>True if run was held this frame, false otherwise</returns>
	bool GetRunningHeld();
}
