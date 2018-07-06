using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB.QOL
{
    public class CaptchaSolve : MonoBehaviour
    {
        public static void SolveFor(int id)
        {
            SolveAfter(id);
            GSOB.TrySendDebug("Solved captcha");
        }

        static IEnumerator SolveAfter(int id)
        {
            yield return new WaitForSeconds(5);
            Scr_RPCSender.instance.AnswerMacroCheck(id);
        }
    }
}
