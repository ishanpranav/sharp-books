﻿// Transaction.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CsvHelper.Configuration.Attributes;
using MessagePack;

namespace Liber;

[MessagePackObject]
[XmlRoot("transaction")]
public class Transaction :
    IComparable,
    IComparable<Transaction>,
    IEquatable<Transaction>,
    IXmlSerializable
{
    public Transaction()
    {
        Lines = new List<Line>();
    }

    [JsonConstructor]
    [SerializationConstructor]
    public Transaction(ICollection<Line> lines)
    {
        Lines = lines;

        foreach (Line line in lines)
        {
            line.Transaction = this;
        }
    }

    [Ignore]
    [Key(0)]
    public ICollection<Line> Lines { get; }

    [Ignore]
    [IgnoreMember]
    [JsonIgnore]
    public IOrderedEnumerable<Line> OrderedLines
    {
        get
        {
            return Lines
                .OrderBy(x => x.Debit > 0 ? -1 : 1)
                .ThenByDescending(x => Math.Abs(x.Balance));
        }
    }

    [Index(1)]
    [Key(1)]
    [Name("Transaction ID")]
    public Guid Id { get; set; }

    [Format("M/d/yyyy")]
    [Index(0)]
    [Key(2)]
    [Name("Date")]
    public DateTime Posted { get; set; }

    [Default(0)]
    [Index(2)]
    [Key(3)]
    [Name("Number")]
    [Optional]
    public decimal Number { get; set; }

    [Index(3)]
    [Key(4)]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? Name { get; set; }

    [Index(4)]
    [Key(5)]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? Memo { get; set; }

    [Ignore]
    [IgnoreMember]
    [JsonIgnore]
    public decimal Balance
    {
        get
        {
            decimal result = 0;

            foreach (Line line in Lines)
            {
                result += line.Balance;
            }

            return result;
        }
    }

    public Line? GetDoubleEntry(Line value)
    {
        if (Lines.Count > 2)
        {
            return null;
        }

        foreach (Line line in Lines)
        {
            if (line != value)
            {
                return line;
            }
        }

        return null;
    }

    int IComparable.CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Transaction transaction)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(transaction);
    }

    public int CompareTo(Transaction? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result = Posted.CompareTo(other.Posted);

        if (result != 0)
        {
            return result;
        }

        result = Number.CompareTo(other.Number);

        if (result != 0)
        {
            return result;
        }

        return Id.CompareTo(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        return obj is Transaction other && Equals(other);
    }

    public bool Equals(Transaction? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteElementString("posted", XmlConvert.ToString(Posted, XmlDateTimeSerializationMode.Utc));

        if (Number == 0)
        {
            writer.WriteElementString("number", value: null);
        }
        else
        {
            writer.WriteElementString("number", XmlConvert.ToString(Number));
        }

        writer.WriteElementString("name", Name);

        IOrderedEnumerable<Line> lines = OrderedLines;

        if (writer is XmlReportWriter reportWriter)
        {
            lines = lines.ThenBy(x => reportWriter.Report.Company!.Accounts[x.AccountId].Number);
        }

        foreach (Line line in lines)
        {
            XmlSerializers.Line.Serialize(writer, line);
        }
    }

    public static bool operator ==(Transaction? left, Transaction? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Transaction? left, Transaction? right)
    {
        return !(left == right);
    }

    public static bool operator <(Transaction? left, Transaction? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Transaction? left, Transaction? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Transaction? left, Transaction? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Transaction? left, Transaction? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
