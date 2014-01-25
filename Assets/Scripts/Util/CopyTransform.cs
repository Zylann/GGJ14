using UnityEngine;
using System.Collections;

public class CopyTransform : MonoBehaviour
{
	public Transform sourceTransform;
	public Transform targetTransform;
	
	public bool positionX = true;
	public bool positionY = false;
	public bool positionZ = true;
	
	public bool applyInitialOffset = true;
	
	public bool rotationX = false;
	public bool rotationY = true;
	public bool rotationZ = false;
	
	private Vector3 _offset;
	
	void Awake ()
	{
		if(sourceTransform == null)
		{
			Debug.LogError("You need to specify a source transform");
		}
		
		if(targetTransform == null)
		{
			targetTransform = this.transform;
		}
		
		if(applyInitialOffset)
		{
			_offset = targetTransform.position - sourceTransform.position;
		}
	}
	
	void LateUpdate()
	{
		Vector3 sourcePosition = sourceTransform.position;
		Vector3 sourceRotation = sourceTransform.rotation.eulerAngles;
		
		Vector3 targetPosition = targetTransform.position;
		Vector3 targetRotation = targetTransform.rotation.eulerAngles;
		
		if(positionX) targetPosition.x = sourcePosition.x + _offset.x;
		if(positionY) targetPosition.y = sourcePosition.y + _offset.y;
		if(positionZ) targetPosition.z = sourcePosition.z + _offset.z;
		
		if(rotationX) targetRotation.x = sourceRotation.x;
		if(rotationY) targetRotation.y = sourceRotation.y;
		if(rotationZ) targetRotation.z = sourceRotation.z;
		
		targetTransform.position = targetPosition;
		targetTransform.rotation = Quaternion.Euler(targetRotation);
	}
	
}



