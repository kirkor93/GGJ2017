using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LamePriorityQueue<T>
{
    // The items and priorities.
    List<T> Values = new List<T>();
    List<float> Priorities = new List<float>();

    // Return the number of items in the queue.
    public int NumItems
    {
        get
        {
            return Values.Count;
        }
    }

    // Add an item to the queue.
    public void Enqueue(T new_value, float new_priority)
    {
        Values.Add(new_value);
        Priorities.Add(new_priority);
    }

    // Remove the item with the largest priority from the queue.
    public void Dequeue(out T top_value, out float top_priority)
    {
        // Find the hightest priority.
        int best_index = 0;
        float best_priority = Priorities[0];
        for (int i = 1; i < Priorities.Count; i++)
        {
            if (Priorities[i] < best_priority)
            {
                best_priority = Priorities[i];
                best_index = i;
            }
        }

        // Return the corresponding item.
        top_value = Values[best_index];
        top_priority = best_priority;

        // Remove the item from the lists.
        Values.RemoveAt(best_index);
        Priorities.RemoveAt(best_index);
    }
}
