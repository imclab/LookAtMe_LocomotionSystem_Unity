using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SmoothingMethod : MonoBehaviour
{
	public string _SmoothingMethod;
}

public interface ComputeMethod
{
	void Smooth(ref List<Vector4> input, ref List<Vector4> output);
}

public class RegressionMethod  : SmoothingMethod, ComputeMethod
{
	public bool _UseOnline;
	public float _TreatmentRangeInMilliseconds = 120;

	public RegressionMethod()
	{
		_SmoothingMethod = "linear regression";
	}

	public void Smooth(ref List<Vector4> input, ref List<Vector4> output)
	{		
		/*double indexAvg = 0;
		double xAvg = 0;
		double yAvg = 0;
		double zAvg = 0;
		
		for (int i = 0; i< input.Count; ++i)
		{
			indexAvg += i;
			xAvg += input[i][1];
			yAvg += input[i][2];
			zAvg += input[i][3];
		}
		
		indexAvg = indexAvg / input.Length;
		xAvg = xAvg / input.Length;
		yAvg = yAvg / input.Length;
		zAvg = zAvg / input.Length;
		
		double v1 = 0;
		double v2 = 0;

		for (int i = 0; x < input.Count; ++i)
		{
			v1x += (i - indexAvg) * (input[i][1] - xAvg);
			v1y += (i - indexAvg) * (input[i][2] - yAvg);
			v1z += (i - indexAvg) * (input[i][3] - zAvg);
			v2 += Math.Pow(i - indexAvg, 2);
		}
		
		double ax = v1x / v2;
		double ay = v1y / v2;
		double az = v1z / v2;
		double bx = xAvg - ax * indexAvg;
		double by = yAvg - ay * indexAvg;
		double bz = zAvg - az * indexAvg;*/
		
		//Console.WriteLine("y = ax + b");
		//Console.WriteLine("a = {0}, the slope of the trend line.", Math.Round(a, 2));
		//Console.WriteLine("b = {0}, the intercept of the trend line.", Math.Round(b, 2));
		
		//Console.ReadLine();
	}
}
