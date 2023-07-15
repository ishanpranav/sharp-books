﻿using CsvHelper.Configuration.Attributes;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Liber;

[MessagePackObject]
[XmlRoot("account")]
public class Account : IXmlSerializable
{
    internal readonly HashSet<Account> children = new HashSet<Account>();
    internal readonly HashSet<Line> lines = new HashSet<Line>();

    public Account() { }

    [JsonConstructor]
    [SerializationConstructor]
    public Account(Guid parentKey)
    {
        ParentKey = parentKey;
    }

    [Ignore]
    [Key(0)]
    public Guid ParentKey { get; internal set; }

    [Default(0)]
    [Index(3)]
    [Key(2)]
    [Name("Account Code")]
    [Optional]
    public decimal Number { get; set; }

    [Index(2)]
    [Key(1)]
    [Name("Account Name")]
    [Optional]
    public string Name { get; set; } = string.Empty;

    [Index(0)]
    [Key(3)]
    [Name("Type")]
    [Optional]
    public AccountType Type { get; set; }

    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(11)]
    [Key(4)]
    [Name("Placeholder")]
    [Optional]
    public bool Placeholder { get; set; }

    [Index(4)]
    [Key(5)]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? Description { get; set; }

    [Index(6)]
    [Key(6)]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? Notes { get; set; }

    [Index(5)]
    [Browsable(false)]
    [Key(7)]
    [MessagePackFormatter(typeof(MessagePackColorFormatter))]
    [Name("Account Color")]
    [Optional]
    [CsvHelper.Configuration.Attributes.TypeConverter(typeof(CsvHelper.TypeConversion.ColorConverter))]
    public Color Color { get; set; }

    [Index(10)]
    [Key(8)]
    [Name("Tax Info")]
    [Optional]
    public TaxType TaxType { get; set; }

    [Ignore]
    [IgnoreMember]
    [JsonIgnore]
    public decimal Balance
    {
        get
        {
            decimal result = 0;

            foreach (Line line in lines)
            {
                result += line.Balance;
            }

            return result;
        }
    }

    [Ignore]
    [IgnoreMember]
    [JsonIgnore]
    public decimal Debit
    {
        get
        {
            if (Balance < 0)
            {
                return 0;
            }

            return Balance;
        }
    }

    [Ignore]
    [IgnoreMember]
    [JsonIgnore]
    public decimal Credit
    {
        get
        {
            if (Balance > 0)
            {
                return 0;
            }

            return -Balance;
        }
    }

    [IgnoreMember]
    [JsonIgnore]
    public IReadOnlyCollection<Account> Children
    {
        get
        {
            return children;
        }
    }

    [IgnoreMember]
    [JsonIgnore]
    public IReadOnlyCollection<Line> Lines
    {
        get
        {
            return lines;
        }
    }

    public override string ToString()
    {
        return $"{Number} - {Name}";
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
        writer.WriteElementString("name", Name);
        writer.WriteElementString("debit", XmlConvert.ToString(Debit));
        writer.WriteElementString("credit", XmlConvert.ToString(Credit));
    }
}
