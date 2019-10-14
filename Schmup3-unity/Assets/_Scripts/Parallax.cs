﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

	[Header("Set in Inspector")]
	public GameObject poi;
    public GameObject[] panels;
	public float scrollspeed = -30f;
	public float motionMult = .25f;
	private float panelHt;
	private float depth; 


	// Start is called before the first frame update
	void Start()
    {
		panelHt = panels[0].transform.localScale.y;
		depth = panels[0].transform.position.z;

		panels[0].transform.position = new Vector3(0, 0, depth);
		panels[1].transform.position = new Vector3(0, panelHt, depth);

	}

    // Update is called once per frame
    void Update()
    {
		float tY, tx = 0;
		tY = Time.time * scrollspeed % panelHt + (panelHt * .5f);

        if(poi!=null)
		{
			tx = -poi.transform.position.x * motionMult;
		}

		panels[0].transform.position = new Vector3(tx, tY, depth);

		if (tY>=0)
		{
			panels[1].transform.position = new Vector3(tx, tY - panelHt, depth);
		}
        else
		{
			panels[1].transform.position = new Vector3(tx, tY + panelHt, depth);
		}
    }
}
