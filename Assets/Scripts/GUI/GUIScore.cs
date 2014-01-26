using UnityEngine;
using System.Collections;

public class GUIScore : MonoBehaviour
{
	public TextMesh scoreText;
	public TextMesh scoreTextTransduckified;
	public Transform scoreObj;
	private int _displayedScore;

	public TextMesh comboText;
	public TextMesh comboTextTransduckified;
	public Transform comboObj;
	private int _displayedCombo;

	void Start ()
	{
		UpdateScoreText(0);
		UpdateComboText(0);
	}
	
	void Update ()
	{
		int score = Game.Inst.m_scoring._current_score;
		if(score != _displayedScore)
		{
			UpdateScoreText(score);
			scoreObj.SendMessage("Bump", SendMessageOptions.DontRequireReceiver);
			_displayedScore = score;
		}

		int combo = Game.Inst.m_scoring._current_combo;
		if(_displayedCombo != combo)
		{
			UpdateComboText(combo);
			comboObj.SendMessage("Bump", SendMessageOptions.DontRequireReceiver);
			_displayedCombo = combo;
		}
	}

	private void UpdateScoreText(int number)
	{
		string numString = ""+number;
		scoreText.text = numString;
		scoreTextTransduckified.text = numString;
	}

	private void UpdateComboText(int number)
	{
		string numString = "x"+number;
		comboText.text = numString;
		comboTextTransduckified.text = numString;
	}

}

