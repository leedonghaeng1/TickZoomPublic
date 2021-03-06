// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 3561 $</version>
// </file>

using System;
using System.Threading;
using System.Windows.Forms;

using ICSharpCode.PythonBinding;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using IronPython.Hosting;
using IronPython.Runtime;
using NUnit.Framework;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Hosting.Shell;

namespace PythonBinding.Tests.Console
{
	/// <summary>
	/// Ensures that both lines of text can be read from the python console if they are written
	/// before ReadLine is called.
	/// </summary>
	[TestFixture]
	public class TwoPythonConsoleLinesWaitingTestFixture
	{
		string line1;
		string line2;
		PythonConsole pythonConsole;
		bool lineAvailableBeforeFirstEnterKey;
		bool lineAvailableAfterFirstEnterKey;
		bool lineAvailableAtEnd;
		
		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			MockTextEditor textEditor = new MockTextEditor();
			using (pythonConsole = new PythonConsole(textEditor, null)) {

				textEditor.RaiseKeyPressEvent('a');
				textEditor.RaiseKeyPressEvent('=');
				textEditor.RaiseKeyPressEvent('1');
				lineAvailableBeforeFirstEnterKey = pythonConsole.IsLineAvailable;
				textEditor.RaiseDialogKeyPressEvent(Keys.Enter);
				lineAvailableAfterFirstEnterKey = pythonConsole.IsLineAvailable;
				
				textEditor.RaiseKeyPressEvent('b');
				textEditor.RaiseKeyPressEvent('=');
				textEditor.RaiseKeyPressEvent('2');
				textEditor.RaiseDialogKeyPressEvent(Keys.Enter);

				Thread t = new Thread(ReadLinesOnSeparateThread);
				t.Start();

				int sleepInterval = 20;
				int currentWait = 0;
				int maxWait = 2000;
				
				while (line2 == null && currentWait < maxWait) {
					Thread.Sleep(sleepInterval);
					currentWait += sleepInterval;
				}
				
				lineAvailableAtEnd = pythonConsole.IsLineAvailable;
			}
		}
		
		[Test]
		public void FirstLineRead()
		{
			Assert.AreEqual("a=1", line1);
		}
		
		[Test]
		public void SecondLineRead()
		{
			Assert.AreEqual("b=2", line2);
		}
		
		[Test]
		public void LineAvailableBeforeEnterKeyPressed()
		{
			Assert.IsFalse(lineAvailableBeforeFirstEnterKey);
		}

		[Test]
		public void LineAvailableAfterEnterKeyPressed()
		{
			Assert.IsTrue(lineAvailableAfterFirstEnterKey);
		}
		
		[Test]
		public void LineAvailableAfterAllLinesRead()
		{
			Assert.IsFalse(lineAvailableAtEnd);
		}
		
		void ReadLinesOnSeparateThread()
		{
			line1 = pythonConsole.ReadLine(0);
			line2 = pythonConsole.ReadLine(0);
		}
	}
}
