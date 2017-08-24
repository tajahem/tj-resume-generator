using System;
using System.Collections;

namespace ResumeGenerator
{
	public class ResumeErrors
	{
        public ResumeErrors(){ errors = new ArrayList(); }
		
        private ArrayList errors;

		public void AddError(string error){ errors.Add(error); }
		public void ClearErrors(){ errors.Clear(); }
		public ArrayList GetErrors(){ return errors; }

		public void PrintErrors()
		{
			foreach (string error in errors)
			{
                Console.ForegroundColor = ConsoleColor.Red;
				Console.Out.WriteLine("ERROR: " + error);
                Console.ResetColor();
			}
		}
	}
}

