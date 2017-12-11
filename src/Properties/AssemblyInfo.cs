using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Resources;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Bridle")]
[assembly: AssemblyDescription("A game engine focused on ease-of-use")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Saddlebound Studios")]
[assembly: AssemblyProduct("Bridle")]
[assembly: AssemblyCopyright("Copyright (c) 2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Allow the content pipeline assembly to access
// some of our internal helper methods that it needs.
[assembly: InternalsVisibleTo("MonoGame.Framework.Content.Pipeline")]
[assembly: InternalsVisibleTo("MonoGame.Framework.Net")]

// Mark the assembly as CLS compliant so it can be safely used in other .NET languages
[assembly: CLSCompliant(false)]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this
// project is exposed to COM.
[assembly: Guid("81119db2-82a6-45fb-a366-63a08437b485")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("17.12.0.0")]
