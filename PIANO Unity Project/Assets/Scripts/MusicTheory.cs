using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MusicTheory 
{
   public static Note HalfStep(bool up, Note  note, Piano piano)
   {
        if (up)
        {
            return piano.Notes[note.index + 1];
        }
        else
        {
            return piano.Notes[note.index - 1];
        }
   }
}
