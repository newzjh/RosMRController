using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_STANDALONE_WIN
using UnityEngine.Windows.Speech;
#endif

public class SpeechRecognitionEngine: MonoBehaviour
{
    public TextMesh results;
    public string[] keywords = new string[] {"open", "close", "break"};
    private string word = "none";
#if UNITY_STANDALONE_WIN
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    protected PhraseRecognizer recognizer;
#endif

    // Use the Keyword Recognizer to detect the words "open", "close" and "break".
    private void Start()
    {
        if (keywords != null)
        {
#if UNITY_STANDALONE_WIN
            var recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
#endif
        }
    }
    
    // Update is called once per frame and is used to update the gripper's behaviours.
    private void update()
    {
        if (word == "open")
        {
            // Open the gripper?
        }
        else if (word == "close")
        {
            // Close the gripper?
        }
        else if (word == "break")
        {
            // Shut down the robot arm?
        }
    }

#if UNITY_STANDALONE_WIN
    // This function is called for phrase transcribing when the speech recognition engine recognises a word.
    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        results.text = "You said: <b>" + word + "</b>";
    }
#endif

    private void OnApplicationQuit()
    {
#if UNITY_STANDALONE_WIN
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.Stop();
        }
#endif
    }
}