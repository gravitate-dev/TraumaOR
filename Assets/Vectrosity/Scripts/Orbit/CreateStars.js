var numberOfStars = 2000;
var stars : VectorPoints;

function Start () {
	// Make a bunch of points in a spherical distribution
	var starPoints = new Vector3[numberOfStars];
	for (var i = 0; i < numberOfStars; i++) {
		starPoints[i] = Random.onUnitSphere * 100.0;
	}
	// Make each star have a size of 1 or 2
	var starSizes = new float[numberOfStars];
	for (i = 0; i < numberOfStars; i++) {
		starSizes[i] = Random.Range(1, 3);
	}
	// Make each star have a random shade of grey
	var starColors = new Color[numberOfStars];
	for (i = 0; i < numberOfStars; i++) {
		var thisValue = Random.value * .75 + .25;
		starColors[i] = Color(thisValue, thisValue, thisValue);
	}
	
	// We want the stars to be drawn behind everything else, like a skybox. So we set the vector camera
	// to have a lower depth than the main camera, and make it have a solid black background
	var vectorCam = Vector.SetCamera(CameraClearFlags.SolidColor);
	vectorCam.backgroundColor = Color.black;
	vectorCam.depth = Camera.main.depth - 1;
	// Set the main camera's clearFlags to depth only, so the vector cam shows through the background
	Camera.main.clearFlags = CameraClearFlags.Depth;
	
	stars = new VectorPoints("Stars", starPoints, starColors, null, 1.0);
	Vector.SetWidths (stars, starSizes);
}

function LateUpdate () {
	Vector.DrawPoints (stars);
}