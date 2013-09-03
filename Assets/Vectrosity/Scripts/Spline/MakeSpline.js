var numberOfSpheres = 9;
var segments = 250;
var loop = true;
var usePoints = false;

function Start () {
	var splinePoints = new Vector3[numberOfSpheres];
	for (var i = 0; i < numberOfSpheres; i++) {
		splinePoints[i] = GameObject.Find("Sphere"+(i+1)).transform.position;
	}

	if (usePoints) {
		var dotLine = new VectorPoints("Spline", new Vector3[segments+1], null, 2.0);
		Vector.MakeSplineInLine (dotLine, splinePoints, segments, loop);
		Vector.DrawPoints (dotLine);
	}
	else {
		var spline = new VectorLine("Spline", new Vector3[segments+1], null, 2.0, LineType.Continuous);
		Vector.MakeSplineInLine (spline, splinePoints, segments, loop);
		Vector.DrawLine (spline);
	}
}