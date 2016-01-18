//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CanEditMultipleObjects]
[CustomEditor(typeof(SpriteText))]
class SpriteTextInspector : Editor
{
	protected virtual void OnEnable()
	{
		string[] guids = AssetDatabase.FindAssets("t:spritefont");
		m_fonts = new List<SpriteFont>();
		m_fontsNames = new string[guids.Length];
		for (int i = 0; i < guids.Length; i++)
		{
			string path = AssetDatabase.GUIDToAssetPath(guids[i]);
			m_fonts.Add((SpriteFont)AssetDatabase.LoadAssetAtPath(path, typeof(SpriteFont)));
			m_fontsNames[i] = m_fonts[i].name;
		}

		m_target = (SpriteText)target;
		m_targets = new List<SpriteText>();
		for (int i = 0; i < targets.Length; i++)
		{
			m_targets.Add((SpriteText)targets[i]);
		}
	}

	protected virtual void OnSceneGUI()
	{
		foreach (SpriteText spriteText in m_targets)
		{
			if (spriteText.LineWrap)
			{
				OnSceneGUISpriteText(spriteText);
			}
		}
	}

	private void OnSceneGUISpriteText(SpriteText spriteText)
	{
		Transform transform = spriteText.transform;
		float xScale = transform.lossyScale.x * spriteText.Scale;
		if (xScale == 0.0f)
		{
			return;
		}
		int side = xScale > 0.0f ? 1 : -1;
		float width = Mathf.Abs(spriteText.LineWidth * xScale);
		Vector3 p0 = Vector3.zero, p1 = Vector3.zero, dir = Vector3.zero;
		float top = 0.0f, bot = 0.0f;
		p0 = transform.position;
		switch (spriteText.Pivot)
		{
			case TextAnchor.UpperRight:
			case TextAnchor.MiddleRight:
			case TextAnchor.LowerRight:
				p1 = p0 - width * transform.right * side;
				dir = -transform.right * side;
				break;
			case TextAnchor.UpperCenter:
			case TextAnchor.MiddleCenter:
			case TextAnchor.LowerCenter:
				p0 = p0 - (width / 2) * transform.right * side;
				p1 = p0 + width * transform.right * side;
				dir = transform.right * side;
				break;
			case TextAnchor.UpperLeft:
			case TextAnchor.MiddleLeft:
			case TextAnchor.LowerLeft:
				p1 = p0 + width * transform.right * side;
				dir = transform.right * side;
				break;
		}

		switch (spriteText.Pivot)
		{
			case TextAnchor.LowerLeft:
			case TextAnchor.LowerCenter:
			case TextAnchor.LowerRight:
				top = 1.6f;
				bot = 0.4f;
				break;
			case TextAnchor.MiddleLeft:
			case TextAnchor.MiddleCenter:
			case TextAnchor.MiddleRight:
				top = 1.0f;
				bot = 1.0f;
				break;
			case TextAnchor.UpperLeft:
			case TextAnchor.UpperCenter:
			case TextAnchor.UpperRight:
				top = 0.4f;
				bot = 1.6f;
				break;
		}
		Handles.DrawLine(p0, p1);
		Handles.DrawLine(p0, p0 + transform.up * top);
		Handles.DrawLine(p0, p0 - transform.up * bot);
		Handles.DrawLine(p1, p1 + transform.up * top);
		Handles.DrawLine(p1, p1 - transform.up * bot);
		p1 = Handles.Slider(p1, dir, HandleUtility.GetHandleSize(p1) / 10, Handles.CubeCap, 0.0f);
		p1 = Handles.Slider(p1, dir, HandleUtility.GetHandleSize(p1), Handles.ArrowCap, 0.0f);

		float lineWidth = Mathf.Abs(Vector3.Distance(p0, p1) / xScale);
		if (lineWidth != spriteText.LineWidth)
		{
			Undo.RecordObjects(new Object[] { spriteText }, "Sprite Settings Changed From Inspector");
			spriteText.LineWidth = Mathf.Abs(Vector3.Distance(p0, p1) / xScale);
			spriteText.Commit();
		}
	}

