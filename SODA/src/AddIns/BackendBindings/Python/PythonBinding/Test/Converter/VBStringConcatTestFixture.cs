// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 4063 $</version>
// </file>

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using ICSharpCode.NRefactory;
using ICSharpCode.PythonBinding;
using NUnit.Framework;

namespace PythonBinding.Tests.Converter
{
	[TestFixture]
	public class VBStringConcatTestFixture
	{
		string vb = "Namespace DefaultNamespace\r\n" +
					"\tPublic Class Class1\r\n" +
					"\t\tPublic Sub Test\r\n" +
					"\t\t\tDim a as String\r\n" +
					"\t\t\ta = \"test\" & \" this\"\r\n" +
					"\t\tEnd Sub\r\n" +
					"\tEnd Class\r\n" +
					"End Namespace";

		[Test]
		public void GeneratedPythonSourceCode()
		{
			NRefactoryToPythonConverter converter = new NRefactoryToPythonConverter(SupportedLanguage.VBNet);
			string python = converter.Convert(vb);
			string expectedPython = "class Class1(object):\r\n" +
									"\tdef Test(self):\r\n" +
									"\t\ta = \"test\" + \" this\"";
			
			Assert.AreEqual(expectedPython, python);
		}
	}
}
