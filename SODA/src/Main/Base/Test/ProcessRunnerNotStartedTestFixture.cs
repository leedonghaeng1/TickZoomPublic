// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 1391 $</version>
// </file>

using System;
using ICSharpCode.SharpDevelop.Util;
using NUnit.Framework;

namespace ICSharpCode.SharpDevelop.Tests
{
	/// <summary>
	/// Checks that the <see cref="ProcessRunner"/> responds
	/// correctly if it has not been started.
	/// </summary>
	[TestFixture]
	//[Ignore("Ignoring since need to run ConsoleApp.exe")]
	public class ProcessRunnerNotStartedTestFixture
	{
		ProcessRunner runner;
		
		[SetUp]
		public void Init()
		{
			runner = new ProcessRunner();
		}
		
		[Test]
		public void ExitCode()
		{
			Assert.AreEqual(0, runner.ExitCode, "Exit code should be zero.");
		}
		
		[Test]
		public void StandardOutput()
		{
			Assert.AreEqual(String.Empty, runner.StandardOutput, "Standard output should be empty.");
		}
		
		[Test]
		public void StandardError()
		{
			Assert.AreEqual(String.Empty, runner.StandardError, "Standard error should be empty.");
		}
		
		[Test]
		public void WaitForExit()
		{
			try {
				runner.WaitForExit();
				Assert.Fail("Expected ProcessRunnerException");
			} catch (ProcessRunnerException ex) {
				Assert.AreEqual(ICSharpCode.Core.StringParser.Parse("${res:ICSharpCode.NAntAddIn.ProcessRunner.NoProcessRunningErrorText}"),
				                ex.Message);
			}
		}
	}
}
