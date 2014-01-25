using UnityEngine;
using System.Collections;

public class DuckizationController : MonoBehaviour {

	public float duckizationAmount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Fabric.EventManager.Instance.SetParameter("Duckable", "Duckization", duckizationAmount);
	}
}
