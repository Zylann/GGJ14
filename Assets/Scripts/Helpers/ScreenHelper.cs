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
    [HideInInspector]
    public float m_ratio
    {
        get;
        private set;
    }

    private Resolution m_screen_resolution;

    // Fade to black and logo stuff
    private Texture2D m_black_texture;
    private Texture2D m_logo;
    public enum FADE_STATUS { NORMAL, FADE, LOGO };
    public FADE_STATUS m_fade_status;
    private Timer m_timer;

    public float m_fade_black;
    public float m_fade_logo;

    void Start()
    {
        InitializeScreen();

        m_black_texture = Resources.Load("Textures/GUI/black") as Texture2D;

        m_logo = Resources.Load("Textures/GUI/logo_ascent") as Texture2D;

        Screen.showCursor = false;
    }

    private void InitializeScreen()
    {
        m_screen_resolution = Screen.currentResolution;
        m_screen_width = (int)(m_screen_resolution.width);
        m_screen_height = (int)(m_screen_resolution.height);
        m_ratio = m_screen_width / m_screen_height;
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
        m_fade_status = FADE_STATUS.FADE;
        m_timer = Timer.CreateTimer(m_fade_black);
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

                if (m_timer.HasEnded())
                {
                    m_timer.Restart(m_fade_logo);
                    m_fade_status = FADE_STATUS.LOGO;
                }

                break;
            case FADE_STATUS.LOGO:

                GUI.color = new Color(1f, 1f, 1f, 1f);
                GUI.DrawTexture(new Rect(0, 0, m_screen_width, m_screen_height), m_black_texture, ScaleMode.StretchToFill, true);
                GUI.color = new Color(1f, 1f, 1f, m_timer.GetProgress());
                GUI.DrawTexture(new Rect(0, 0, m_screen_width, m_screen_height), m_logo, ScaleMode.StretchToFill, true);


                break;
        }
    }
}
