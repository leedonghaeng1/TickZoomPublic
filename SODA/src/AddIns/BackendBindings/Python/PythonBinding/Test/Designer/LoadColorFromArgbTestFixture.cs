// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 3897 $</version>
// </file>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using ICSharpCode.PythonBinding;
using NUnit.Framework;
using PythonBinding.Tests.Utils;

namespace PythonBinding.Tests.Designer
{
	[TestFixture]
	public class LoadColorFromArgbTestFixture : LoadFormTestFixtureBase
	{		
		public override string PythonCode {
			get {
				return "class TestForm(System.Windows.Forms.Form):\r\n" +
							"    def InitializeComponent(self):\r\n" +
							"        self.SuspendLayout()\r\n" +
							"        # \r\n" +
							"        # TestForm\r\n" +
							"        # \r\n" +
							"        self.BackColor = System.Drawing.Color.FromArgb(10, 190, 0)\r\n" +
							"        self.Name = \"TestForm\"\r\n" +
							"        self.ResumeLayout(False)\r\n";
			}
		}
								
		[Test]
		public void FormBackColor()
		{
			Assert.AreEqual(Color.FromArgb(10, 190, 0), Form.BackColor);
		}
	}
}
