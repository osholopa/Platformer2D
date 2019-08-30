using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallThrough : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ShootableEnemy"), LayerMask.NameToLayer("ShootableEnemy"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
