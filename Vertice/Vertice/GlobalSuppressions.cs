// <copyright file="GlobalSuppressions.cs" company="Thomas Castonguay-Gagnon">
// Copyright (c) Thomas Castonguay-Gagnon. All rights reserved.
// Licensed under the GPL3 license. See LICENSE file in the project root for full license information.
// </copyright>

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:Property summary documentation should match accessors", Justification = "Anoying")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:Documentation text should end with a period", Justification = "Anoying")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Hard to do")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Not needed")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1614:Element parameter documentation should have text", Justification = "Sometimes names are clear enough")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Plenty used in dotnet core injection")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Names are clear enough")]
