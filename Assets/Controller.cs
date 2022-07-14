using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject inputField;
    [SerializeField] Button addButton;
    [SerializeField] Button continueButton;

    [SerializeField] TMP_InputField number;

    [SerializeField] GameObject[] listItemResults;

    [SerializeField] TextMeshProUGUI result;
    [SerializeField] TextMeshProUGUI resultPrime;

    [SerializeField] GameObject listFieldParent;

    [SerializeField] Button calButton;

    private List<GameObject> list = new List<GameObject>();
    private List<double> listInt = new List<double>();
    private int amount = 0;

    public string CURRENCY_FORMAT = "#,##0.00";
    public NumberFormatInfo NFI = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };

    private int type = 1;

    [SerializeField] Color[] listColor;

    //Singleton
    public static Controller Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        Clear();
    }

    private void DropdownitemSelected()
    {
        
    }

    private void DropdownitemAgeSelected()
    {
        
    }


    public void OnValueChanged()
    {
        if(CheckValidate())
        {
            calButton.interactable = true;
        }
        else
        {
            calButton.interactable= false;
        }
    }

    private bool CheckValidate()
    {
        if (number.text == "")
        {
            return false;
        }

        int root_m = int.Parse(number.text);
        if (root_m < 0) return false;

        //return text.All(char.IsDigit);
        return true;
    }


    public void Sum()
    {
        CalWithAdult();
        listFieldParent.SetActive(true);
    }

    private void CalWithAdult()
    {
        int root_m = int.Parse(number.text);

        // find factors
        List<int> listFactors = new List<int>();
        for(int i = 1; i < root_m; i++)
        {
            if(root_m%i == 0)
            {
                listFactors.Add(i);
            }
        }

        result.text = "";
        for (int i = 0; i<listFactors.Count; i++)
        {
            result.text += listFactors[i];

            if(i != listFactors.Count-1)
            {
                result.text += ", ";
            }
        }

        // Find prime
        List<int> listPrime = new List<int>();
        foreach(var item in listFactors)
        {
            if(CalcIsPrime(item))
            {
                listPrime.Add(item);
            }
        }

        int temp = root_m;
        List<int> primeFactors = new List<int>();
        for (int i = 0; i < listPrime.Count; i++)
        {
            while (temp % listPrime[i] == 0)
            {
                temp = temp / listPrime[i];
                primeFactors.Add(listPrime[i]);
            }
        }

        resultPrime.text = root_m + "=";
        for (int i = 0; i < primeFactors.Count; i++)
        {
            resultPrime.text += primeFactors[i];

            if (i != primeFactors.Count - 1)
            {
                resultPrime.text += "x";
            }
        }
    }

    public bool CalcIsPrime(int number)
    {

        if (number == 1) return false;
        if (number == 2) return true;

        if (number % 2 == 0) return false; // Even number     

        for (int i = 2; i < number; i++)
        { // Advance from two to include correct calculation for '4'
            if (number % i == 0) return false;
        }

        return true;

    }

    public void Continue()
    {
        if(amount==0) return;
        Clear();
    }

    public void Clear()
    {
        listFieldParent.SetActive(false);

        number.text = "";
        result.text = "";
        resultPrime.text = "";

        calButton.interactable = false;
    }

    public void Quit()
    {
        Clear();
        Application.Quit();
    }
}
