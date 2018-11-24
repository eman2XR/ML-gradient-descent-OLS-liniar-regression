using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Line : MonoBehaviour {

    LineRenderer lineRend;
    public Transform lineEnd;
    public Transform lineStart;

    void Start () {
        lineRend = this.GetComponent<LineRenderer>();	
	}
	
	void Update () {
        lineRend.SetPosition(0, lineStart.position);
        lineRend.SetPosition(1, lineEnd.position);
	}
}
