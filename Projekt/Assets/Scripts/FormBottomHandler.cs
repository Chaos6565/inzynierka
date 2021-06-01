using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormBottomHandler : MonoBehaviour
{
    [SerializeField] private Toggle checkYes;
    [SerializeField] private Toggle checkNo;
    [SerializeField] private Text nameFromField;
    [SerializeField] private Text dropDownMenu;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject resultWindowPass;
    [SerializeField] private GameObject resultWindowFailed;

    private bool check;
    private string name;
    private string option;

    // Update is called once per frame
    void Update()
    {
        if (checkYes.GetComponent<Toggle>().isOn == true && checkNo.GetComponent<Toggle>().isOn == false)
        {
            check = true;
        }
        else
        {
            check = false;
        }

        name = nameFromField.text;

        option = dropDownMenu.text;
        
    }

    public void Click()
    {
        if (check == true && name != "" && option == "Opcja trzecia - ta do wygrania")
        {
            background.SetActive(true);
            resultWindowPass.SetActive(true);
        }
        else
        {
            background.SetActive(true);
            resultWindowFailed.SetActive(true);
        }
    }
}
