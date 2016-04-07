using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Navigator : MonoBehaviour {

	public static int Abs(int a) {
		return (a < 0? -a : a);
	}

	public static int Sign(int a) {
		return (a < 0 ? -1 : 1);
	}

	public List<Point> ComputePath(Point from, Point to) {
		List<Point> result = new List<Point>();
		// TODO: Basic pathing
		// Go one tile at a time (add the coords to the result list) to get to the destination.
		

		return result;
	}
}
