//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using UnityEngine;

public class FpsSprite : MonoBehaviour
{
	private void Start()
	{
		m_timeleft = m_updateInterval;
	}

	private void Update()
	{
		m_timeleft -= Time.deltaTime;
		m_accum += Time.timeScale / Time.deltaTime;
		++m_frames;

		// Interval ended - update GUI text and start new interval
		if (m_timeleft <= 0.0)
		{
			// display two fractional digits (f2 format)
			float fps = m_accum / m_frames;
			m_spriteTetxt.Text = "FPS: ";
			Color color;
			if (fps < 30)
			{
				color = Color.yellow;
			}
			else
			{
				color = fps < 10 ? Color.red : Color.green;
			}
			m_spriteTetxt.Append(string.Format("{0:F2}", fps), color);
			m_timeleft = m_updateInterval;
			m_accum = 0.0F;
			m_frames = 0;
		}
	}

	[SerializeField]
	private float m_updateInterval = 0.2f;
	[SerializeField]
	private SpriteText m_spriteTetxt = null;

	private float m_accum = 0;
	private int m_frames = 0;
	private float m_timeleft;
}