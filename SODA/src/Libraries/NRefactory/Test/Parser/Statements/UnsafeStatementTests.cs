// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 4482 $</version>
// </file>

using System;
using System.IO;
using NUnit.Framework;
using ICSharpCode.NRefactory.Parser;
using ICSharpCode.NRefactory.Ast;

namespace ICSharpCode.NRefactory.Tests.Ast
{
	[TestFixture]
	public class UnsafeStatementTests
	{
		#region C#
		[Test]
		public void CSharpUnsafeStatementTest()
		{
			UnsafeStatement unsafeStatement = ParseUtilCSharp.ParseStatement<UnsafeStatement>("unsafe { }");
			Assert.IsFalse(unsafeStatement.Block.IsNull);
		}
		#endregion
		
		#region VB.NET
			// No VB.NET representation
		#endregion
	}
}
