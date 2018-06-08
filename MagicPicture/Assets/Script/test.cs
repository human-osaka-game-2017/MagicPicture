using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Material material = this.GetComponent<Renderer>().material;
        BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Fade);
        material.SetColor("_Color", new Color(1, 1, 1, 0.2f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
