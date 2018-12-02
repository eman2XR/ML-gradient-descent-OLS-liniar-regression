//   Linear Regression using Ordinary Least Squares Method
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLS_LinearRegression : MonoBehaviour {

    public GameObject dataPoint; //gameobject used to instantiate at mouse position
    public Transform lineStart; 
    public Transform lineEnd;

    //formula for a straight line is  [y = mx + b] where 'm' is the slope and 'b' is the y intercept
    //initiaze the values at zero
    float m = 0;
    float b = 0;

    //store a list of the positions of each click (dataPoint)
    public List<Vector2> data = new List<Vector2>();

    void Update () {

        //draw a line if there's at least 2 data points
        if(data.Count > 1)
        {
            CalculateLinearRegression();
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
            data.Add(new Vector2(newDataPoint.transform.position.x, newDataPoint.transform.position.y));
            //print(new Vector2(newDataPoint.transform.position.x, newDataPoint.transform.position.y));
        } 	
	}

    void drawLine()
    {
        //formula for a straight line is  [y = mx + b] where 'm' is the slope and 'b' is the y intercept
        float x1 = 0; //we want our line to start at the left side of the graph
        float y1 = m * x1 + b; // = y1 = b
        float x2 = 100; //and end at the right side
        float y2 = m * x2 + b;

        //clamp it so that it doesnt go further than the graph
        //y1 = Mathf.Clamp(y1, 0, 100);
        //y2 = Mathf.Clamp(y2, 0, 100);

        lineStart.position = new Vector3(x1, y1, 0);
        lineEnd.position = new Vector3(x2, y2, 0);
    }

    void CalculateLinearRegression()
    {
        //formula for m is  m = sumOf(x - xmean) * (y - ymean)/ sumOf(x - xmean) squared 
        //--> where 'xmean' is the average or : the sum of all x's divided by the number of x's 

        //calculate the sum of each
        float xSum = 0;
        float ySum = 0;
        for (int i = 0; i < data.Count; i++)
        {
            xSum += data[i].x;
            ySum += data[i].y;
        }


        //work out the average
        float xMean = xSum / data.Count;
        float yMean = ySum / data.Count;

        //work out the top part of the formula and the bottom part
        float num = 0;
        float den = 0;
        for (int i = 1; i < data.Count; i++)
        {
            float x = data[i].x;
            float y = data[i].y;
            num += (x - xMean) * (y - yMean);
            den += (x - xMean) * (x - xMean);
        }

        //final formula sumOf(x - xmean) * (y - ymean)/ sumOf(x - xmean) squared  
        m = num / den;

        //formula for y intercept 
        b = yMean - m * xMean;

        print(m);
        print(b);
    }

    private void Start()
    {
    }
}
