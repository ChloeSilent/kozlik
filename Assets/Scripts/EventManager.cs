using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;


// This script is taken from https://unity3d.com/ru/learn/tutorials/topics/scripting/events-creating-simple-messaging-system
// It implements messaging system for decoupling components.

public class EventManager : MonoBehaviour
{
	public class CustomEventWithItemButtonParameter : UnityEvent<ItemButton>
	{
	}

	private Dictionary<string, CustomEventWithItemButtonParameter> eventDictionary;

	private static EventManager eventManager;

	public static EventManager Instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

				if (!eventManager)
				{
					Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init();
				}
			}
			return eventManager;
		}
	}

	void Init()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, CustomEventWithItemButtonParameter>();
		}
	}

	public static void StartListening(string eventName, UnityAction<ItemButton> listener)
	{
		CustomEventWithItemButtonParameter thisEvent = null;
		// Pass by reference copies an argument's memory address into the formal parameter. 
		// Inside the method, the address is used to access the actual argument used in the call. 
		// This means that changes made to the parameter affect the argument.
		// Output parameters are similar to reference parameters, except that they transfer data out of the method rather than accept data in.
		// They are defined using the out keyword.
		// The variable supplied for the output parameter need not be initialized since that value will not be used.
		// Output parameters are particularly useful when you need to return multiple values from a method.

		// TryGetValue https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trygetvalue?view=netframework-4.7.2
		// Gets the value associated with the specified key.

		// public bool TryGetValue(TKey key, out TValue value);

		// key TKey
		// The key of the value to get.

		// value TValue
		// When this method returns, contains the value associated with the specified key, if the key is found;
		// otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.

		// Returns Boolean
		// true if the Dictionary<TKey,TValue> contains an element with the specified key; otherwise, false.

		// So here we will check if there is an event with eventName in the dictionary

		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		// If there is already an event with such name, we just add listener to original event.
		// It is possible due to using out keyword
		{
			thisEvent.AddListener(listener);
		}
		else
		// ..and if there is no such event in dictionary, we`ll create one, also add listener, just as in true case,
		// and finally put this new event to Dictionary.
		{
			thisEvent = new CustomEventWithItemButtonParameter();
			thisEvent.AddListener(listener);
			Instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	// We must take care of memory leaks, and unsubscribe. 
	public static void StopListening(string eventName, UnityAction<ItemButton> listener)
	{
		//  If there is no eventManager in scene, we have nothing to worry about, and just leave method.
		if (eventManager == null)
		{
			return;
		}

		CustomEventWithItemButtonParameter thisEvent = null;
		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	public static void TriggerEvent(string eventName,ItemButton itemButton)
	{
		CustomEventWithItemButtonParameter thisEvent = null;
		if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke(itemButton);
		}
	}
}