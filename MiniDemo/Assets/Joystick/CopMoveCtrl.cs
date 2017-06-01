using UnityEngine;
using System.Collections;
using System;

public class CopMoveCtrl : MonoBehaviour {

    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private RectTransform m_joystick;
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private float m_rotateSpeed = 0.5f;

    private enum ActionState { IDLE, WALK, JOG, RUN }
    private ActionState m_nowState = ActionState.IDLE;
    private float m_horOffset;
    private float m_verOffset;
    private float m_distanceSum;
    private float m_rotateAngle;
    private float m_timepiece;
    float dir = 0;

    void Start() {

    }

    void Update() {
        PlayerMove();
    }

    private void PlayerMove() {
        if (m_joystick.anchoredPosition.x > 0.1 || m_joystick.anchoredPosition.x < -0.1) {
            m_horOffset = m_joystick.anchoredPosition.x;
        } else {
            m_horOffset = 0;
        }
        if (m_joystick.anchoredPosition.y > 0.1 || m_joystick.anchoredPosition.y < -0.1) {
            m_verOffset = m_joystick.anchoredPosition.y;
        } else {
            m_verOffset = 0;
        }
        m_distanceSum = Mathf.Abs(m_horOffset) + Mathf.Abs(m_verOffset);
        m_rotateAngle = Mathf.Atan2(m_horOffset, m_verOffset) * Mathf.Rad2Deg;
        if (m_distanceSum == 0) {
            m_nowState = ActionState.IDLE;
        } else if (m_distanceSum < 30) {
            m_nowState = ActionState.WALK;
        } else if (m_distanceSum < 90) {
            m_nowState = ActionState.JOG;
        } else if (m_distanceSum >= 90) {
            m_nowState = ActionState.RUN;
        }
        PlayerActionStateSwitch(m_nowState);
        //m_player.transform.localPosition += m_velocity * Time.fixedDeltaTime;
        //float angle = Mathf.Atan2(m_horOffset, m_verOffset) * Mathf.Rad2Deg;
        //Animator.GetCurrentAnimationClipState
        if (m_distanceSum != 0) {
            m_rotateAngle = Mathf.Atan2(m_horOffset, m_verOffset) * Mathf.Rad2Deg;
            m_player.transform.rotation = Quaternion.Slerp(m_player.transform.rotation, Quaternion.Euler(0, m_rotateAngle, 0), m_rotateSpeed);
        }
    }

    private void PlayerActionStateSwitch(ActionState state) {
        switch (state) {
            case ActionState.IDLE:
                m_animator.SetBool("Idle", true);
                m_animator.SetBool("Walk", false);
                m_animator.SetBool("Jog", false);
                m_animator.SetBool("Run", false);
                break;
            case ActionState.WALK:
                m_animator.SetBool("Idle", false);
                m_animator.SetBool("Walk", true);
                m_animator.SetBool("Jog", false);
                m_animator.SetBool("Run", false);
                break;
            case ActionState.JOG:
                m_animator.SetBool("Idle", false);
                m_animator.SetBool("Walk", false);
                m_animator.SetBool("Jog", true);
                m_animator.SetBool("Run", false);
                break;
            case ActionState.RUN:
                m_animator.SetBool("Idle", false);
                m_animator.SetBool("Walk", false);
                m_animator.SetBool("Jog", false);
                m_animator.SetBool("Run", true);
                break;
            default:
                break;
        }
    }

}
