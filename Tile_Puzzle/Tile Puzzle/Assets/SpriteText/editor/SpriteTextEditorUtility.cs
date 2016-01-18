//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

public static class SpriteTextEditorUtility
{
	static SpriteTextEditorUtility()
	{
		s_useGradient = EditorPrefs.GetBool(UseGradientKey, false);
		s_color1 = LoadColor("1");
		s_color2 = LoadColor("2");
	}

	[MenuItem("Assets/Create/Sprite Font")]
	private static void CreateFont()
	{
		SpriteFont font = CreateAsset<SpriteFont>();
		EditorUtility.SetDirty(font);
	}

	[MenuItem("GameObject/Create Other/Sprite Text")]
	public static void CreateGameObjectInScene()
	{
		string[] guids = AssetDatabase.FindAssets("t:spritefont");
		SpriteFont font = null;
		if (guids.Length > 0)
		{
			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			font = (SpriteFont)AssetDatabase.LoadAssetAtPath(path, typeof(SpriteFont));
		}

		GameObject go = new GameObject("SpriteText");
		if (Selection.activeGameObject != null)
		{
			string assetPath = AssetDatabase.GetAssetPath(Selection.activeGameObject);
			if (assetPath.Length == 0)
			{
				go.transform.parent = Selection.activeGameObject.transform;
			}
		}
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one;
		go.AddComponent<SpriteText>().Font = font;
	}

	public static T CreateAsset<T>(string dir = null, string name = null)
		where T : ScriptableObject
	{
		if (dir == null)
		{
			foreach (Object obj in Selection.objects)
			{
				if (obj.GetType() == typeof(Object))
				{
					string path = AssetDatabase.GetAssetPath(obj);

					FileAttributes fa = File.GetAttributes(path);
					if ((fa & FileAttributes.Directory) != 0)
					{
						dir = path;
					}
				}
			}

			if (dir == null)
			{
				dir = "Assets";
			}
		}

		if (name == null)
		{
			name = typeof(T).Name;
		}

		string assetPath = AssetDatabase.GenerateUniqueAssetPath(dir + "/" + name + ".asset");
		T asset = ScriptableObject.CreateInstance<T>();
		EditorUtility.SetDirty(asset);
		AssetDatabase.CreateAsset(asset, assetPath);
		Selection.activeObject = asset;

		return asset;
	}

	public static string[] GetSortingLayers()
	{
		Type internalEditorUtilityType = typeof(InternalEditorUtility);
		PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames",
																				   BindingFlags.Static |
																				   BindingFlags.NonPublic);
		string[] sortingLayers = (string[])sortingLayersProperty.GetValue(null, new object[0]);
		return sortingLayers;
	}

	public static void DrawHexConverter()
	{
		//color to hex converter utility
		GUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("Hex Converter");
		Color hcolor = EditorGUILayout.ColorField("Color 1", s_color1);
		if (hcolor != s_color1)
		{
			s_color1 = hcolor;
			SaveColor(s_color1, "1");
		}
		hcolor = EditorGUILayout.ColorField("Color 2", s_color2);
		if (hcolor != s_color2)
		{
			s_color2 = hcolor;
			SaveColor(s_color2, "2");
		}
		string hexcolor1 = ColorToHex(s_color1);
		string hexcolor2 = ColorToHex(s_color2);
		bool gradient = EditorGUILayout.Toggle("Use Gradient", s_useGradient);
		if (gradient != s_useGradient)
		{
			s_useGradient = gradient;
			EditorPrefs.SetBool(UseGradientKey, s_useGradient);
		}
		if (!s_useGradient)
		{
			EditorGUILayout.TextField("Hex Format", "[" + hexcolor1 + ": ]");
		}
		else
		{
			EditorGUILayout.TextField("Hex Format", "[" + hexcolor1 + " " + hexcolor2 + ": ]");
		}
		GUILayout.EndVertical();
	}

	public static string ColorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		if (color.a != 255)
		{
			hex += color.a.ToString("X2");
		}
		return hex;
	}

	private static Color LoadColor(string key)
	{
		Color color = new Color();
		color.r = EditorPrefs.GetFloat(ColorKey + key + "_r", 1.0f);
		color.g = EditorPrefs.GetFloat(ColorKey + key + "_g", 1.0f);
		color.b = EditorPrefs.GetFloat(ColorKey + key + "_b", 1.0f);
		color.a = EditorPrefs.GetFloat(ColorKey + key + "_a", 1.0f);
		return color;
	}

	private static void SaveColor(Color color, string key)
	{
		EditorPrefs.SetFloat(ColorKey + key + "_r", color.r);
		EditorPrefs.SetFloat(ColorKey + key + "_g", color.g);
		EditorPrefs.SetFloat(ColorKey + key + "_b", color.b);
		EditorPrefs.SetFloat(ColorKey + key + "_a", color.a);
	}

	private static bool s_useGradient = false;
	private static Color s_color1 = Color.white;
	private static Color s_color2 = Color.white;

	private const string ColorKey = "sprite_text_inspector_color";
	private const string UseGradientKey = "sprite_text_inspector_use_gradient";
}