using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalReader : MonoBehaviour
{
    [SerializeField] PauseHandler pauseHandler;
    [SerializeField] GameObject GFX;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject soundDrop;
    private void Start()
    {
        GFX.SetActive(false);
    }

    public void NewLog(string value)
    {
        GameObject sounddrop = Instantiate(soundDrop);
        textMesh.text = EnsureMinLinesAfterCurrentText(value);
        StartCoroutine(ScrollToTopNextFrame());
        GFX.SetActive(true);
        pauseHandler.PauseGame();
    }

    public void EndLog()
    {
        GameObject sounddrop = Instantiate(soundDrop);
        pauseHandler.ResumeGame();
        GFX.SetActive(false);
    }

    string EnsureMinLinesAfterCurrentText(string value)
    {
        string originalText = value;
        originalText = "\n" + originalText;
        int currentLineCount = CountLines(originalText);

        if (currentLineCount < 17)
        {
            int linesToAdd = 17 - currentLineCount;
            originalText += new string('\n', linesToAdd);
        }

        return originalText;
    }

    int CountLines(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0;

        int lineCount = 1;
        int position = 0;

        while ((position = text.IndexOf('\n', position)) != -1)
        {
            lineCount++;
            position++; // Move past the '\n'
        }

        return lineCount;
    }

    private IEnumerator ScrollToTopNextFrame()
    {
        // Wait for the end of frame to ensure the text has been updated
        yield return new WaitForEndOfFrame();
        // Set the scroll position to the top
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
