// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 4687 $</version>
// </file>

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.PythonBinding;
using NUnit.Framework;
using PythonBinding.Tests.Utils;

namespace PythonBinding.Tests.Designer
{
	class ProtectedPanelBaseForm : Form
	{
		protected Panel panel1 = new Panel();
		Button button1 = new Button();
		
		public ProtectedPanelBaseForm()
		{
			button1.Name = "button1";

			panel1.Name = "panel1";
			panel1.Location = new Point(5, 10);
			panel1.Size = new Size(200, 100);
			panel1.Controls.Add(button1);

			Controls.Add(panel1);
		}
	}
	
	class ProtectedPanelDerivedForm : ProtectedPanelBaseForm 
	{
		[EditorBrowsableAttribute(EditorBrowsableState.Never)]
		internal Point PanelLocation {
			get { return panel1.Location; }
			set { panel1.Location = value; }
		}
		
		[EditorBrowsableAttribute(EditorBrowsableState.Never)]
		internal Size PanelSize {
			get { return panel1.Size; }
			set { panel1.Size = value; }
		}
		
		[EditorBrowsableAttribute(EditorBrowsableState.Never)]
		internal Panel GetPanel() 
		{
			return panel1;
		}
	}
	
	/// <summary>
	/// Tests that no code is generated for a protected panel control in the base class
	/// that has child controls.
	/// </summary>
	[TestFixture]
	public class GenerateInheritedProtectedPanelFormTestFixture
	{
		string generatedPythonCode;
		
		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			using (DesignSurface designSurface = new DesignSurface(typeof(ProtectedPanelDerivedForm))) {
				IDesignerHost host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
				Form form = (Form)host.RootComponent;
				form.ClientSize = new Size(284, 264);

				PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(form);
				PropertyDescriptor namePropertyDescriptor = descriptors.Find("Name", false);
				namePropertyDescriptor.SetValue(form, "MainForm");

				// Move protected panel so we generate code for the new location.
				ProtectedPanelDerivedForm derivedForm = (ProtectedPanelDerivedForm)form;
				derivedForm.PanelLocation = new Point(10, 15);
				
				DesignerSerializationManager serializationManager = new DesignerSerializationManager(host);
				using (serializationManager.CreateSession()) {					
					PythonCodeDomSerializer serializer = new PythonCodeDomSerializer("    ");
					generatedPythonCode = serializer.GenerateInitializeComponentMethodBody(host, serializationManager, String.Empty, 1);
				}
			}
		}
		
		[Test]
		public void GeneratedCode()
		{
			string expectedCode = "    self.SuspendLayout()\r\n" +
								"    # \r\n" +
								"    # panel1\r\n" +
								"    # \r\n" +
								"    self.panel1.Location = System.Drawing.Point(10, 15)\r\n" +
								"    # \r\n" +
								"    # MainForm\r\n" +
								"    # \r\n" +
								"    self.ClientSize = System.Drawing.Size(284, 264)\r\n" +
								"    self.Name = \"MainForm\"\r\n" +
								"    self.ResumeLayout(False)\r\n";
			
			Assert.AreEqual(expectedCode, generatedPythonCode, generatedPythonCode);
		}
	}
}
