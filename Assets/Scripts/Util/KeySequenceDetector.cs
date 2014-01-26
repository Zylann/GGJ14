using UnityEngine;
using System.Collections;

public class KeySequenceDetector : MonoBehaviour
{
	public KeyCode[] sequence;
	public KeyCode[] _typed;
	private int _index;

	void Start ()
	{
		sequence = new KeyCode[] {
			KeyCode.UpArrow,
			KeyCode.UpArrow,
			KeyCode.DownArrow,
			KeyCode.DownArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.B,
			KeyCode.A
		};

		_typed = new KeyCode[sequence.Length];
	}

	private bool Check()
	{
		for(int i = 0; i < _index; ++i)
		{
			if(_typed[i] != sequence[i])
				return false;
		}
		return true;
	}

	private void Reset()
	{
		_index = 0;
		for(int i = 0; i < _typed.Length; ++i)
		{
			_typed[i] = KeyCode.None;
		}
	}
	
	void OnGUI()
	{
		if(!_bazinga && Input.anyKeyDown && Event.current.isKey)
		{
			//Bazinga();

			KeyCode code = Event.current.keyCode;
			//Debug.Log(code);
			_typed[_index] = code;
			++_index;
			if(Check ())
			{
				if(_index == sequence.Length)
				{
					Bazinga();
					Reset();
				}
			}
			else
			{
				Reset();
			}
		}
	}

	private bool _bazinga;
	void Bazinga()
	{
		_bazinga = true;

		GetComponent<Rain>().enabled = true;

		Invoke("FinBazinga", 3);
	}

	void FinBazinga()
	{
		GetComponent<Rain>().enabled = false;
		_bazinga = false;
	}

}





