﻿using System;
using System.Collections.Generic;

namespace Liber.Forms;

internal sealed class ReverseDateTimeComparer : Comparer<DateTime>
{
    private static ReverseDateTimeComparer? s_instance;

    public static new ReverseDateTimeComparer Default
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new ReverseDateTimeComparer();
            }

            return s_instance;
        }
    }

    public override int Compare(DateTime x, DateTime y)
    {
        return y.CompareTo(x);
    }
}
