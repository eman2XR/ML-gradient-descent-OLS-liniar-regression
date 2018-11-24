//   Linear Regression using Gradient Descent Method
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientDescent_LinearRegression : MonoBehaviour {

    bool calculated;

    public GameObject dataPoint; //gameobject used to instantiate at mouse position
    public Transform lineStart; 
    public Transform lineEnd;

    //formula for a straight line is  [y = mx + b] where 'm' is the slope and 'b' is the y intercept
    //initiaze the values at zero
    float m = 0;
    float b = 0;

    //store a list of the positions of each click (dataPoint)
    public List<Vector2> dataPoints = new List<Vector2>();

    void Update () {

        //draw a line if there's at least 2 data points
        if(dataPoints.Count > 1)
        {
            CalculateGradientDescent();
            drawLine();
        }

        //when we click mouse add a new dataPoint
        if (Input.GetMouseButtonDown(0))
        {
            //intantiate it 100 in the z direction as that's how far the camera is from the graph
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
            Vector3 worldPos;
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject newDataPoint = Instantiate(dataPoint, worldPos, Quaternion.identity);

            //add to data list 
            dataPoints.Add(new Vector2(newDataPoint.transform.position.x, newDataPoint.transform.position.y));
            //print(new Vector2(newDataPoint.transform.position.x, newDataPoint.transform.position.y));
        } 	
	}

    void drawLine()
    {
        //formula for a straight line is  [y = mx + b] where 'm' is the slope and 'b' is the y intercept
        float x1 = 0; //we want our line to start at the left side of the graph
        float y1 = m * x1 + b;
        float x2 = 100; //and end at the right side
        float y2 = m * x2 + b;

        lineStart.position = new Vector3(x1, y1, 0);
        lineEnd.position = new Vector3(x2, y2, 0);
    }

    void CalculateGradientDescent()
    {
            float learningRate = 0.1f;
         
                for (int i = 0; i < dataPoints.Count; i++)
                {
                    float x = dataPoints[i].x;
                    float y = dataPoints[i].y;

                    float guess = (m * x + b);

                    float error = (y - guess);

                    m = m + (error * x) * (learningRate/10000);
                    b = b + (error) * learningRate;

                    print("m is: " + m);
                    print("b is: " + b);
                }
    }

    // y = mx + b
    // m is slope, b is y-intercept
    //https://spin.atomicobject.com/2014/06/24/gradient-descent-linear-regression/
    //void computeErrorForLineGivenPoints(b, m, points) {
    //float totalError = 0
    //for i in range(0, len(points)) :
    //    totalError += (points[i].y - (m * points[i].x + b)) * *2
    //return totalError / float(len(points))
    //}
}
