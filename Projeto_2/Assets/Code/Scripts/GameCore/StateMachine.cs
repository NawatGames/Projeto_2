using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

// class for a generic StateMachine
public class StateMachine
{
   private IState _currentState;

   // Dicitionary that dictates that this State (Type) have the following possible transitions (List<Transition>).
   // CurrentTransitions are the possible transitions that can happen from current state.
   // AnyTransitions are transitions that can happen anytime, no matter what the current state is. Have priority over CurrentTransitions.
   private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>(); 
   private List<Transition> _currentTransitions = new List<Transition>();
   private List<Transition> _anyTransitions = new List<Transition>();
   
   // Empty list to prevent errors on malformed state machines or state.
   private static List<Transition> _emptyTransitions = new List<Transition>(0);

   // Class Tick function of current state.
   public void Tick()
   {
      // Checks if a transition is happening. If yes, change the current state.
      var transition = GetTransition();
      if (transition != null)
         SetState(transition.NextState);
      
      // Checks if there is a current state or not.
      _currentState?.Tick();
   }

   // Changes current state of the machine.
   public void SetState(IState state)
   {
      // If new state is the same as current, does nothing.
      if (state == _currentState)
         return;
      
      // Applies State clean up, and then changes for new state.
      _currentState?.OnExit();
      _currentState = state;
      
      // If current state have possible transitions, set them on current transitions.
      _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
      if (_currentTransitions == null)
         _currentTransitions = _emptyTransitions;
      
      // Applies State setup.
      _currentState.OnEnter();
   }

   // Adds new transition with defined pathing (has a from and next state well defined) and a condition for transition.
   public void AddTransition(IState previousState, IState nextState, Func<bool> predicate)
   {
      // Checks if previousState already have a list of possible transitions, if not, creates it.
      if (_transitions.TryGetValue(previousState.GetType(), out var transitions) == false)
      {
         transitions = new List<Transition>();
         _transitions[previousState.GetType()] = transitions;
      }
      
      transitions.Add(new Transition(nextState, predicate));
   }

   // Adds transition that can happen whenever it hits a certain condition.
   public void AddAnyTransition(IState state, Func<bool> predicate)
   {
      _anyTransitions.Add(new Transition(state, predicate));
   }

   // Class for a transition with a next state + condition for transition to happen.
   private class Transition
   {
      public Func<bool> Condition {get; }
      public IState NextState { get; }

      public Transition(IState nextState, Func<bool> condition)
      {
         NextState = nextState;
         Condition = condition;
      }
   }

   // Checks if a transition is happening.
   private Transition GetTransition()
   {
      foreach(var transition in _anyTransitions)
         if (transition.Condition())
            return transition;
      
      foreach (var transition in _currentTransitions)
         if (transition.Condition())
            return transition;

      return null;
   }
}