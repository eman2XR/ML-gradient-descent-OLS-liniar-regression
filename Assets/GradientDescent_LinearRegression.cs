//   Linear Regression using Gradient Descent Method]
// good explanation here: https://stackoverflow.com/questions/32274474/machine-learning-linear-regression-using-batch-gradient-descent

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientDescent_LinearRegression : MonoBehaviour {

    //store a list of the positions of each click (dataPoint)
    public List<Vector2> data = new List<Vector2>();

    //learning rate 
    float alpha = 0.03f;

    //number of iterations
    int iter = 100;

    bool calculated;

    public GameObject dataPointPrefab; //gameobject used to instantiate at mouse position
    public Transform lineStart; 
    public Transform lineEnd;

    //formula for a straight line is  [y = mx + b] where 'm' is the slope and 'b' is the y intercept
    //initiaze the values at zero
    float m = 0;
    float b = 0;

    void Update () {

        //draw a line if there's at least 2 data points
        if(data.Count > 1)
        {
            CalculateCost();
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
            GameObject newDataPoint = Instantiate(dataPointPrefab, worldPos, Quaternion.identity);

            //add to data list 
            data.Add(new Vector2(newDataPoint.transform.position.x, newDataPoint.transform.position.y));
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

        //clamp it so that it doesnt go further than the graph
        //y1 = Mathf.Clamp(y1, 0, 100);
        //y2 = Mathf.Clamp(y2, 0, 100);

        lineStart.position = new Vector3(x1, y1, 0);
        lineEnd.position = new Vector3(x2, y2, 0);
    }

    void CalculateGradientDescent()
    {
        float b_gradient = 0;
        float m_gradient = 0;
        float n = (float)data.Count;

        for (int i = 0; i < data.Count; i++)
        {
            float x = data[i].x;
            float y = data[i].y;

            b_gradient += (1 / n) * (((m * x) + b) - y);
            m_gradient += (1 / n) * (((m * x) + b) - y) * x;

            print(b_gradient);
            print(m_gradient);

            b -= (alpha * b_gradient);
            m -= (alpha * m_gradient)/1000;

            print(b);
            print(m);
        }
    }

    //this function just monitors that the cost/error is decreasing
    float CalculateCost()
    {
        float totalCost = 0;
        float cost = 0;
        float n = (float)data.Count;

        for (int i = 0; i < data.Count; i++)
        {
            float x = data[i].x;
            float y = data[i].y;

            totalCost =+ (y - (m * x + b) ) * (y -(m * x + b)); //the sum of hypothesis - actual y value squared
            //float error = (y - guess);
        }

        cost = (1 / (2 * n)) * totalCost; // cost = totalCost/n; 
        //print("cost is : " + cost);
        return cost;
    }

    private void Start()
    {
       /// CalculateCost();
        //instantiate any dataPoints already in the list
        foreach(Vector2 dataPoint in data) {
            GameObject newDataPoint = Instantiate(dataPointPrefab, new Vector3(dataPoint.x, dataPoint.y, 0), Quaternion.identity);
        }

      //  InvokeRepeating("CalculateGradientDescent", 0, 0.5f);
    }

}

