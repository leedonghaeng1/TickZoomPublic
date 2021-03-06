// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 4574 $</version>
// </file>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using ICSharpCode.PythonBinding;
using IronPython.Compiler.Ast;
using NUnit.Framework;
using PythonBinding.Tests.Utils;

namespace PythonBinding.Tests.Designer
{
	class PublicPanelBaseForm : Form
	{
		public Panel panel1 = new Panel();
		Button button1 = new Button();
		
		public PublicPanelBaseForm()
		{
			button1.Name = "button1";

			panel1.Name = "panel1";
			panel1.Location = new Point(5, 10);
			panel1.Size = new Size(200, 100);
			panel1.Controls.Add(button1);

			Controls.Add(panel1);
		}
	}
	
	class PublicPanelDerivedForm : PublicPanelBaseForm 
	{
	}
	
	[TestFixture]
	public class LoadInheritedPublicPanelFormTestFixture : LoadFormTestFixtureBase
	{		
		public override string PythonCode {
			get {
				base.ComponentCreator.AddType("PythonBinding.Tests.Designer.PublicPanelDerivedForm", typeof(PublicPanelDerivedForm));
				
				return "class MainForm(PythonBinding.Tests.Designer.PublicPanelDerivedForm):\r\n" +
							"    def InitializeComponent(self):\r\n" +
							"        self.SuspendLayout()\r\n" +
							"        # \r\n" +
							"        # panel1\r\n" +
							"        # \r\n" +
							"        self.panel1.Location = System.Drawing.Point(10, 15)\r\n" +
							"        self.panel1.Size = System.Drawing.Size(108, 120)\r\n" +
							"        # \r\n" +
							"        # form1\r\n" +
							"        # \r\n" +
							"        self.Location = System.Drawing.Point(10, 20)\r\n" +
							"        self.Name = \"form1\"\r\n" +
							"        self.Controls.Add(self._textBox1)\r\n" +
							"        self.ResumeLayout(False)\r\n";
			}
		}
		
		PublicPanelDerivedForm DerivedForm { 
			get { return Form as PublicPanelDerivedForm; }
		}
				
		[Test]
		public void PanelLocation()
		{
			Assert.AreEqual(new Point(10, 15), DerivedForm.panel1.Location);
		}

		[Test]
		public void PanelSize()
		{
			Assert.AreEqual(new Size(108, 120), DerivedForm.panel1.Size);
		}
		
		[Test]
		public void GetPublicPanelObject()
		{
			Assert.AreEqual(DerivedForm.panel1, PythonControlFieldExpression.GetInheritedObject("panel1", DerivedForm));
		}		
	}
}
