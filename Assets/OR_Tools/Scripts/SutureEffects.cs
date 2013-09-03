using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(LineRenderer))]
public class SutureEffects : MonoBehaviour
{
	public GUIText gtext;
	public GameObject suturePoint;
	private GameObject targetObject; //this thing spawns and kills anything sutureable upon completeting suture
	public Transform ZPositionStopAt;
	public List<Vector3> linePoints = new List<Vector3>();
	LineRenderer lineRenderer;
	private float startWidth = 0.05f;
	private float endWidth = 0.05f;
	public float threshold = 0.001f;
	public AudioSource effectSound;
	private float oldh=0.0f;
	private float oldv=0.0f;
	private float totalY;
	private float totalX;
	private int totalMeasurements;
	private float averageY = 0.0f;
	private float averageX = 0.0f;
	private int suturepointCount = 0;
	public int suturepointHorizontalCount = 0;
	public int suturepointVerticalCount = 0;
	private int totalLinecasts = 0;
	private int successfulLinecasts = 0;
	private Transform suturePtOLD_A; //we use linecasts real time
	private Transform suturePtOLD_B; //we use linecasts real time
	private Transform lastPointMade;
	Camera thisCamera;
	
	int lineCount = 0;
	
	 
	public Vector3 lastPos = Vector3.one;
	 
	 
	void Awake()
	{
	    thisCamera = Camera.main;
	    lineRenderer = GetComponent<LineRenderer>();
	}
	
	public void clearEffects(){
		
		
		/*debug show measurements */
		if (totalY!=0.0f && totalX!= 0.0f){
			checkIfSutureIsBad();
			/* REJECT BAD SUTURES */
			lastPointMade = null;
		}
		/* Clear average measurements */
		totalMeasurements = 0;
		totalY = 0.0f;
		totalX = 0.0f;
		
		if (targetObject!=null){
			// find horziontal or not
		float horizontalRegression = ((float)suturepointHorizontalCount/(float)suturepointCount)*100.0f;
		float verticalRegression = 100.0f - horizontalRegression;
		
		//for this to be horizontal or vertical the absolute difference of these guys must be greater than a certain amount
		//otherwise its a BAD CUT
		
		float zigzagness = Mathf.Abs(horizontalRegression-verticalRegression);
		Debug.Log("Zig zaggyness: "+zigzagness);
		
			checkLineForSutures();
			BaseAttack baseAttack = targetObject.GetComponent<BaseAttack>();
			if (baseAttack==null)Destroy(targetObject);
			else baseAttack.onToolSuccess();
		}
		totalLinecasts = 0;
		successfulLinecasts = 0;
		suturepointCount = 0;
		suturepointHorizontalCount = 0;
		suturepointVerticalCount = 0;
		
		clearLine();
		if (effectSound.isPlaying == true)effectSound.Stop();
		
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		
	}
	public void drawEffects()
	{
		if (Input.GetMouseButton(0)) {
			
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = ZPositionStopAt.position.z;
			Vector3 mouseWorld = thisCamera.ScreenToWorldPoint(mousePos);
			
			float dist = Vector3.Distance(lastPos, mouseWorld);
			if(dist <= threshold)
			return;
			
			//find average movement of the suture
			totalMeasurements++;
			totalY += Mathf.Abs(Input.GetAxis("Mouse Y"));
			totalX += Mathf.Abs(Input.GetAxis("Mouse X"));
			averageY = totalY / (float)totalMeasurements;
			averageX = totalX / (float)totalMeasurements;
			//if (totalY>totalX && totalMeasurements > 5){
			//	//Debug.Log("Horizontal Cut");
			//}
			
			
			//put the point down
			lastPos = mouseWorld;
			if(linePoints == null)
			linePoints = new List<Vector3>();
			if (mouseWorld.z > 0.1)linePoints.Add(mouseWorld); //this is a fix for camera glitching
			//we compare signs here
			if (Input.GetAxis("Mouse X")*oldh < 0 &&(lastPointMade==null || Vector3.Distance(lastPointMade.position,mouseWorld)>threshold*5)||
			    Input.GetAxis("Mouse Y")*oldv < 0 &&(lastPointMade==null || Vector3.Distance(lastPointMade.position,mouseWorld)>threshold*5)||
			    suturepointCount == 0)
			{
				/* use this for anti circle*/
				if (suturepointCount!=0){
					if (Input.GetAxis("Mouse X")*oldh < 0)
						suturepointHorizontalCount++;
					else
						suturepointVerticalCount++;
				}
				/* use this for anti circle*/
				suturepointCount++;
				//So music plays
				if (effectSound.isPlaying == true)effectSound.Stop();
				effectSound.Play();
				
				/* all of this code is to try and get the TARGETOBJECT
				*	I first make OLDPOINT A the first suture point thats not GLITCHED for android u make
				*	then i draw linecasts from A to all the other suture point cubes and if it intersects a sutureable
				*	then i dont need to run the code here so thats why i have this if below this line
				*/
				if (targetObject==null){
				GameObject suturePt = (GameObject)Instantiate(suturePoint,mouseWorld,Quaternion.identity);
				//suturePt.transform.forward = Vector3.Normalize(new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y")));
				if (suturepointCount==2) {
					suturePt.tag = "SuturePT_A";
					suturePtOLD_A = suturePt.transform;
					lastPointMade = suturePtOLD_A;
					//here we can do the logic to see if it does suture right
				}
				else {
					
					suturePt.tag = "SuturePT_B";
					suturePtOLD_B = suturePt.transform;
					//here we can do the logic to see if it does suture right
					onSutureLineCreated();
					lastPointMade = suturePtOLD_B;
				}
				
				suturePt.transform.parent = transform;
				} else {
					lineRenderer.SetColors(Color.green,Color.green); //successful color
				}
			}
			oldh = Input.GetAxis("Mouse X");
			oldv = Input.GetAxis("Mouse Y");
        	//float oldv = Input.GetAxis("Mouse Y");
			UpdateLine();
		} else {
			clearLine();
		}
	}
	 
