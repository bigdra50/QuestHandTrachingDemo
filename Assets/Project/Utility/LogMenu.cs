using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogMenu : MonoBehaviour
{
    [SerializeField] private Text m_textUI = null;
    [SerializeField] private TMP_Text m_tmpText = null;

    private void Awake()
    {
        Application.logMessageReceived += OnLogMessage;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived += OnLogMessage;
    }

    private void OnLogMessage(string i_logText, string i_stackTrace, LogType i_type)
    {
        if (string.IsNullOrEmpty(i_logText))
        {
            return;
        }

        if (m_textUI != null)
        {
            m_textUI.text += i_logText + System.Environment.NewLine;
        }

        if (m_tmpText != null)
        {
            m_tmpText.text += i_logText + System.Environment.NewLine;
        }
    }
}