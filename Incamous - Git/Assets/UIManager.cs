using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public GameObject WASD;
	public GameObject Jump;
	public GameObject Begin;
	public GameObject End;

	bool Begin1=false;
	bool beginfinished=false;
	bool wasd1=false;
	bool wasd2=false;
	bool wasd3=false;
	bool wasd4=false;
	bool wasdfinished=false;
	bool jump1=false;
	bool jumpfinished=false;
	bool end1=false;
	bool endfinished=false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.E))
		Begin1=true;

		if(Begin1==true)
		{
		Begin.SetActive(false);
		beginfinished=true;
		WASD.SetActive(true);
		}

		if(beginfinished==true)
		{
			if(Input.GetKeyDown(KeyCode.W))
			wasd1=true;
			if(Input.GetKeyDown(KeyCode.S))
			wasd2=true;
			if(Input.GetKeyDown(KeyCode.A))
			wasd3=true;
			if(Input.GetKeyDown(KeyCode.D))
			wasd4=true;

			if(wasd1==true && wasd2==true && wasd3==true && wasd4==true)
			{
				WASD.SetActive(false);
				wasdfinished=true;
				Jump.SetActive(true);
			}
		}

		if(wasdfinished==true)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			jump1=true;

			if(jump1==true)
			{
				Jump.SetActive(false);
				jumpfinished=true;
				End.SetActive(true);
			}
		}

		if(jumpfinished==true)
		{
			if(Input.GetKeyDown(KeyCode.E))
			end1=true;

			if(end1==true)
			{
				End.SetActive(false);
				endfinished=true;
			}
		}

	}
}