	private void onSutureLineCreated(){
		if (targetObject!=null) return;
		lineRenderer.SetColors(Color.white,Color.white);
			if (suturePtOLD_A!=null && suturePtOLD_B!=null) {
		RaycastHit hit;
		totalLinecasts++;
		if (Physics.Linecast(suturePtOLD_A.position,suturePtOLD_B.position,out hit)){
			if (hit.transform.tag.Equals("Sutureable")){
				successfulLinecasts++;
				//Debug.Log("You just hit "+successfulLinecasts+"/"+totalLinecasts);
				//the total line casts needed is 4
					targetObject = hit.transform.gameObject;
				
			}
		}
	}
	}
	
	//the magic is here after creating the line we will look at each point to see if its inside the sutureable or not
	void checkLineForSutures(){
		bool isLineInsideTarget=false; // a successful suture will be outside inside REPEAT
		int sutureTimes = 0; //how many suture stitches were made
		foreach( Vector3 pt in linePoints ){
			if (isLineInsideTarget!=targetObject.collider.bounds.Contains(pt)){
				sutureTimes++;}
			isLineInsideTarget = targetObject.collider.bounds.Contains(pt);
		}
		Debug.Log("sutured times : " + sutureTimes);
	}
	void UpdateLine()
	{
	    lineRenderer.SetWidth(startWidth, endWidth);
	    lineRenderer.SetVertexCount(linePoints.Count);
		if (linePoints.Count>0)lineRenderer.SetPosition(0,linePoints[0]); //this fixes the line for the glitch
	    for(int i = lineCount; i < linePoints.Count; i++)
	    {
			//Debug.Log(linePoints[i].ToString());
	    	lineRenderer.SetPosition(i, linePoints[i]);
	    }
	    lineCount = linePoints.Count;
	}
	void clearLine(){
		lineRenderer.SetVertexCount(0);
		linePoints.Clear();
		targetObject = null;
	}
	
	private void checkIfSutureIsBad()
	{
		
			/*circle test via horizontal and vertical suture point counts
			if (Mathf.Abs(suturepointHorizontalCount-suturepointVerticalCount) == 0 ){
			Debug.Log("CIRCLE LOL!");
			targetObject=null;
			return;
			}
			*/
			float diff = Mathf.Abs(totalY-totalX); //if the difference is low
			float sum = Mathf.Abs(totalY+totalX); //and the sum is high
			float circleNess = (1.0f- (diff/sum)) * 100.0f;
			//gtext.text = circleNess.ToString()+"% Circle-Like";
			//a circleness of 80.0f or higher is reasonably fair to not work
			
			/* REJECT BAD SUTURES */
			if (circleNess > 80.0f)targetObject = null; //SET NULL SINCE ITS CIRCLE
		
			
	}
}