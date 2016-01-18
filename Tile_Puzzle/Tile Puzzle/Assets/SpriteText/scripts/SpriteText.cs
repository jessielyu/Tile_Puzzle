//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SpriteText : MonoBehaviour
{
	public SpriteFont Font
	{
		get { return m_font; }
		set
		{
			if (m_font != value)
			{
				m_font = value;
				Commit();
			}
		}
	}

	public string SortingLayer
	{
		get
		{
			string layer = Renderer.sortingLayerName;
			if (layer == "")
			{
				return "Default";
			}
			return layer;
		}
		set
		{
			Renderer.sortingLayerName = value;
		}
	}

	public int OrderInLayer
	{
		get { return Renderer.sortingOrder; }
		set { Renderer.sortingOrder = value; }
	}

	public string Text
	{
		get { return m_text; }
		set
		{
			if (m_text != value)
			{
				m_text = value;
				if (InlineColor)
				{
					SetChanged(SpriteTextFlags.UpdateBuffer);
				}
				else
				{
					SetChanged(SpriteTextFlags.UpdateGeom);
				}
			}
		}
	}

	public string DisplayedText
	{
		get { return Geom.DisplayedText; }
	}

	public int Capacity
	{
		get { return m_capacity; }
		set
		{
			value = Mathf.Max(value, 0);
			if (m_capacity != value)
			{
				m_capacity = value;
				SetChanged(SpriteTextFlags.UpdateBuffer);
			}
		}
	}

	public TextAnchor Pivot
	{
		get { return m_pivot; }
		set
		{
			if (m_pivot != value)
			{
				m_pivot = value;
				SetChanged(SpriteTextFlags.UpdateGeom);
			}
		}
	}

	public TextAlignment Alignment
	{
		get { return m_alignment; }
		set
		{
			if (m_alignment != value)
			{
				m_alignment = value;
				SetChanged(SpriteTextFlags.UpdateGeom);
			}
		}
	}

	public bool LineWrap
	{
		get { return m_lineWrap; }
		set
		{
			if (m_lineWrap != value)
			{
				m_lineWrap = value;
				SetChanged(SpriteTextFlags.UpdateGeom);
			}
		}
	}

	public float LineWidth
	{
		get { return m_lineWidth; }
		set
		{
			if (m_lineWidth != value)
			{
				m_lineWidth = value;
				SetChanged(SpriteTextFlags.UpdateGeom);
			}
		}
	}

	public float Scale
	{
		get { return m_scale; }
		set
		{
			value = Mathf.Max(value, 0.0f);
			if (m_scale != value)
			{
				m_scale = value;
				SetChanged(SpriteTextFlags.UpdateGeom);
			}
		}
	}

	public bool Gradient
	{
		get { return m_gradient; }
		set
		{
			if (m_gradient != value)
			{
				m_gradient = value;
				SetChanged(SpriteTextFlags.UpdateColor);
			}
		}
	}

	public bool InlineColor
	{
		get { return m_inlineColor; }
		set
		{
			if (m_inlineColor != value)
			{
				m_inlineColor = value;
				SetChanged(SpriteTextFlags.UpdateBuffer);
			}
		}
	}

	public Color Color1
	{
		get { return m_color1; }
		set
		{
			if (m_color1 != value)
			{
				m_color1 = value;
				SetChanged(SpriteTextFlags.UpdateColor);
			}
		}
	}

	public Color Color2
	{
		get { return m_color2; }
		set
		{
			if (m_color2 != value)
			{
				m_color2 = value;
				SetChanged(SpriteTextFlags.UpdateColor);
			}
		}
	}

	public float Transparency
	{
		get { return m_transparency; }
		set
		{
			value = Mathf.Clamp(value, 0.0f, 1.0f);
			if (m_transparency != value)
			{
				m_transparency = value;
				if (InlineColor)
				{
					SetChanged(SpriteTextFlags.UpdateBuffer);
				}
				else
				{
					SetChanged(SpriteTextFlags.UpdateColor);
				}
			}
		}
	}

	public MeshRenderer Renderer
	{
		get
		{
			if (m_renderer == null)
			{
				m_renderer = GetComponent<MeshRenderer>();
			}
			return m_renderer;
		}
	}

	public MeshFilter MeshFilter
	{
		get
		{
			if (m_meshFilter == null)
			{
				m_meshFilter = GetComponent<MeshFilter>();
			}
			return m_meshFilter;
		}
	}

	private Mesh Mesh
	{
		get
		{
			if (MeshFilter.sharedMesh == null)
			{
				MeshFilter.sharedMesh = new Mesh();
				MeshFilter.sharedMesh.name = GetInstanceID().ToString();
			}
			return MeshFilter.sharedMesh;
		}
	}

	private SpriteTextGeom Geom
	{
		get
		{
			if (m_spriteTextGeom == null)
			{
				m_spriteTextGeom = new SpriteTextGeom(this);
				m_spriteTextGeom.SetChanged(SpriteTextFlags.UpdateBuffer);
			}
			return m_spriteTextGeom;
		}
	}

	protected virtual void Awake()
	{
		if (MeshFilter.sharedMesh != null && MeshFilter.sharedMesh.name != GetInstanceID().ToString())
		{
			MeshFilter.sharedMesh = null;
		}
		Commit();
	}

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
	}

	protected virtual void LateUpdate()
	{
		if (m_font != null && Geom.NeedUpdate)
		{
			Geom.Recalc(Mesh);
		}
	}

	protected virtual void OnDestroy()
	{
		if (Application.isPlaying)
		{
			Destroy(Mesh);
		}
		else
		{
			DestroyImmediate(Mesh);
		}
	}

	public virtual void Commit()
	{
		if (m_font != null)
		{
			Renderer.sharedMaterials = m_font.Materials;
			Geom.SetChanged(SpriteTextFlags.UpdateBuffer);
		}
		else
		{
			Renderer.sharedMaterials = new Material[0];
			Mesh.Clear();
		}
	}

	public void Append(string text)
	{
		Text += text;
	}

	public void Append(string text, Color32 color)
	{
		Text += "[" + ColorToHex(color) + ":" + text + "]";
	}

	public void Append(string text, Color32 color1, Color32 color2)
	{
		Text += "[" + ColorToHex(color1) + " " + ColorToHex(color2) + ":" + text + "]";
	}

	private void SetChanged(SpriteTextFlags flag)
	{
		Geom.SetChanged(flag);
	}

	private static string ColorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		if (color.a != 255)
		{
			hex += color.a.ToString("X2");
		}
		return hex;
	}

	[SerializeField]
	[HideInInspector]
	private SpriteFont m_font = null;

	[SerializeField]
	private string m_text = "SpriteText";
	[SerializeField]
	private int m_capacity = 20;
	[SerializeField]
	private TextAnchor m_pivot = TextAnchor.MiddleCenter;
	[SerializeField]
	private TextAlignment m_alignment = TextAlignment.Center;
	[SerializeField]
	private bool m_lineWrap = false;
	[SerializeField]
	private float m_lineWidth = 0.0f;
	[SerializeField]
	private float m_scale = 1.0f;
	[SerializeField]
	private bool m_gradient = false;
	[SerializeField]
	private bool m_inlineColor = false;
	[SerializeField]
	private Color m_color1 = Color.white;
	[SerializeField]
	private Color m_color2 = Color.white;
	[SerializeField]
	private float m_transparency = 0.0f;

	private SpriteTextGeom m_spriteTextGeom = null;
	private MeshRenderer m_renderer = null;
	private MeshFilter m_meshFilter = null;
}