import UnityEngine.GUILayout;

var lineMaterial : Material;
var rotateSpeed = 90.0;
private var line : VectorLine;
private var index : int;
private var endReached : boolean;
private var currentColor = Color.white;
private var linePoints : Vector2[];
private var lineColors : Color[];
private var continuous = true;
private var fillJoins = false;
private var thickLine = false;
private var canClick = true;
private var spazotron = false;

function Start () {
	SetLine();
}

function SetLine () {
	Vector.DestroyLine(line);

	if (!continuous) {
		fillJoins = false;
	}
	var lineType = (continuous? LineType.Continuous : LineType.Discrete);
	var joins = (fillJoins? Joins.Fill : Joins.None);
	var lineWidth = (thickLine? 24 : 2);

	linePoints = new Vector2[500];
	linePoints[0] = Vector2.one; // Make sure the first point contains a different point from the last, so Joins.Fill won't try to connect them.
	if (lineType == LineType.Continuous) {
		lineColors = new Color[linePoints.Length-1];
	}
	else {
		lineColors = new Color[linePoints.Length/2];
	}
	for (color in lineColors) color = Color.white;

	line = new VectorLine("Line", linePoints, lineColors, lineMaterial, lineWidth, lineType, joins);
	endReached = false;
	index = 0;
}

function Update () {
	// Since we can rotate the transform, get the local space for the current point, so the mouse position won't be rotated with the line
	var mousePos = transform.InverseTransformPoint(Input.mousePosition);
	// Set the current line point and color when the mouse is clicked
	if (Input.GetMouseButtonDown(0) && canClick) {
		if (continuous) {
			lineColors[index] = currentColor;
		}
		else {
			lineColors[index/2] = currentColor;
		}
		linePoints[index++] = mousePos;
		// Don't overflow the points array
		if (index == line.points2.Length-1) {
			index--;
			endReached = true;
		}
		line.maxDrawIndex = index;
	}
	
	if (spazotron) {
		line.lineWidth = Random.Range(0.0, 25.0);
	}
	
	// The current line point should always be where the mouse is
	linePoints[index] = mousePos;
	if (index > 0) {
		Vector.DrawLine(line, transform);
	}
	
	// Rotate around midpoint of screen.  This could also be accomplished by rotating the line.vectorObject.transform instead,
	// in which case we'd just need to use Vector.DrawLine(line) without the transform. However, we can use the transform to rotate about
	// any axis, not just Z, and the line will still be drawn correctly. Try changing "forward" to "right", for example.
	transform.RotateAround(Vector2(Screen.width/2, Screen.height/2), Vector3.forward, Time.deltaTime * rotateSpeed * Input.GetAxis("Horizontal"));
}

function OnGUI () {
	var rect = Rect(20, 20, 260, 280);
	canClick = (rect.Contains(Event.current.mousePosition)? false : true);
	BeginArea(rect);
	GUI.contentColor = Color.black;
	Label("Click to add points to the line\nRotate with the right/left arrow keys");
	Space(10);
	Label("These options take effect when line is reset:");
	continuous = Toggle(continuous, "Continuous line");
	thickLine = Toggle(thickLine, "Thick line");
	fillJoins = Toggle(fillJoins, "Fill joins (only works with continuous line)");
	Space(5);
	GUI.contentColor = Color.white;
	if (Button("Reset line", Width(150))) {
		SetLine();
	}
	Space(30);
	if (Button("Randomize Color", Width(150))) {
		RandomizeColor();
	}
	if (Button("Randomize All Colors", Width(150))) {
		RandomizeAllColors();
	}
	if (Button("Spazotron" + (spazotron? " off" : ""), Width(150))) {
		spazotron = !spazotron;
		if (!spazotron) {
			line.lineWidth = thickLine? 24 : 2;
		}
	}
	if (endReached) {
		GUI.contentColor = Color.black;
		Label("No more points available. You must be really bored!");
	}
	EndArea();
}

function RandomizeColor () {
	currentColor = Color(Random.value, Random.value, Random.value);
	for (var i = 0; i < lineColors.Length; i++) {
		lineColors[i] = currentColor;
	}
	Vector.SetColors(line, lineColors);
}

function RandomizeAllColors () {
	for (var i = 0; i < lineColors.Length; i++) {
		lineColors[i] = Color(Random.value, Random.value, Random.value);
	}
	Vector.SetColors(line, lineColors);
}