// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 5343 $</version>
// </file>

using System;
using ICSharpCode.NRefactory;
using ICSharpCode.RubyBinding;
using NUnit.Framework;

namespace RubyBinding.Tests.Converter
{
	/// <summary>
	/// Tests that removing a method from an event handler is converted
	/// from C# to Ruby correctly.
	/// </summary>
	[TestFixture]
	public class RemoveHandlerConversionTestFixture
	{
		string csharp = "class Foo\r\n" +
						"{\r\n" +
						"    public Foo()\r\n" +
						"    {" +
						"        button = new Button();\r\n" +
						"        button.Click -= ButtonClick;\r\n" +
						"    }\r\n" +
						"\r\n" +
						"    void ButtonClick(object sender, EventArgs e)\r\n" +
						"    {\r\n" +
						"    }\r\n" +
						"}";
		
		[Test]
		public void ConvertedRubyCode()
		{
			string expectedCode =
				"class Foo\r\n" +
				"    def initialize()\r\n" +
				"        button = Button.new()\r\n" +
				"        button.Click.remove(OnButtonClick)\r\n" +
				"    end\r\n" +
				"\r\n" +
				"    def ButtonClick(sender, e)\r\n" +
				"    end\r\n" +
				"end";
			
			NRefactoryToRubyConverter converter = new NRefactoryToRubyConverter(SupportedLanguage.CSharp);
			converter.IndentString = "    ";
			string code = converter.Convert(csharp);
			
			Assert.AreEqual(expectedCode, code);
		}	
	}
}
