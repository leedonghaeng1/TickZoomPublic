// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 4687 $</version>
// </file>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.PythonBinding;
using NUnit.Framework;

namespace PythonBinding.Tests.Designer
{
	/// <summary>
	/// The python form code generator should ignore any design time properties (e.g. Locked) of the form. 
	/// These are put in the .resx file not the source code.
	/// </summary>
	[TestFixture]
	public class IgnoreDesignTimePropertiesTestFixture
	{
		string expectedCode = "    self.SuspendLayout()\r\n" +
							  "    # \r\n" +
							  "    # MainForm\r\n" +
							  "    # \r\n" +
							  "    self.ClientSize = System.Drawing.Size(200, 300)\r\n" +
							  "    self.Name = \"MainForm\"\r\n" +
							  "    self.ResumeLayout(False)\r\n";

		string generatedPythonCode;
		
		/// <summary>
		/// After a form is loaded onto a DesignSurface this checks that the PythonForm does not try to 
		/// add design time properties and does not throw a null reference exception.
		/// </summary>
		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			using (DesignSurface designSurface = new DesignSurface(typeof(Form))) {
				IDesignerHost host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
				Form form = (Form)host.RootComponent;
				form.ClientSize = new Size(200, 300);

				PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(form);
				PropertyDescriptor namePropertyDescriptor = descriptors.Find("Name", false);
				namePropertyDescriptor.SetValue(form, "MainForm");
				
				DesignerSerializationManager serializationManager = new DesignerSerializationManager(host);
				using (serializationManager.CreateSession()) {					
					PythonCodeDomSerializer serializer = new PythonCodeDomSerializer("    ");
					generatedPythonCode = serializer.GenerateInitializeComponentMethodBody(host, serializationManager, String.Empty, 1);
				}
			}
		}
		
		[Test]
		public void DesignTimePropertyIsIgnoredInGeneratedCode()
		{						
			Assert.AreEqual(expectedCode, generatedPythonCode);
		}
	}
}
