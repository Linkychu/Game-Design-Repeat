using System;
using System.Collections;
using System.Collections.Generic;
using Data.AI;
using UnityEngine;




namespace AIStateManager
{
    public class StateManager : MonoBehaviour
    {
        public State currentState;

        private void Start()
        {
            foreach (Transform obj in transform)
            {
                obj.gameObject.tag = transform.tag;
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (gameObject.CompareTag("Ally"))
            {
                Debug.Log($"Name: {gameObject.name}, State:  {currentState} ");
            }

            RunStateMachine();
        }

        public void RunStateMachine()
        {
            State nextState = currentState?.RunCurrentState();

            if (nextState != null)
            {
                SwitchToTheNextState(nextState);
            }
        }

        private void SwitchToTheNextState(State nextState)
        {
            currentState = nextState;
           
        }
    }
}
