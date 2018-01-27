using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Firewall))]
public class FirewallEditor : Editor {

	private Firewall firewall;
	SerializedProperty offsetProp;

	void OnEnable() {
		// Setup properties
		offsetProp = serializedObject.FindProperty("");
	}

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

		firewall = target as Firewall;

		EditorGUILayout.Slider(offsetProp, 0f, 3.6f, new GUIContent("Offset from Top"));

		float collisionOffsetFromTop = offsetProp.floatValue;

		BoxCollider2D bc = firewall.GetComponent<BoxCollider2D>();
		bc.size = new Vector2(7.2f, 3.6f - collisionOffsetFromTop);
		bc.offset = new Vector2(0.0f, -collisionOffsetFromTop / 2);
		serializedObject.ApplyModifiedProperties ();

	}
}
