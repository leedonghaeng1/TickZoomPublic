// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision: 2313 $</version>
// </file>

using System;
using ICSharpCode.SharpDevelop.Gui;

namespace ICSharpCode.SharpDevelop
{
	/// <summary>
	/// This class defines the SharpDevelop display binding interface, it is a factory
	/// structure, which creates IViewContents.
	/// </summary>
	public interface ISecondaryDisplayBinding
	{
		/// <summary>
		/// Gets if the display binding can attach to the specified view content.
		/// </summary>
		bool CanAttachTo(IViewContent content);
		
		/// <summary>
		/// When you return true for this property, the CreateSecondaryViewContent method
		/// is called again after the LoadSolutionProjects thread has finished.
		/// </summary>
		bool ReattachWhenParserServiceIsReady { get; }
		
		/// <summary>
		/// Creates the secondary view contents for the given view content.
		/// If ReattachWhenParserServiceIsReady is used, the implementation is responsible
		/// for checking that no duplicate secondary view contents are added.
		/// </summary>
		IViewContent[] CreateSecondaryViewContent(IViewContent viewContent);
	}
}
