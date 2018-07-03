﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundSourceTranslationController))]
public class SoundSourceTranslationControllerEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		SoundSourceTranslationController controller = (SoundSourceTranslationController)target;

		// Positioning
		GUILayout.Label("\nPosition at:");	
		if (GUILayout.Button("LF")) {
			controller.PositionSoundSource(controller.leftFront);
		}		
		if (GUILayout.Button("RF")) {
			controller.PositionSoundSource(controller.rightFront);
		}	
		if (GUILayout.Button("LB")) {
			controller.PositionSoundSource(controller.leftBack);
		}	
		if (GUILayout.Button("RB")) {
			controller.PositionSoundSource(controller.rightBack);
		}
		if (GUILayout.Button("C")) {
			controller.PositionSoundSource(controller.center);
		}

		// Translations
		GUILayout.Label("\nTranslate From-To:");	
		if (GUILayout.Button("C-RF")) {
			controller.StartTranslateSoundSource(controller.center, controller.rightFront);
		}
		if (GUILayout.Button("RF-C")) {
			controller.StartTranslateSoundSource(controller.rightFront, controller.center);
		}
		if (GUILayout.Button("C-LF")) {
			controller.StartTranslateSoundSource(controller.center, controller.leftFront);
		}
		if (GUILayout.Button("LF-C")) {
			controller.StartTranslateSoundSource(controller.leftFront, controller.center);
		}

		if (GUILayout.Button("C-RB")) {
			controller.StartTranslateSoundSource(controller.center, controller.rightBack);
		}
		if (GUILayout.Button("RB-C")) {
			controller.StartTranslateSoundSource(controller.rightBack, controller.center);
		}
		if (GUILayout.Button("C-LB")) {
			controller.StartTranslateSoundSource(controller.center, controller.leftBack);
		}
		if (GUILayout.Button("LB-C")) {
			controller.StartTranslateSoundSource(controller.leftBack, controller.center);
		}

		if (GUILayout.Button("RF-LF")) {
			controller.StartTranslateSoundSource(controller.rightFront, controller.leftFront);
		}
		if (GUILayout.Button("LF-RF")) {
			controller.StartTranslateSoundSource(controller.leftFront, controller.rightFront);
		}

		if (GUILayout.Button("LF-LB")) {
			controller.StartTranslateSoundSource(controller.leftFront, controller.leftBack);
		}
		if (GUILayout.Button("LB-LF")) {
			controller.StartTranslateSoundSource(controller.leftBack, controller.leftFront);
		}

		if (GUILayout.Button("LB-RB")) {
			controller.StartTranslateSoundSource(controller.leftBack, controller.rightBack);
		}
		if (GUILayout.Button("RB-LB")) {
			controller.StartTranslateSoundSource(controller.rightBack, controller.leftBack);
		}

		if (GUILayout.Button("RB-RF")) {
			controller.StartTranslateSoundSource(controller.rightBack, controller.rightFront);
		}
		if (GUILayout.Button("RF-RB")) {
			controller.StartTranslateSoundSource(controller.rightFront, controller.rightBack);
		}

		// Stop
		GUILayout.Label("\nStop:");
		if (GUILayout.Button("Stop Translation")) {
			controller.StopTranslation();
		}
	}
}
