using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour 
{
	public Transform pointPrefab;
	[Range(10,100)]public int resolution = 10;
	Transform[] points;
	[Range(0,1)]public int function;

	static float SineFunction(float x,float t)
	{
		return Mathf.Sin(Mathf.PI * (x + t));
	}

	static float MultiSineFunction(float x,float t)
	{
		float y = Mathf.Sin(Mathf.PI * (x + t));
		y += Mathf.Sin(2f * Mathf.PI * (x + 2f * t)) / 2f;
		y *= 2f/3f;
		return y;
	}

	// Use this for initialization
	void Awake () 
	{
		float step = 2f / resolution;
		Vector3 scale = Vector3.one * step;
		Vector3 position;
		position.y = 0f;
		position.z = 0f;
		points = new Transform[resolution];
		for (int i = 0 ; i < resolution ; i++){
			Transform point = Instantiate(pointPrefab);
			position.x = (i+0.5f) * step - 1f;
			//position.y = position.x * position.x * position.x;
			point.localPosition = position;
			point.localScale = scale;
			point.SetParent(transform,false);
			points[i] = point;
		}
	}

	void Update()
	{
		for (int i = 0; i < points.Length; i++)
		{
			float t = Time.time;
			Transform point = points[i];
			Vector3 position = point.localPosition;
			//position.y = position.x * position.x * position.x;
			//position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
			//position.y = SineFunction(position.x, t);
			if (function == 0){
				position.y = SineFunction(position.x,t);
			}
			else {
				position.y = MultiSineFunction(position.x,t);
			}
			point.localPosition = position;
		}
	}
	
}