	public override void OnInspectorGUI()
	{
		m_changed = false;

		//check UndoRedo
		UndoRedoPerformed();

		//draw default inspector for m_Script property
		EditorGUI.BeginChangeCheck();
		SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
		serializedObject.Update();
		EditorGUILayout.PropertyField(scriptProp, true, new GUILayoutOption[0]);
		serializedObject.ApplyModifiedProperties();
		EditorGUI.EndChangeCheck();

		// set font for sprites
		Color back = GUI.backgroundColor;
		if (m_target.Font == null)
		{
			GUI.backgroundColor = Color.red;
		}

		SpriteFont font = m_target.Font;
		if (m_fonts.Count == 0)
		{
			EditorGUILayout.Popup("Font", 0, new [] { "No fonts found" });
		}
		else
		{
			int fontIndex = m_fonts.FindIndex(c => c == font);
			if (fontIndex == -1)
			{
				fontIndex = 0;
			}
			EditorGUILayout.BeginHorizontal();
			fontIndex = EditorGUILayout.Popup("Font", fontIndex, m_fontsNames);
			if (GUILayout.Button("→", GUILayout.Width(23), GUILayout.Height(15)))
			{
				EditorGUIUtility.PingObject(m_fonts[fontIndex]);
			}
			EditorGUILayout.EndHorizontal();
			font = m_fonts[fontIndex];
		}

		UndoAction(font != m_target.Font, c => c.Font = font);
		GUI.backgroundColor = back;

		// set sorting layer for text
		string[] layers = SpriteTextEditorUtility.GetSortingLayers();
		int prev = layers.ToList().IndexOf(m_target.SortingLayer);
		int curr = EditorGUILayout.Popup("Sorting Layer", prev, layers);
		curr = Mathf.Clamp(curr, 0, layers.Length - 1);
		UndoAction(curr != prev, c => c.SortingLayer = layers[curr]);

		// set order in layer for text
		int orderInLayer = EditorGUILayout.IntField("Order In Layer", m_target.OrderInLayer);
		UndoAction(orderInLayer != m_target.OrderInLayer, c => c.OrderInLayer = orderInLayer);

		//set capacity of text
		int capacity = EditorGUILayout.IntField("Capacity", m_target.Capacity);
		UndoAction(capacity != m_target.Capacity, c => c.Capacity = capacity);

		//set Pivot
		TextAnchor anchor = (TextAnchor)EditorGUILayout.EnumPopup("Pivot", m_target.Pivot);
		UndoAction(anchor != m_target.Pivot, c => c.Pivot = anchor);

		TextAlignment alignment = (TextAlignment)EditorGUILayout.EnumPopup("Alignment", m_target.Alignment);
		UndoAction(alignment != m_target.Alignment, c => c.Alignment = alignment);

		// set linewrap and lint width
		bool linewrap = EditorGUILayout.Toggle("Line Wrap", m_target.LineWrap);
		UndoAction(linewrap != m_target.LineWrap, c => c.LineWrap = linewrap);
		GUI.enabled = linewrap;
		float lineWidth = EditorGUILayout.FloatField("Line Width", m_target.LineWidth);
		UndoAction(lineWidth != m_target.LineWidth, c => c.LineWidth = lineWidth);
		GUI.enabled = true;

		//set text scale
		float scale = m_target.Scale;
		scale = EditorGUILayout.FloatField("Scale", scale);
		UndoAction(scale != m_target.Scale, c => c.Scale = scale);

		//set colors for sprites
		//GUILayout.BeginVertical("box");
		bool inlineColor = EditorGUILayout.Toggle("Use Inline", m_target.InlineColor);
		UndoAction(inlineColor != m_target.InlineColor, c => c.InlineColor = inlineColor);
		bool gradient = EditorGUILayout.Toggle("Use Gradient", m_target.Gradient);
		UndoAction(gradient != m_target.Gradient, c => c.Gradient = gradient);
		if (gradient)
		{
			Color c1 = EditorGUILayout.ColorField("Color 1", m_target.Color1);
			Color c2 = EditorGUILayout.ColorField("Color 2", m_target.Color2);
			UndoAction(c1 != m_target.Color1, c => c.Color1 = c1);
			UndoAction(c2 != m_target.Color2, c => c.Color2 = c2);
		}
		else
		{
			Color color = EditorGUILayout.ColorField("Color 1", m_target.Color1);
			bool guiEnabled = GUI.enabled;
			GUI.enabled = false;
			EditorGUILayout.ColorField("Color 2", m_target.Color2);
			GUI.enabled = guiEnabled;
			UndoAction(color != m_target.Color1, c => c.Color1 = color);
		}
		float transparency = EditorGUILayout.Slider("Transparency", m_target.Transparency, 0.0f, 1.0f);
		UndoAction(transparency != m_target.Transparency, c => c.Transparency = transparency);
		//GUILayout.EndVertical();

		// set text for sprites
		GUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("Text:");
		m_textScroll = GUILayout.BeginScrollView(m_textScroll, GUILayout.Height(100));
		string text = EditorGUILayout.TextArea(m_target.Text, GUILayout.MinHeight(95));
		GUILayout.EndScrollView();
		UndoAction(text != m_target.Text, c => c.Text = text);
		EditorGUILayout.EndVertical();

		SpriteTextEditorUtility.DrawHexConverter();

		if (GUILayout.Button("Commit"))
		{
			Action(true, c => c.Commit());
		}

		if (m_changed || GUI.changed)
		{
			Action(true, EditorUtility.SetDirty);
		}
	}

	protected void UndoRedoPerformed()
	{
		if (Event.current.type == EventType.ValidateCommand &&
			Event.current.commandName == "UndoRedoPerformed")
		{
			Action(true, c => c.Commit());
		}
	}

	protected void Action(bool expression, Action<SpriteText> action)
	{
		if (expression)
		{
			m_targets.ForEach(action);
			m_changed = true;
		}
	}

	private void UndoAction(bool expression, Action<SpriteText> action)
	{
		if (expression)
		{
			Undo.RecordObjects(targets, "Sprite Settings Changed From Inspector");
			m_targets.ForEach(action);
			m_changed = true;
		}
	}

	private SpriteText m_target = null;
	private List<SpriteText> m_targets = null;
	private bool m_changed = false;
	private Vector2 m_textScroll = Vector2.zero;
	private List<SpriteFont> m_fonts = new List<SpriteFont>();
	private string[] m_fontsNames = new string[0];
}
