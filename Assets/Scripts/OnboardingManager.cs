using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnboardingManager : MonoBehaviour
{
    public bool sceneOne;

    public bool leftThumbstickMove, rightThumbstickTurn, selectBone, moveBone, returnBoneToOrigin, deselectBone, attachBone, answerQuiz;

    public bool leftThumbstickMoveDone, rightThumbstickTurnDone, selectBoneDone, moveBoneDone, returnBoneToOriginDone, deselectBoneDone, attachBoneDone, answerQuizDone;

    public Animator leftThumbstickMoveAni, rightThumbstickTurnAni, selectBoneAni,  returnBoneToOriginAni, deselectBoneAni, attachBoneAni, answerQuizAni;

    public Animator fadeOutCanvas;

    public bool canvasHasFaded;

    public int completeCounter = 0;


    public void UpdateChecklist()
    {
        if (sceneOne)
        {
            if (completeCounter == 7)
            {
                return;
            }

            else
            {
                if (leftThumbstickMove && !leftThumbstickMoveDone)
                {
                    leftThumbstickMoveAni.Play("Complete");
                    leftThumbstickMoveDone = true;
                    completeCounter++;
                }

                if (rightThumbstickTurn && !rightThumbstickTurnDone)
                {
                    rightThumbstickTurnAni.Play("Complete");
                    rightThumbstickTurnDone = true;
                    completeCounter++;
                }

                if (selectBone && !selectBoneDone)
                {
                    selectBoneAni.Play("Complete");
                    selectBoneDone = true;
                    completeCounter++;
                }

                //if (moveBone && !moveBoneDone)
                //{
                //    moveBoneAni.Play("Complete");
                //    moveBoneDone = true;
                //    completeCounter++;
                //}

                if (returnBoneToOrigin && !returnBoneToOriginDone)
                {
                    returnBoneToOriginAni.Play("Complete");
                    returnBoneToOriginDone = true;
                    completeCounter++;
                }

                if (deselectBone && !deselectBoneDone)
                {
                    deselectBoneAni.Play("Complete");
                    deselectBoneDone = true;
                    completeCounter++;
                }

                if (attachBone && !attachBoneDone)
                {
                    attachBoneAni.Play("Complete");
                    attachBoneDone = true;
                    completeCounter++;
                }

                if (answerQuiz && !answerQuizDone)
                {
                    answerQuizAni.Play("Complete");
                    answerQuizDone = true;
                    completeCounter++;
                }

                if (completeCounter == 7 && !canvasHasFaded)
                {
                    fadeOutCanvas.Play("fadeOutCanvas");
                }
            }
        }
        else
        {

        }


    }
}
