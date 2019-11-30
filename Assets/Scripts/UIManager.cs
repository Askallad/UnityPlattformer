using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager currentUIManager;
    public TextMeshProUGUI numberBlocks;

    private void Awake()
    {
        if (currentUIManager != null&& currentUIManager != this)
        {
            Destroy(gameObject);
            return;
        }

        currentUIManager = this;
        DontDestroyOnLoad(gameObject);
    }

   public void updateBlockNumber(int number)
    {
        if (currentUIManager == null)
        {
            return;
        }
        currentUIManager.numberBlocks.text = number.ToString();
    }
}
