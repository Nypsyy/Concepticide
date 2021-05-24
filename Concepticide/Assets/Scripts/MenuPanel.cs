using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public delegate void SelectionHandler(string optionName);
    public GameObject menuChoice;

    // Will be called on selection validation.
    public SelectionHandler selectionHandler = (optionName) => {};

    private List<(string name, GameObject obj, Text text)> _options = new List<(string name, GameObject obj, Text text)>();
    private int _selectedPos = 0;


    void Start()
    {
        menuChoice.SetActive(false);
        menuChoice.transform.SetParent(null, false);

    }


    // Update is called once per frame
    void Update()
    {
        // TODO: better input system

        if (Input.GetKeyDown("up"))
            _UpdateSelectedPos(_selectedPos - 1);
        if (Input.GetKeyDown("down"))
            _UpdateSelectedPos(_selectedPos + 1);
        
        if (Input.GetKeyDown(KeyCode.Return))
            selectionHandler(_options[_selectedPos].name);

    }


    public void SetOptions(string[] optionNames, int selectedPos=0) {
        // Deleting existing options
        foreach (var (_,option,_) in _options) {
            GameObject.Destroy(option);
        }
        _options.Clear();
        
        // Pushing new options
        foreach (var optionName in optionNames) {
            var option = Instantiate(menuChoice, transform);
            option.SetActive(true);
            var text = option.GetComponent<Text>();
            text.text = optionName;
            _options.Add((optionName, option, text));
        }

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80 * _options.Count);
        _selectedPos = 0;
        _UpdateSelectedPos(selectedPos);
    }

    private void _UpdateSelectedPos(int newPos) {
        if (newPos < 0)
            newPos += _options.Count;
        else if (newPos >= _options.Count)
            newPos -= _options.Count;

        _options[_selectedPos].text.fontStyle = FontStyle.Normal;
        _selectedPos = newPos;
        _options[_selectedPos].text.fontStyle = FontStyle.Bold;
    }

    void RemoveOptions()
    {
    }

}
