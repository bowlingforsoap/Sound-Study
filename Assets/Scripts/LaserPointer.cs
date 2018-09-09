﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserPointer : MonoBehaviour {

	public GameObject buildingCachingController;
	public SoundSourceTranslationController soundSourceTranslationController;
	public GameObject rectilePrefab;

	public Material guessIndicatorMaterial;
    public LayerMask raycastMask;
    public GameObject laserPrefab;

	private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
	private GameObject rectile;
	private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
	private float hitDistance;
	private const float maxHitDistance = 200f;

	private bool selectionAllowed = true;
	private bool ready = false;
	public bool Ready {
		get { return ready; }
	}

	private bool controllerActive = false;

    void Awake () {

	}

    void Start()
    {
        trackedObj = buildingCachingController.GetComponent<SteamVR_TrackedObject>();
		StartCoroutine(WaitUntilTheControllerIsActive());
        
		laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

		rectile = Instantiate(rectilePrefab, Vector3.zero, Quaternion.identity);
		rectile.SetActive(false);

		/* guessIndicators[0] = Instantiate(rectilePrefab, Vector3.zero, Quaternion.identity);
		guessIndicators[0].GetComponent<MeshRenderer>().material = guessMaterial1;
		guessIndicators[0].SetActive(false);
		guessIndicators[1] = Instantiate(rectilePrefab, Vector3.zero, Quaternion.identity);
		guessIndicators[1].GetComponent<MeshRenderer>().material = guessMaterial2;
		guessIndicators[1].SetActive(false); */
    }
	
	// Update is called once per frame
	void Update () {
			if (controllerActive) 
			{
				RaycastHit hit;
				bool hitSomething = Physics.Raycast(trackedObj.transform.position, buildingCachingController.transform.forward, out hit, maxHitDistance, raycastMask);
				
				// Show laser
				if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Axis0)) // Joystick/Touchpad touch
				{
					// Debug.Log("Joystick/Touchpad touch");
					
					if (hitSomething)
					{
						hitPoint = hit.point;
						hitDistance = hit.distance;
						
						// TODO: remove
						ShowRectile(hit.point);
					} else {
						hitPoint = buildingCachingController.transform.position + buildingCachingController.transform.forward * maxHitDistance;
						hitDistance = maxHitDistance;
						
						// TODO: remove
						HideRectile();
					}

					ShowLaser();
				} else {
					HideLaser();
					HideRectile();
				}
				

				if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && selectionAllowed) // Joystick/Touchpad press
				// if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger)) // Hair Trigger press
				{
					if (hitSomething)
					{
						// Store correct guess
						// ...

						StartCoroutine(IndicateCorrectGuess(hit.collider.gameObject));
						
					}
					else
					{
						// Store false guess
						// ...
						
						// StartCoroutine(IndicateIncorrectGuess(hit.collider.gameObject));
					}
				}
			}
			
	}

	private IEnumerator WaitUntilTheControllerIsActive() {
		while (true)
		{
			yield return null;

			if (buildingCachingController.activeSelf)
			{
				// Debug.Log("Building Catching Controller became active!");
				controllerActive = true;
			}
			else 
			{
				continue;
			}

			break;
		}

		ready = true;
	}

	private void ShowLaser()
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f); // Place the laser in the middle between the controller and the hitPoint
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hitDistance);
    }

	private void ShowRectile(Vector3 position) {
		rectile.transform.position = position;
		rectile.SetActive(true);
	}

	private void HideRectile() {
		rectile.SetActive(false);
	}

	private void HideLaser() {
		laser.SetActive(false);
	}

	private IEnumerator IndicateCorrectGuess(GameObject go) {
		soundSourceTranslationController.StopTranslation(destroyChildren: false);
		
		MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
		meshRenderer.material = guessIndicatorMaterial;
		meshRenderer.enabled = true;

		selectionAllowed = false;
		yield return new WaitForSeconds(1f);
		selectionAllowed = true;

		if (meshRenderer != null) {
			meshRenderer.enabled = false;
		}

		soundSourceTranslationController.DestroyChildren(go.transform.parent.parent.gameObject);
	}

	private IEnumerator IndicateIncorrectGuess(GameObject go) {
		yield return null;
	}

	/* public static void PlaceGuessIndicator(int guessIndicator, Vector3 point) {
		guessIndicators[guessIndicator].transform.position = point;
		guessIndicators[guessIndicator].SetActive(true);
	} */

	/* public static IEnumerator HideGuessesIndicator() {
		yield return new WaitForSeconds(.3f); 
		guessIndicators[0].SetActive(false);
		guessIndicators[1].SetActive(false);
	} */
}
