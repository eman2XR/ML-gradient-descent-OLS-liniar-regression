using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GradientDescent_LinearRegression))]
public class Insperctor_GradientDescentLiniarRegression : Editor {

    public override void OnInspectorGUI()
    {
        GUILayout.Label("We want to seek parameters for theta  0 & 1 (or b and m) " +
            "\n that minimize this cost function:");
        GUILayout.Label(Resources.Load<Texture2D>("Cost Function"));

        GUILayout.Label("m corresponds to the number of training samples we \n " +
            "have available and x^{i} corresponds to the \n " +
            "ith training example. y^{i} corresponds to the ground \n " +
            "truth value we  have associated with the ith training \n " +
            "sample.  h is our hypothesis, and it is given as:");
        GUILayout.Label(Resources.Load<Texture2D>("Hypothesis"));

        GUILayout.Label("We minimize the cost function using batch gradient descent:");
        GUILayout.Label(Resources.Load<Texture2D>("Gradient Descent"));

        base.OnInspectorGUI();
    }
}
