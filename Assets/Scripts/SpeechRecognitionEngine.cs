using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class SpeechRecognitionEngine: MonoBehaviour
{
    public TextMesh results;
    public string[] keywords = new string[] {"open", "close", "break"};
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    private string word = "none";

    protected PhraseRecognizer recognizer;
    
    // Use the Keyword Recognizer to detect the words "open", "close" and "break".
    private void Start()
    {
        if (keywords != null)
        {
            var recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
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

    // This function is called for phrase transcribing when the speech recognition engine recognises a word.
    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        results.text = "You said: <b>" + word + "</b>";
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.Stop();
        }
    }
}