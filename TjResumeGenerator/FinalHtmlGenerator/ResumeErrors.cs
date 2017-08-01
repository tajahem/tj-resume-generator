using System;
using System.Collections;

namespace FinalHtmlGenerator
{
	public class ResumeErrors
	{
		private ArrayList errors = new ArrayList();

		public void addError(string error){ errors.Add(error); }
		public void clearErrors(){ errors.Clear(); }
		public ArrayList getErrors(){ return errors; }

		public void printErrors()
		{
			foreach (string error in errors)
			{
				Console.Out.WriteLine("ERROR: " + error);				
			}
		}
	}
}

