using UnityEngine;
using System.Collections;

public class ScreenHelper : MonoBehaviour
{
    [HideInInspector]
    public int m_screen_width
    {
        get;
        private set;
    }
    [HideInInspector]
    public int m_screen_height
    {
        get;
        private set;
    }

    private Resolution m_screen_resolution;

    // Fade to black and logo stuff
    public Texture2D m_black_texture;
	public Texture2D m_logo;
    public enum FADE_STATUS { NORMAL, FADE, LOGO };
    public FADE_STATUS m_fade_status;
    private Timer m_timer;

    public float m_level_restart;
    public float m_fade_logo;

    void Start()
    {
        InitializeScreen();

        Screen.showCursor = false;
    }

	public void Update()
	{
		InitializeScreen();
	}

    private void InitializeScreen()
    {
        //m_screen_resolution = Screen.currentResolution;
        m_screen_width = Screen.width;
        m_screen_height = Screen.height;
    }

    public float W_to_px(float perc)
    {
        return (m_screen_width * perc);
    }

    public float H_to_px(float perc)
    {
        return (m_screen_height * perc);
    }

    public void StartLogoFade()
    {
		if (m_fade_status == FADE_STATUS.NORMAL)
	    {
			m_fade_status = FADE_STATUS.LOGO /*FADE_STATUS.FADE*/;
			m_timer = Timer.CreateTimer(m_level_restart);
		}
    }

    public void OnGUI()
    {
        switch (m_fade_status)
        {
            case FADE_STATUS.NORMAL:

                break;
            case FADE_STATUS.FADE:
                GUI.color = new Color(1f, 1f, 1f, m_timer.GetProgress());
                GUI.DrawTexture(new Rect(0, 0, m_screen_width, m_screen_height), m_black_texture);

                break;
            case FADE_STATUS.LOGO:
			/*
                GUI.color = new Color(1f, 1f, 1f, 1f);
                GUI.DrawTexture(new Rect(0, 0, m_screen_width, m_screen_height), m_black_texture, ScaleMode.StretchToFill, true);
			 */
                GUI.color = new Color(1f, 1f, 1f, m_timer.GetProgress());
				GUI.DrawTexture(new Rect(W_to_px(0.25f), H_to_px(0.10f), W_to_px(0.5f), W_to_px(0.5f)), m_logo, ScaleMode.StretchToFill, true);
			
				if (m_timer.HasEnded())
				{
					Map.Reload();
					//Application.LoadLevel(Application.loadedLevel);
				}

			
			break;
		}
	}
}
