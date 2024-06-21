using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [System.Serializable]
    public class State
    {
        public Color fillColor;
        public Color outlineColor;
    }

    private TextMeshProUGUI text;
    private Image fill;
    private Outline outline;

    // Private backing field for letter
    private char _letter;

    // Public property for accessing the letter
    public char letter
    {
        get { return char.ToLower(_letter); }  // Always return lowercase
        private set { _letter = value; }       // Set normally
    }
    public State state { get; private set; }

    private void Awake()
    {
        Debug.Log("Tile Awake");
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetLetter(char letter)
    {   
        this.letter = letter;
        text.text = letter.ToString();
    }

    public void SetState(State state)
    {
        this.state = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor;

    }

}
