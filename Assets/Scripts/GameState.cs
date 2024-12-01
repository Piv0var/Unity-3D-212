using System;
using System.Collections.Generic;
public class GameState
{
    public static bool isDay { get; set; }
    public static bool isFpv { get; set; }

    #region effectsVolume
    public static float _effectsVolume { get; set; } = 1f;
    public static float effectsVolume
    {
        get => _effectsVolume;
        set
        {
            if (_effectsVolume != value)
            {
                _effectsVolume = value;
                NotifyListeners(nameof(effectsVolume));
            }
        }
    }
    #endregion

    #region ambientVolume
    public static float _ambientVolume { get; set; } = 1f;
    public static float ambientVolume
    {
        get => _ambientVolume;
        set
        {
            if (_ambientVolume != value)
            {
                _ambientVolume = value;
                NotifyListeners(nameof(ambientVolume));
            }
        }
    }
    #endregion

    #region isSoundsMuted (Mute All)
    public static bool _isSoundsMuted = false;
    public static bool isSoundsMuted
    {
        get => _isSoundsMuted;
        set
        {
            if (_isSoundsMuted != value)
            {
                _isSoundsMuted = value;
                NotifyListeners(nameof(isSoundsMuted));
            }
        }
    }
    #endregion

    #region Change Notifier
    private static Dictionary<String, List<Action<string>>> chaangeListeners = new();
    public static void AddChangeListener(Action<string> listener, String name)
    {
        if(!chaangeListeners.ContainsKey(name))
        {
            chaangeListeners[name] = new List<Action<string>>();
        }
        chaangeListeners[name].Add(listener);
        listener(name);
    }

    public static void RemoveChangeListener(Action<string> listener, String name)
    {
        if (chaangeListeners.ContainsKey(name))
        {
            chaangeListeners[name].Remove(listener);

        }
    }
    private static void NotifyListeners(String name)
    {
        if (chaangeListeners.ContainsKey(name))
        {
            foreach(var action in chaangeListeners[name])
            {
                action(name);
            }

        }
    }
    #endregion

    #region collectSubscribers
    private static List<Action<String>> collectSubscribers = new List<Action<String>>();
    public static void AddCollectListener(Action<String> subscriber)
    {
        collectSubscribers.Add(subscriber);
    }
    public static void RemoveCollectListener(Action<String> subscriber) 
    {
        collectSubscribers.Remove(subscriber);
    }
    public static void Collect(String itemName)
    {
        collectSubscribers.ForEach(s => s(itemName));
    }
    #endregion

}
