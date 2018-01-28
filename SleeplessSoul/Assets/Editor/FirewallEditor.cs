using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Firewall))]
public class FirewallEditor : Editor {

	private Firewall firewall;
	float offsetProp;

	void OnEnable() {
		// Setup properties
	}

	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector ();
		serializedObject.Update();

		firewall = target as Firewall;

        EditorGUI.BeginChangeCheck();
		offsetProp = EditorGUILayout.Slider("Offset from Top", offsetProp, 0f, 3.6f);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(firewall, "Change Offset");
		    BoxCollider2D bc = firewall.GetComponent<BoxCollider2D>();
		    bc.size = new Vector2(7.2f, 3.6f - offsetProp);
		    bc.offset = new Vector2(0.0f, 3.6f - offsetProp / 2);
		    serializedObject.ApplyModifiedProperties ();
            EditorUtility.SetDirty(firewall);
        }
	}
}
