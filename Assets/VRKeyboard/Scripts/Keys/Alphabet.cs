using UnityEngine;
//using UnityEngine.UI;
using TMPro;


namespace VRKeyboard.Utils
{
    public class Alphabet : Key
    {
        //public TMP_Text key;

        public override void CapsLock(bool isUppercase)
        {
            if (isUppercase)
            {
                key.text = key.text.ToUpper();
            }
            else
            {
                key.text = key.text.ToLower();
            }
        }
    }
}