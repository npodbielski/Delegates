// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegatesAssemblyInfo.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
#if NETCOREAPP1_0 || NETCOREAPP2_0 || STANDARD
#define No_InternalsVisibleTo
#endif

using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Delegates")]
[assembly: AssemblyProduct("Delegates")]
[assembly: InternalsVisibleTo("DelegatesTestNET35")]
[assembly: InternalsVisibleTo("DelegatesTestNET4")]
[assembly: InternalsVisibleTo("DelegatesTestNET45")]
[assembly: InternalsVisibleTo("DelegatesTestNET46")]
[assembly: InternalsVisibleTo("DelegatesTestNETCore10")]
[assembly: InternalsVisibleTo("DelegatesTestNETCore20")]
[assembly: InternalsVisibleTo("DelegatesTestNETPortable")]