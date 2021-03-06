// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 4358 $</version>
// </file>

using System;
using System.Collections.Generic;
using System.Resources;

namespace PythonBinding.Tests.Utils
{
	public class MockResourceWriter : IResourceWriter
	{
		bool disposed;
		Dictionary<string, object> resources = new Dictionary<string, object>();
		
		public MockResourceWriter()
		{
		}
		
		public object GetResource(string name)
		{
			if (resources.ContainsKey(name)) {
				return resources[name];
			}
			return null;
		}
		
		public void AddResource(string name, string value)
		{
			resources.Add(name, value);
		}
		
		public void AddResource(string name, object value)
		{
			resources.Add(name, value);
		}
		
		public void AddResource(string name, byte[] value)
		{
			throw new NotImplementedException();
		}
		
		public void Close()
		{
		}
		
		public void Generate()
		{
		}
		
		public void Dispose()
		{
			disposed = true;
		}
		
		public bool IsDisposed {
			get { return disposed; }
		}
	}
}
